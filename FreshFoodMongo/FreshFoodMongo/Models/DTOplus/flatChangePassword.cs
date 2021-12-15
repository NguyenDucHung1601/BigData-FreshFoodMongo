using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTOplus
{
    public class flatChangePassword
    {
        public Guid IDUser { get; set; }
        public string Username { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }
        public string Confirm { get; set; }
    }
}