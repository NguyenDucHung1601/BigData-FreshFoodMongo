using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class TaiKhoanThanhToan
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDTaiKhoan { get; set; }
        [BsonElement]
        public string VietTat { get; set; }
        [BsonElement]
        public string Ten { get; set; }
        [BsonElement]
        public string Logo { get; set; }
        [BsonElement]
        public string LoaiTaiKhoan { get; set; }
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