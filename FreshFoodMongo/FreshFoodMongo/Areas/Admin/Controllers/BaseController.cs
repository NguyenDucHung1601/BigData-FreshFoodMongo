using FreshFoodMongo.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public BaseController()
        {
            _client = new MongoClient(CommonVariables.g_connectionString);
            _database = _client.GetDatabase(CommonVariables.g_databaseName);
        }

        // GET: Admin/Base
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}