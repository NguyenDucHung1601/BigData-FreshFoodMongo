using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Common
{
    public static class CommonVariables
    {
        public static readonly string g_connectionString = @"mongodb://127.0.0.1:27017";
        public static readonly string g_databaseName = "FreshFoodHTH";
        public static string g_apiGetRecommendProdct = @"https://localhost:44391/ProductRecommend?idKhachHang={0}";
    }
}