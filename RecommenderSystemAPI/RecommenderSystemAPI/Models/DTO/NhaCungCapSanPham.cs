using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class NhaCungCapSanPham
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDNhaCungCapSanPham { get; set; }
        [BsonElement]
        public Guid IDNhaCungCap { get; set; }
        [BsonElement]
        public Guid IDSanPham { get; set; }
        [BsonElement]
        public string DonViTinh { get; set; }
        [BsonElement]
        public decimal? GiaCungUng { get; set; }
        [BsonElement]
        public DateTime? NgayCapNhat { get; set; }
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