using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTO
{
    public class MaGiamGia
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public Guid IDMaGiamGia { get; set; }
        [BsonElement]
        public string CodeGiamGia { get; set; }
        [BsonElement]
        public decimal TienGiam { get; set; }
        [BsonElement]
        public Guid? IDLoaiKhachHang { get; set; }
        [BsonElement]
        public string DieuKienApDung { get; set; }
        [BsonElement]
        public DateTime? HanSuDung { get; set; }
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