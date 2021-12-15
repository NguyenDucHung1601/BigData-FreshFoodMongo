using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTOplus
{
    public class flatMaGiamGia
    {
        public Guid IDMaGiamGia { get; set; }
        public string MaGiamGia1 { get; set; }
        public int? SoLuong { get; set; }
        public decimal TienGiam { get; set; }
        public string DieuKienApDung { get; set; }
        public DateTime HanSuDung { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}