from django.urls import path
from . import views

urlpatterns = [
    path('users/find/all', views.getAllUsers, name="findAllUsers")
]