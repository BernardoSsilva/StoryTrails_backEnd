from django.db import models

import uuid 

class User(models.Model):
    id=models.UUIDField(default = uuid.uuid4, editable = False, db_index=True, primary_key=True,auto_created=True)
    userName = models.CharField(max_length=255)
    userLogin = models.CharField(max_length=255, unique=True)
    userPassword = models.CharField(max_length=255)

    def __str__(self):
        return self.userName

class Collection(models.Model):
    id=models.UUIDField( default = uuid.uuid4,  editable = False, db_index=True, primary_key=True, auto_created=True, )
    user = models.ForeignKey(User, on_delete=models.CASCADE)
    collectionName = models.CharField(max_length=255)
    collectionObjective = models.IntegerField()
    def __str__(self):
        return self.collectionName
    
class Book(models.Model):
    id=models.UUIDField( default = uuid.uuid4, editable = False, db_index=True, primary_key=True, auto_created=True, )
    collection = models.ForeignKey(Collection, on_delete=models.CASCADE)
    bookName = models.CharField(max_length=255)
    pagesAmount = models.IntegerField()
    concluded = models.BooleanField()
    def __str__(self):
        return self.bookName
    