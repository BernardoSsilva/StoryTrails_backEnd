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

# @api_view(["GET"])
# @api_view(["POST"])
# @api_view(["PATCH"])
# @api_view(["DELETE"])