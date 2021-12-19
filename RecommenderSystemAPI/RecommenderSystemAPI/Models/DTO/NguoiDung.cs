using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class NguoiDung
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDNguoiDung { get; set; }
        [BsonElement]
        public Guid IDLoaiNguoiDung { get; set; }
        [BsonElement]
        public Guid? IDLoaiKhachHang { get; set; }
        [BsonElement]
        public string Ten { get; set; }
        [BsonElement]
        public string DienThoai { get; set; }
        [BsonElement]
        public string Email { get; set; }
        [BsonElement]
        public string DiaChi { get; set; }
        [BsonElement]
        public string Username { get; set; }
        [BsonElement]
        public string Password { get; set; }
        [BsonElement]
        public string Avatar { get; set; }
        [BsonElement]
        public decimal? TongTienGioHang { get; set; }
        [BsonElement]
        public int? SoDonHangDaMua { get; set; }
        [BsonElement]
        public decimal? TongTienHangDaMua { get; set; }
        [BsonElement]
        public bool? TrangThai { get; set; }
        [BsonElement]
        public DateTime? LanHoatDongGanNhat { get; set; }
        [BsonElement]
        public bool IsAdmin { get; set; }
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