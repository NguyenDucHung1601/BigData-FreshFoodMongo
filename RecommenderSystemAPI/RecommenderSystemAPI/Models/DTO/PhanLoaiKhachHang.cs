using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class PhanLoaiKhachHang
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDLoaiKhachHang { get; set; }
        [BsonElement]
        public int? CapDo { get; set; }
        [BsonElement]
        public string Ten { get; set; }
        [BsonElement]
        public int? SoDonHangToiThieu { get; set; }
        [BsonElement]
        public decimal? TongTienHangToiThieu { get; set; }
        [BsonElement]
        public string DieuKien { get; set; }
        public DateTime? CreatedDate { get; set; }
        [BsonElement]
        public string CreatedBy { get; set; }
        [BsonElement]
        public DateTime? ModifiedDate { get; set; }
        [BsonElement]
        public string ModifiedBy { get; set; }
    }
}