from django.shortcuts import render
from django.http import HttpResponse
from rest_framework.decorators import api_view 
from rest_framework.response import Response
from rest_framework import status
import jwt
from .models import User, Book, Collection
from .serializers import BookSerializers, CollectionSerializers, UserSerializers
import os



# * user endPoints

@api_view(["GET"])
def getAllUsers(request):
    if request.method == "GET":
        users = User.objects.all()
        serializer = UserSerializers(users, many=True)
        
        return Response(serializer.data)
    
    return Response(status=status.HTTP_400_BAD_REQUEST)

@api_view(["GET"])
def getById(request, id):
     if request.method == "GET":
         try:
            user = User.objects.get(pk=id)
         except: 
             return Response(status=status.HTTP_404_NOT_FOUND)
         serializer = UserSerializers(user)
         
         return Response(serializer.data)
     
     return Response(status=status.HTTP_400_BAD_REQUEST)
@api_view(["POST"])
def createUser(request):
    try:
        serializer = UserSerializers( data=request.data)
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
        serializer =  UserSerializers(updatedUser, data=request.data, partial=True)
        
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
            token = jwt.encode({"id": userDict["id"]},'b3a60efa-6a44-4141-880b-97c26ffab9fe',  algorithm="HS256")
            return Response({"token": token}, status=status.HTTP_200_OK)
        
        return Response({"detail": "Invalid credentials."}, status=status.HTTP_401_UNAUTHORIZED)
    
    except:
        return Response({"detail": "User not found."}, status=status.HTTP_404_NOT_FOUND)
    
 


# * collections endPoints
@api_view(["GET"])
def findAllCollections(request):
    try:
        token = request.data.get("Authorization")

        decodedToken = jwt.decode(token,'b3a60efa-6a44-4141-880b-97c26ffab9fe',  algorithms=["HS256"])
        print(decodedToken["id"])
        user = User.objects.get(pk=decodedToken["id"])
        
        allCollections = Collection.objects.all()
        
        print(allCollections)
        collectionsSerializer = CollectionSerializers(allCollections, many=True)
        print("collectionsSerializer", collectionsSerializer)
        presenterBody = [collection for collection in collectionsSerializer.data if collection["user"] == str(user.id)]
        
        if presenterBody:
            return Response(presenterBody, status=status.HTTP_200_OK)
        return Response({"detail": "Empty content."}, status=status.HTTP_204_NO_CONTENT)
    
    except:
        return Response(status=status.HTTP_400_BAD_REQUEST)

# * books endPoints
