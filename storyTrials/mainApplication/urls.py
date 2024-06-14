from django.urls import path
from . import views

urlpatterns = [
    path('users/find/all', views.getAllUsers, name="findAllUsers"),
    path('users/find/<str:id>', views.getById, name="findUserById"),
    path("users/create", views.createUser, name="createUser"),
    path("users/edit/<str:id>", views.updateUser, name="editUser")
    
]