using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review.Domain.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string ProductId { get; set; }
        
        public string Title { get; set; }

        public Store Store { get; set; }

        public IEnumerable<ProductReview> Reviews { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}