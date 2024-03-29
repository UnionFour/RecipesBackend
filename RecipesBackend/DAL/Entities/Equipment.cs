﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Equipment
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public new int? Id { get; set; }

        [BsonElement("name")]
		public LocalizedString Name { get; set; } = new();

		public string Image { get; set; } = "";
	}
}