using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class ChiTietGioHang
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDChiTietGioHang { get; set; }
        [BsonElement]
        public Guid IDKhachHang { get; set; }
        [BsonElement]
        public Guid IDSanPham { get; set; }
        [BsonElement]
        public int? SoLuong { get; set; }
        [BsonElement]
        public decimal? ThanhTien { get; set; }
        [BsonElement]
        public bool? DuocChon { get; set; }
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