using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class NhaCungCap
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDNhaCungCap { get; set; }
        [BsonElement]
        public string Ten { get; set; }
        [BsonElement]
        public string DiaChi { get; set; }
        [BsonElement]
        public string HinhAnh { get; set; }
        [BsonElement]
        public string DienThoai { get; set; }
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