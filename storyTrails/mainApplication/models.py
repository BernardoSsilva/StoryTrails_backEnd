from django.db import models

class User(models.Model):
    id=models.UUIDField(unique=True, db_index=True, primary_key=True, unique=True, auto_created=True, )
    userName = models.CharField(max_length=255)
    userLogin = models.CharField(max_length=255, unique=True)
    userPassword = models.CharField(max_length=255, min_length=8)
    
    def __str__(self):
        return self.userName

class Collection(models.Model):
    id=models.UUIDField(unique=True, db_index=True, primary_key=True, unique=True, auto_created=True, )
    user = models.ForeignKey(User, on_delete=models.CASCADE)
    collectionName = models.CharField(max_length=255)
    collectionObjective = models.IntegerField()
    
class Book(models.Model):
    id=models.UUIDField(unique=True, db_index=True, primary_key=True, unique=True, auto_created=True, )
    collection = models.ForeignKey(Collection, on_delete=models.CASCADE)
    bookName = models.CharField(max_length=255)
    pagesAmount = models.IntegerField()
    concluded = models.BooleanField()
    