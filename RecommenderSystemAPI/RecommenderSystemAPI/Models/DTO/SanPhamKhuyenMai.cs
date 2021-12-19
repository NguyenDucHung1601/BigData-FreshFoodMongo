using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class SanPhamKhuyenMai
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid ID { get; set; }
        [BsonElement]
        public Guid? IDSanPham { get; set; }
        [BsonElement]
        public decimal? GiaKhuyenMai { get; set; }
        [BsonElement]
        public DateTime? ThoiGianBatDau { get; set; }
        [BsonElement]
        public DateTime? ThoiGianKetThuc { get; set; }
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