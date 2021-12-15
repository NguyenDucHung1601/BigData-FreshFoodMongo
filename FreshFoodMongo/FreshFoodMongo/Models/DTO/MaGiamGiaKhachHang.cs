using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class MaGiamGiaKhachHang
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDMaGiamGia { get; set; }
        [BsonElement]
        public Guid IDKhacHang { get; set; }
        [BsonElement]
        public bool? ConHanSuDung { get; set; }
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