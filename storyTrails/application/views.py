from django.shortcuts import render
from django.http import HttpResponse
from rest_framework.decorators import api_view 
from rest_framework.response import Response
from rest_framework import status
import jwt
from .models import User, Book, Collection
from .serializers import BookSerializer, CollectionSerializer, UserSerializer
import os




tokenKey = 'b3a60efa-6a44-4141-880b-97c26ffab9fe'
# * user endPoints



@api_view(["GET"])
def getAllUsers(request):
    if request.method == "GET":
        users = User.objects.all()
        serializer = UserSerializer(users, many=True)
        
        return Response(serializer.data)
    
    return Response(status=status.HTTP_400_BAD_REQUEST)

@api_view(["GET"])
def getById(request, id):
     if request.method == "GET":
         try:
            user = User.objects.get(pk=id)
         except: 
             return Response(status=status.HTTP_404_NOT_FOUND)
         serializer = UserSerializer(user)
         
         return Response(serializer.data)
     
     return Response(status=status.HTTP_400_BAD_REQUEST)
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

@api_view(["DELETE"])
def deleteUser(request, id):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        deletedUser = User.objects.get(pk=id)
        if(str(decodedToken["id"]) == str(deletedUser.id)):
            deletedUser.delete()
            return Response( status=status.HTTP_202_ACCEPTED)
        else:
            return Response(status=status.HTTP_401_UNAUTHORIZED)       
    except:
        return Response(status=status.HTTP_400_BAD_REQUEST)

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
        return Response(status=status.HTTP_400_BAD_REQUEST)

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
        return Response({"detail": "Empty content."}, status=status.HTTP_204_NO_CONTENT)
    
    except:
        return Response(status=status.HTTP_400_BAD_REQUEST)
    
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
            return Response(status=status.HTTP_204_EMPTY)
        else:
            return Response(status=status.HTTP_401_UNAUTHORIZED)
    except:
        return Response(status=status.HTTP_404_NOT_FOUND)
    
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
                return Response(status=status.HTTP_400_BAD_REQUEST)
        else:
            return Response(status=status.HTTP_401_UNAUTHORIZED)
        
    except:
        return Response(status=status.HTTP_400_BAD_REQUEST)
    
    
@api_view(["DELETE"])
def deleteCollection(request, id):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        collectionToDelete = Collection.objects.get(pk=id)
        
        if(str(decodedToken["id"]) == str(collectionToDelete.user.id)):
            collectionToDelete.delete()
            return Response( status=status.HTTP_202_ACCEPTED)
        else:
            return Response(status=status.HTTP_401_UNAUTHORIZED)

    except:
        return Response(status=status.HTTP_404_NOT_FOUND)

# * books endPoints
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
                print("chegou aqui")
                serializer.save()
                return Response(serializer.data, status = status.HTTP_201_CREATED)
        else:
            return Response(status=status.HTTP_401_UNAUTHORIZED)
    except:
        return Response(status=status.HTTP_400_BAD_REQUEST)


@api_view(["GET"])
def findAllBooks(request):
    try:
        token = request.headers.get("token")
  
        if(not token):
            return Response(status=status.HTTP_401_UNAUTHORIZED)
        
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
        return Response(status=status.HTTP_404_NOT_FOUND)
    
@api_view(["GET"])
def findAllBooksIntoCollection(request, id):
    try:
        token = request.headers.get("token")
  
        if(not token):
            return Response(status=status.HTTP_401_UNAUTHORIZED)
        
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
        return Response(status=status.HTTP_404_NOT_FOUND)
    
@api_view(["GET"])
def findBookById(request, id):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        
        book = Book.objects.get(pk=id)
        
        if(str(book.user.id) == str(decodedToken["id"])):
            return Response(book, status=status.HTTP_200_OK)
        else:
            return Response(status=status.HTTP_401_UNAUTHORIZED)
    except:
        return Response(status=status.HTTP_404_NOT_FOUND)


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
                 return Response(status=status.HTTP_404_NOT_FOUND)
        else:
            return Response(status=status.HTTP_401_UNAUTHORIZED)
               
    except:
        return Response(status=status.HTTP_404_NOT_FOUND)
    
@api_view(["DELETE"])
def deleteBook(request, id):
    try:
        token = request.headers.get("token")
        decodedToken = jwt.decode(token, tokenKey, algorithms=["HS256"])
        bookToDelete = Book.objects.get(pk=id)
        
        if(str(decodedToken["id"]) == str(bookToDelete.user.id)):
            bookToDelete.delete()
            return Response(status=status.HTTP_204_NO_CONTENT)
        else:
            return Response(status=status.HTTP_401_UNAUTHORIZED)

    except:
        return Response(status=status.HTTP_404_NOT_FOUND)
        
