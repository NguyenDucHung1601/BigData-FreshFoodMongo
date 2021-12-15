using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class SanPham
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDSanPham { get; set; }
        [BsonElement]
        public int MaOrder { get; set; }
        [BsonElement]
        public string MaSo { get; set; }
        [BsonElement]
        public string Ten { get; set; }
        [BsonElement]
        public string DonViTinh { get; set; }
        [BsonElement]
        public decimal? GiaTien { get; set; }
        [BsonElement]
        public decimal? GiaKhuyenMai { get; set; }
        [BsonElement]
        public string HinhAnh { get; set; }
        [BsonElement]
        public string MoTa { get; set; }
        [BsonElement]
        public bool? CoSan { get; set; }
        [BsonElement]
        public long? SoLuong { get; set; }
        [BsonElement]
        public Guid IDTheLoai { get; set; }
        [BsonElement]
        public int? SoLuotXem { get; set; }
        [BsonElement]
        public int? SoLuotMua { get; set; }
        public DateTime? CreatedDate { get; set; }
        [BsonElement]
        public string CreatedBy { get; set; }
        [BsonElement]
        public DateTime? ModifiedDate { get; set; }
        [BsonElement]
        public string ModifiedBy { get; set; }
    }
}