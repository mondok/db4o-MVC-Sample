using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using Db4objects.Db4o.Ext;
using DbShared;

namespace DbWeb.Controllers
{
    public class HomeController : Controller
    {
        private IObjectContainer _database;

        public HomeController()
        {
            IClientConfiguration config = Db4oClientServer.NewClientConfiguration();
            config.Common.ObjectClass(typeof(ClothingType)).CascadeOnUpdate(true);
            config.Common.ObjectClass(typeof(ClothingType)).GenerateUUIDs(true);

            _database = Db4oClientServer.OpenClient(config, DbConfig.HOST, DbConfig.PORT, DbConfig.USER, DbConfig.PASSWORD);
        }

        public ActionResult Index()
        {
            IList<ClothingType> clothingTypes = _database.Query<ClothingType>();
            return View(clothingTypes);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateClothingType(ClothingType clothingType)
        {
            clothingType.Id = Guid.NewGuid().ToString();
            _database.Store(clothingType);
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CreateClothingType()
        {
            return View();
        }

        public ActionResult ClothingTypeDetails(string id)
        {
            ClothingType clothingType = _database.Query<ClothingType>(c => c.Id == id).First();
            return View(clothingType);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditClothingType(string id)
        {
            ClothingType clothingType = _database.Query<ClothingType>(c => c.Id == id).First();
            return View(clothingType);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditClothingType(ClothingType clothingType)
        {
            ClothingType clothingTypeOld = _database.Query<ClothingType>(c => c.Id == clothingType.Id).First();
            clothingTypeOld.Description = clothingType.Description;
            clothingTypeOld.Name = clothingType.Name;
            clothingTypeOld.Color = clothingType.Color;
            clothingTypeOld.DatePurchased = clothingType.DatePurchased;
            _database.Store(clothingTypeOld);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteClothingType(string id)
        {

            ClothingType clothingType = _database.Query<ClothingType>(c => c.Id == id).First();
            _database.Delete(clothingType);
            return RedirectToAction("Index");
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _database.Commit();
            base.OnActionExecuted(filterContext);
        }
    }
}
