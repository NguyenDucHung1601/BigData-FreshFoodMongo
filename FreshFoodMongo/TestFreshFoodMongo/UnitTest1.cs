using FreshFoodMongo.Models.DAO;
using MongoDB.Driver;
using System;
using Xunit;
using FreshFoodMongo.Common;
using FreshFoodMongo.Models.DTO;
using FreshFoodMongo.Models.DAO.Admin;

namespace TestFreshFoodMongo
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            CommonDAO commonDao = new CommonDAO();
            var data = commonDao.getRf_MaSoHoaDonNhap(new Guid("1253B097-BDA6-42E1-AF34-FD042B36E3CB"));
        }
    }
}
