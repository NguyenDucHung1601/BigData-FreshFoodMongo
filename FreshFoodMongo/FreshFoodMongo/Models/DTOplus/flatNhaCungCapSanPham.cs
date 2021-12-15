using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTOplus
{
    public class flatNhaCungCapSanPham
    {
        public Guid IDNhaCungCapSanPham { get; set; }

        public string TenNhaCungCap { get; set; }

        public string TenSanPham { get; set; }

        public string DonViTinh { get; set; }

        public decimal GiaCungUng { get; set; }

        public DateTime NgayCapNhat { get; set; }

    }
}