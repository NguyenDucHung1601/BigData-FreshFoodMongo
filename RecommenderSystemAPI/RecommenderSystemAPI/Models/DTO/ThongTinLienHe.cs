using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class ThongTinLienHe
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid ID { get; set; }
        [BsonElement]
        public string TenCuaHang { get; set; }
        [BsonElement]
        public string DiaChi { get; set; }
        [BsonElement]
        public string DienThoai1 { get; set; }
        [BsonElement]
        public string DienThoai2 { get; set; }
        [BsonElement]
        public string GioMoCua { get; set; }
        [BsonElement]
        public string Email { get; set; }
        [BsonElement]
        public string LinkFacebook { get; set; }
        [BsonElement]
        public string LinkYoutube { get; set; }
        [BsonElement]
        public string LinkInstagram { get; set; }
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