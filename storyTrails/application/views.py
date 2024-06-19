from django.shortcuts import render
from django.http import HttpResponse
from rest_framework.decorators import api_view 
from rest_framework.response import Response
from rest_framework import status
import jwt
from .models import User, Book, Collection
from .serializers import BookSerializer, CollectionSerializer, UserSerializer
import os
from drf_yasg.utils import swagger_auto_schema
from drf_yasg import openapi



tokenKey = os.environ.get("TOKEN_KEY")
# * user endPoints
@swagger_auto_schema(
    method='get',
    responses={200: UserSerializer(many=True), 400:"Bad request", 204:"no content"}
)
@api_view(["GET"])
def getAllUsers(request):
    try:
        users = User.objects.all()
        serializer = UserSerializer(users, many=True)
        if(len(serializer.data) > 0):
            return Response(serializer.data, status=status.HTTP_200_OK)
        return Response({"details":"no content"},status=status.HTTP_204_NO_CONTENT)
    except:    
        return Response(status=status.HTTP_400_BAD_REQUEST)

@swagger_auto_schema(
    method='get',
    responses={200: UserSerializer(), 404:"not found"}
)
@api_view(["GET"])
def getById(request, id):
    try:
        user = User.objects.get(pk=id)
        serializer = UserSerializer(user)
         
        return Response(serializer.data, status=status.HTTP_200_OK)
    except: 
        return Response({"details":"user not found"},status=status.HTTP_404_NOT_FOUND)
         

@swagger_auto_schema(
    method='post',
    request_body=UserSerializer,
    responses={201: UserSerializer(), 400: 'Bad Request'}
)
@api_view(["POST"])
def createUser(request):
    try:
        serializer = UserSerializer( data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(status=status.HTTP_400_BAD_REQUEST) 
    except:
        return Response(status=status.HTTP_400_BAD_REQUEST)
    

@swagger_auto_schema(
    method='patch',
    request_body=UserSerializer(partial=True),
    responses={202: UserSerializer(), 401: 'Unauthorized',404:"not found"}
)
@api_view(["PATCH"])
def updateUser(request, id):
    try:
        updatedUser = User.objects.get(pk=id)
        print(updatedUser)
        serializer =  UserSerializer(updatedUser, data=request.data, partial=True)
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        if(str(decodedToken["id"]) == str(updatedUser.id)):
            if serializer.is_valid():
                serializer.save()
                return Response(serializer.data, status=status.HTTP_202_ACCEPTED)
        else: 
            return Response(status=status.HTTP_401_UNAUTHORIZED)
    except:
        return Response(status=status.HTTP_404_NOT_FOUND)

@swagger_auto_schema(
    method='delete',
    responses={202: "user deleted", 401: 'Unauthorized',404:"not found"}
)
@api_view(["DELETE"])
def deleteUser(request, id):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        deletedUser = User.objects.get(pk=id)
        if(str(decodedToken["id"]) == str(deletedUser.id)):
            deletedUser.delete()
            return Response({"details": "user deleted"}, status=status.HTTP_202_ACCEPTED)
        else:
            return Response({"details": "Unauthorized"},status=status.HTTP_401_UNAUTHORIZED)       
    except:
        return Response({"details": "not found"},status=status.HTTP_400_BAD_REQUEST)

@swagger_auto_schema(
    method='post',
    request_body=UserSerializer(partial=True),
    responses={200: UserSerializer(), 401: "Invalid credentials.",404:"not found",400:"User login and password are required."}
)
@api_view(["POST"])
def authenticate(request):
    try:
        user_login = request.data.get("userLogin")
        password = request.data.get("userPassword")

        if not user_login or not password:
            return Response({"detail": "User login and password are required."}, status=status.HTTP_400_BAD_REQUEST)

        user = User.objects.get(userLogin=user_login)
        userDict = user.to_dict()
       
        if(password == userDict["userPassword"]):
            token = jwt.encode({"id": userDict["id"]},tokenKey,  algorithm="HS256")
            serializer = UserSerializer(user)
            return Response({"token": token, "user":serializer.data}, status=status.HTTP_200_OK)
        
        return Response({"detail": "Invalid credentials."}, status=status.HTTP_401_UNAUTHORIZED)
    
    except:
        return Response({"detail": "User not found."}, status=status.HTTP_404_NOT_FOUND)
    
 


# * collections endPoints

@swagger_auto_schema(
    method='post',
    request_body=CollectionSerializer(),
    responses={200: CollectionSerializer(),400:"bad request"})
@api_view(["POST"])
def createNewCollection(request):
    try:
        token =  request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        
        user = User.objects.get(pk=decodedToken["id"])     

     
        collectionName = request.data.get("collectionName")
        collectionObjective = request.data.get("collectionObjective")
        
    
        
        collectionBody = {"user":user.id, "collectionName":collectionName, "collectionObjective":collectionObjective,}
        
        serializer = CollectionSerializer(data=collectionBody)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data,status=status.HTTP_201_CREATED)
    except:
        return Response({"details":"bad request"},status=status.HTTP_400_BAD_REQUEST)


@swagger_auto_schema(
    method='get',
    responses={200: CollectionSerializer(many=True), 204: "empty content",400:"bad request"}
)
@api_view(["GET"])
def findAllCollections(request):
    try:
        token =  request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey,  algorithms=["HS256"])
        allCollections = Collection.objects.all()
        
        collectionsSerializer = CollectionSerializer(allCollections, many=True)
   
        presenterBody = []
        for collection in collectionsSerializer.data:
            if str(collection["user"]) == str(decodedToken["id"]):
                presenterBody.append(collection)
        
        print(presenterBody)
        
        if presenterBody:
            return Response(presenterBody, status=status.HTTP_200_OK)
        return Response({"details": "Empty content."}, status=status.HTTP_204_NO_CONTENT)
    
    except:
        return Response({"details": "bad request"},status=status.HTTP_400_BAD_REQUEST)

@swagger_auto_schema(
    method='get',
    responses={200: CollectionSerializer(), 204: "empty content",404:"not found", 401:"unauthorized"}
) 
@api_view(["GET"])
def findCollectionById(request, id):
    try:
        token =  request.headers.get("token")
        decoded_token =  jwt.decode(token, tokenKey,  algorithms=["HS256"])
        
        
        collection = Collection.objects.get(pk=id)
        
        if(str(collection.user.id) == str(decoded_token["id"])):
            if(collection):
                serializer = CollectionSerializer(collection)
                return Response(serializer.data, status=status.HTTP_200_OK)
            return Response({"details":"empty content"},status=status.HTTP_204_EMPTY)
        else:
            return Response({"details":"unauthorized"}, status=status.HTTP_401_UNAUTHORIZED)
    except:
        return Response({"details":"not found"}, status=status.HTTP_404_NOT_FOUND)
 
@swagger_auto_schema(
    method='patch',
    request_body=CollectionSerializer(partial=True),
    responses={200: CollectionSerializer(), 400: "bad request", 401:"unauthorized"}
)    
@api_view(["PATCH"])
def updateCollection(request, id):
    try:
        token = request.headers.get("token")
        decoded_token =  jwt.decode(token, tokenKey,  algorithms=["HS256"])
        collectionToAlter = Collection.objects.get(pk=id)
        print(decoded_token, collectionToAlter)
        if((str(decoded_token["id"]) == str(collectionToAlter.user.id))):
            print(request.data)
            serializer = CollectionSerializer(collectionToAlter, data=request.data, partial=True)
            if(serializer.is_valid()):
                serializer.save()
                return Response(serializer.data, status=status.HTTP_200_OK)
            else:
                return Response({"details":"bad request"},status=status.HTTP_400_BAD_REQUEST)
        else:
            return Response({"details":"unauthorized"},status=status.HTTP_401_UNAUTHORIZED)
        
    except:
        return Response({"details":"bad request"},status=status.HTTP_400_BAD_REQUEST)
   

@swagger_auto_schema(
    method='delete',
    responses={202: "collection deleted", 400: "bad request", 401:"unauthorized"}
)      
@api_view(["DELETE"])
def deleteCollection(request, id):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        collectionToDelete = Collection.objects.get(pk=id)
        
        if(str(decodedToken["id"]) == str(collectionToDelete.user.id)):
            collectionToDelete.delete()
            return Response({"details":"collection deleted"},status=status.HTTP_202_ACCEPTED)
        else:
            return Response({"details":"unauthorized"},status=status.HTTP_401_UNAUTHORIZED)

    except:
        return Response({"details":"bad request"},status=status.HTTP_404_NOT_FOUND)

# * books endPoints

@swagger_auto_schema(
    method='post',
    request_body=BookSerializer(),
    responses={201: BookSerializer(), 400: "bad request", 401:"unauthorized"}
)  
@api_view(["POST"])
def createNewBook(request):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms = ["HS256"])        
        if(decodedToken):
            requestBody = {"collection":request.data.get("collection"),"bookName":request.data.get("bookName"),
                           "pagesAmount":request.data.get("pagesAmount"),
                           "concluded":request.data.get("concluded"),
                           "user":decodedToken["id"]}
           
            serializer = BookSerializer(data=requestBody)
            
            if serializer.is_valid():
                serializer.save()
                return Response(serializer.data, status = status.HTTP_201_CREATED)
        else:
            return Response({"details": "unauthorized"},status=status.HTTP_401_UNAUTHORIZED)
    except:
        return Response({"details": "bad request"},status=status.HTTP_400_BAD_REQUEST)


@swagger_auto_schema(
    method='get',
    responses={200: BookSerializer(many=True), 400: "bad request", 401:"unauthorized", 204: "empty content"}
)  
@api_view(["GET"])
def findAllBooks(request):
    try:
        token = request.headers.get("token")
  
        if(not token):
            return Response({"details": "unauthorized"},status=status.HTTP_401_UNAUTHORIZED)
        
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        allBooks = Book.objects.all()
        
        booksSerializer = BookSerializer(allBooks, many=True)
        booksByUser = []
        
        for book in booksSerializer.data:
       
            if(str(book["user"]) == str(decodedToken["id"])):
                booksByUser.append(book)
        
        if(len(booksByUser) > 0):
            return Response(booksByUser, status= status.HTTP_200_OK)
        else:
            return Response({"details": "empty content"},status = status.HTTP_204_NO_CONTENT)
    except:
        return Response({"details": "bad request"},status=status.HTTP_400_BAD_REQUEST)

@swagger_auto_schema(
    method='get',
    responses={200: BookSerializer(many=True), 404: "not found", 401:"unauthorized", 204: "empty content"}
)     
@api_view(["GET"])
def findAllBooksIntoCollection(request, id):
    try:
        token = request.headers.get("token")
  
        if(not token):
            return Response({"details": "unauthorized"}, status=status.HTTP_401_UNAUTHORIZED)
        
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        allBooks = Book.objects.all()
        
        
        booksSerializer = BookSerializer(allBooks, many=True)
        booksByCollection = []
        
        
        for book in booksSerializer.data:
            if(str(book["user"]) == str(decodedToken["id"])):
                if(str(book["collection"] )== str(id)):
                    booksByCollection.append(book)
        
        if(len(booksByCollection) > 0):
            return Response(booksByCollection, status= status.HTTP_200_OK)
        else:
            return Response({"details": "empty content"},status = status.HTTP_204_NO_CONTENT)
    except:
        return Response({"details": "not found"},status=status.HTTP_404_NOT_FOUND)
    
    
@swagger_auto_schema(
    method='get',
    responses={200: BookSerializer(many=True), 404: "not found", 401:"unauthorized"}
)     
@api_view(["GET"])
def findBookById(request, id):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        
        book = Book.objects.get(pk=id)
        
        if(str(book.user.id) == str(decodedToken["id"])):
            return Response(book, status=status.HTTP_200_OK)
        else:
            return Response({"details": "unauthorized"},status=status.HTTP_401_UNAUTHORIZED)
    except:
        return Response({"details": "not found"},status=status.HTTP_404_NOT_FOUND)


@swagger_auto_schema(
    method='patch',
    request_body=BookSerializer(partial=True),
    responses={200: BookSerializer(), 404: "not found", 401:"unauthorized"}
)  
@api_view(["PATCH"])
def updateBook(request, id):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        bookToUpdate = Book.objects.get(pk=id)
        if(str(decodedToken["id"]) == str(bookToUpdate.user.id)):
            serializer = BookSerializer(bookToUpdate,data=request.data ,partial=True)
            print(serializer)
            if(serializer.is_valid()):
                serializer.save()
                return Response(serializer.data, status=status.HTTP_200_OK)
            else:
                 return Response({"details": "not found"},status=status.HTTP_404_NOT_FOUND)
        else:
            return Response({"details": "unauthorized"}, status=status.HTTP_401_UNAUTHORIZED)
               
    except:
        return Response({"details": "not found"},status=status.HTTP_404_NOT_FOUND)


@swagger_auto_schema(
    method='delete',
    responses={204: "empty content", 404: "not found", 401:"unauthorized"}
)  
@api_view(["DELETE"])
def deleteBook(request, id):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        bookToDelete = Book.objects.get(pk=id)
        
        if(str(decodedToken["id"]) == str(bookToDelete.user.id)):
            bookToDelete.delete()
            return Response({"details": "empty content"},status=status.HTTP_204_NO_CONTENT)
        else:
            return Response({"details": "unauthorized"},status=status.HTTP_401_UNAUTHORIZED)

    except:
        return Response({"details": "not found"},status=status.HTTP_404_NOT_FOUND)
        
