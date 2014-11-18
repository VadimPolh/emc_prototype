using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public SubjectController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /Model/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Subject>();
            return View(data);
        }

        //
        // GET: /Model/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Subject>(id);
            return View(data);
        }

        //
        // GET: /Model/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingSubject();
            return View(model);
        }

        //
        // POST: /Model/Create

        [HttpPost]
        public ActionResult Create(CreateSubjectModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateSubject(entity);
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
            var model = _manager.GetModelForEditingSubject(id);
            return View(model);
        }

        //
        // POST: /Model/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateSubjectModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateSubject(entity);
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
            var data = _crudManager.GetById<Subject>(id);
            return View(data);
        }

        //
        // POST: /Model/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Subject>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
