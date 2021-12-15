using FreshFoodMongo.Models.DAO;
using MongoDB.Driver;
using System;
using Xunit;
using FreshFoodMongo.Common;

namespace TestFreshFoodMongo
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            BaseDAO baseDao = new BaseDAO();
            var dataNguoiDung = baseDao.getDataNguoiDung();
            var data = baseDao.getDataThongTinLienHe();
        }
    }
}
