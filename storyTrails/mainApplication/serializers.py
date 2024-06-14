from rest_framework import serializers
from .models import User, Book, Collection
class UserSerializers( serializers.ModelSerializer):
    class Meta:
        model = User
        fields = "__all__"
    def validate(self, attrs):
       self._kwargs["partial"] = True
       return super().validate(attrs)
        
class BookSerializers( serializers.ModelSerializer):
    class Meta:
        model = Book
        fields = "__all__"
        

class CollectionSerializers( serializers.ModelSerializer):
    class Meta:
        model = Collection
        fields = "__all__"
        