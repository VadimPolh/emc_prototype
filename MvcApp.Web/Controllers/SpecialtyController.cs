using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class SpecialtyController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public SpecialtyController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /Specialty/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Specialty>();
            return View(data);
        }

        //
        // GET: /Specialty/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Specialty>(id);
            return View(data);
        }

        //
        // GET: /Specialty/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingSpecialty();
            return View(model);
        }

        //
        // POST: /Specialty/Create

        [HttpPost]
        public ActionResult Create(CreateSpecialtyModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateSpecialty(entity);
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
        // GET: /Specialty/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _manager.GetModelForEditingSpecialty(id);
            return View(model);
        }

        //
        // POST: /Specialty/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateSpecialtyModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateSpecialty(entity);
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
        // GET: /Specialty/Delete/5

        public ActionResult Delete(int id)
        {
            var data = _crudManager.GetById<Specialty>(id);
            return View(data);
        }

        //
        // POST: /Specialty/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Specialty>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
