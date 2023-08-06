﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel;

namespace XYZ.Models;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool Active { get; set; }
}