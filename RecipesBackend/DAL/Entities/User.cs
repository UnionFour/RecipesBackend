﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public new string? Id { get; set; }

        public string Email { get; set; }
        public string passHash { get; set; }

        public string UserName { get; set; }

        public HashSet<string> LikedRecipes { get; set; } = new();
        public List<int> CreatedRecipes { get; set; } = new();

        public bool isSuper { get; set; }
    }
}
