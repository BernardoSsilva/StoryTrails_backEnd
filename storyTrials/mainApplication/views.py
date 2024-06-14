from django.shortcuts import render
from django.http import HttpResponse
from rest_framework.decorators import api_view 
from rest_framework.response import Response
from rest_framework import status
from .models import User, Book, Collection
from .serializers import BookSerializers, CollectionSerializers, UserSerializers

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
    

# @api_view(["PATCH"])
# @api_view(["DELETE"])