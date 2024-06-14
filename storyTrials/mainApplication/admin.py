from django.contrib import admin
from .models import User, Book, Collection

admin.site.register(User)
admin.site.register(Book)
admin.site.register(Collection)
