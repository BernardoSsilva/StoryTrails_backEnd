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
        serializer =  UserSerializer(updatedUser, data=request.data, partial=True)
        
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_202_ACCEPTED)
    except:
        return Response(status=status.HTTP_400_BAD_REQUEST)

@api_view(["DELETE"])
def deleteUser(request, id):
    try:
        deletedUser = User.objects.get(pk=id)
        
        deletedUser.delete()
        return Response( status=status.HTTP_202_ACCEPTED)
        
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
            return Response({"token": token}, status=status.HTTP_200_OK)
        
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
        user = User.objects.get(pk=decodedToken["id"])
        
        allCollections = Collection.objects.all()
        
        collectionsSerializer = CollectionSerializer(allCollections, many=True)
   
        presenterBody = []
        for collection in collectionsSerializer.data:
            if collection["user"] == user.id:
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
def editCollection(request, id):
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
        collectionToExclude = Collection.objects.get(pk=id)
        
        if(str(decodedToken["id"]) == str(collectionToExclude.user.id)):
            collectionToExclude.delete()
            return Response( status=status.HTTP_202_ACCEPTED)
        else:
            return Response(status=status.HTTP_401_UNAUTHORIZED)

    except:
        return Response(status=status.HTTP_404_NOT_FOUND)

# * books endPoints
