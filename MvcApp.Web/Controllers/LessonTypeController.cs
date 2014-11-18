using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class LessonTypeController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public LessonTypeController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /LessonType/

        public ActionResult Index()
        {
            var data = _crudManager.Get<LessonType>();
            return View(data);
        }

        //
        // GET: /LessonType/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<LessonType>(id);
            return View(data);
        }

        //
        // GET: /LessonType/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingLessonType();
            return View(model);
        }

        //
        // POST: /LessonType/Create

        [HttpPost]
        public ActionResult Create(CreateLessonTypeModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateLessonType(entity);
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
        // GET: /LessonType/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _manager.GetModelForEditingLessonType(id);
            return View(model);
        }

        //
        // POST: /LessonType/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateLessonTypeModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateLessonType(entity);
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
        // GET: /LessonType/Delete/5

        public ActionResult Delete(int id)
        {
            var data = _crudManager.GetById<LessonType>(id);
            return View(data);
        }

        //
        // POST: /LessonType/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<LessonType>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
