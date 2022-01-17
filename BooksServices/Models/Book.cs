﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BooksService.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
    }
} 