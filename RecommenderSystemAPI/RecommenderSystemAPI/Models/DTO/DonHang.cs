using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class DonHang
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDDonHang { get; set; }
        [BsonElement]
        public Guid IDKhachHang { get; set; }
        [BsonElement]
        public int MaSo { get; set; }
        [BsonElement]
        public string TenNhanHang { get; set; }
        [BsonElement]
        public string SdtNhanHang { get; set; }
        [BsonElement]
        public string DiaChiNhanHang { get; set; }
        [BsonElement]
        public string GhiChu { get; set; }
        [BsonElement]
        public decimal? TienHang { get; set; }
        [BsonElement]
        public decimal? TienShip { get; set; }
        [BsonElement]
        public decimal? TienGiam { get; set; }
        [BsonElement]
        public decimal? TongTien { get; set; }
        [BsonElement]
        public Guid IDTrangThai { get; set; }
        [BsonElement]
        public Guid IDPhuongThucThanhToan { get; set; }
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