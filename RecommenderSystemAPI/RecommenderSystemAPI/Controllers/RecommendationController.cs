using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecommendSystemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace RecommenderSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly ILogger<RecommendationController> _logger;

        public RecommendationController(ILogger<RecommendationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/ProductRecommend")]
        public List<Guid> GetIDsRecommendProduct(Guid idKhachHang)
        {
            BaseDAO baseDao = new BaseDAO();
            var idSanPhams = baseDao.getDataSanPham().Select(x => x.IDSanPham).ToList();

            // Sample
            var recommendResult = new List<CustomPredictOject>();
            foreach (var item in idSanPhams)
            {
                recommendResult.Add(new CustomPredictOject()
                {
                    Sample = new ProductRecommender.ModelInput()
                    {
                        IDKhachHang = idKhachHang.ToString().ToLower(),
                        IDSanPham = item.ToString().ToLower()
                    },
                    User = idKhachHang,
                    Item = item,
                });
            };

            // Predict
            foreach (var item in recommendResult)
            {
                item.Score = ProductRecommender.Predict(item.Sample).Score;
            }

            //Result
            return recommendResult.OrderByDescending(x => x.Score).Select(x => x.Item).Take(10).ToList();
        }

        public class CustomPredictOject
        {
            public ProductRecommender.ModelInput Sample { get; set; }
            public Guid User { get; set; }
            public Guid Item { get; set; }
            public float Score { get; set; }
        }
    }
}
