using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class ThongTinNhanHang
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDThongTinNhanHang { get; set; }
        [BsonElement]
        public Guid? IDNguoiDung { get; set; }
        [BsonElement]
        public string TenNhanHang { get; set; }
        [BsonElement]
        public string SdtNhanHang { get; set; }
        [BsonElement]
        public string DiaChiNhanHang { get; set; }
        [BsonElement]
        public bool? MacDinh { get; set; }
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