﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        public string Email { get; set; }
        public string HashPassword { get; set; }

        public List<int> LikedRecipes { get; set; }
        public List<int> CreatedRecipes { get; set; }

        public bool IsAdmin { get; set; }
    }
}
