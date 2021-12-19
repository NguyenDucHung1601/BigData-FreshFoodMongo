using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class HoaDonNhap
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDHoaDonNhap { get; set; }
        [BsonElement]
        public Guid IDNhaCungCap { get; set; }
        [BsonElement]
        public int MaSo { get; set; }
        [BsonElement]
        public string GhiChu { get; set; }
        [BsonElement]
        public decimal? TienHang { get; set; }
        [BsonElement]
        public decimal? TienShip { get; set; }
        [BsonElement]
        public decimal TienGiam { get; set; }
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