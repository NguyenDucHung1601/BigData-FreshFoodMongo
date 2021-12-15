using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class TKThanhToanNguoiDung
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDTaiKhoan { get; set; }
        [BsonElement]
        public Guid IDNguoiDung { get; set; }
        [BsonElement]
        public string Username { get; set; }
        [BsonElement]
        public string Password { get; set; }
        [BsonElement]
        public decimal? TongTien { get; set; }
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