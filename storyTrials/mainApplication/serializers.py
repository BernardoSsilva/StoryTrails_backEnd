from rest_framework import serializers
from .models import User, Book, Collection
class UserSerializers( serializers.ModelSerializer):
    class Meta:
        model = User
        fields = "__all__"
        
class BookSerializers( serializers.ModelSerializer):
    class Meta:
        model = Book
        fields = "__all__"
        

class CollectionSerializers( serializers.ModelSerializer):
    class Meta:
        model = Collection
        fields = "__all__"
        