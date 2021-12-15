using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class TheLoai
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDTheLoai { get; set; }
        [BsonElement]
        public string MaSo { get; set; }
        [BsonElement]
        public string Ten { get; set; }
        [BsonElement]
        public string HinhAnh { get; set; }
        [BsonElement]
        public string MoTa { get; set; }
        [BsonElement]
        public DateTime? CreatedDate { get; set; }
        [BsonElement]
        public string CreatedBy { get; set; }
        [BsonElement]
        public DateTime? ModifiedDate { get; set; }
        [BsonElement]
        public string ModifiedBy { get; set; }
    }
}