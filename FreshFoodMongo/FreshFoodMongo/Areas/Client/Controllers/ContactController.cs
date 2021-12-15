using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Client.Controllers
{
    public class ContactController : Controller
    {
        // GET: Client/Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}