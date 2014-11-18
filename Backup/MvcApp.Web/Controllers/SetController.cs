using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class SetController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public SetController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /Model/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Set>();
            return View(data);
        }

        //
        // GET: /Model/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Set>(id);
            return View(data);
        }

        //
        // GET: /Model/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingSet();
            return View(model);
        }

        //
        // POST: /Model/Create

        [HttpPost]
        public ActionResult Create(CreateSetModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateSet(entity);
                    return RedirectToAction("Index");
                }
                return View(entity);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Model/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _manager.GetModelForEditingSet(id);
            return View(model);
        }

        //
        // POST: /Model/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateSetModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateSet(entity);
                    return RedirectToAction("Index");
                }
                return View(entity);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Model/Delete/5

        public ActionResult Delete(int id)
        {
            var data = _crudManager.GetById<Set>(id);
            return View(data);
        }

        //
        // POST: /Model/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Set>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
