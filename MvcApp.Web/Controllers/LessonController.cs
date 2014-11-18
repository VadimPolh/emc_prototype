using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class LessonController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public LessonController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /Lesson/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Lesson>();
            return View(data);
        }

        //
        // GET: /Lesson/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Lesson>(id);
            return View(data);
        }

        //
        // GET: /Lesson/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingLesson();
            return View(model);
        }

        //
        // POST: /Lesson/Create

        [HttpPost]
        public ActionResult Create(CreateLessonModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateLesson(entity);
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
        // GET: /Lesson/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _manager.GetModelForEditingLesson(id);
            return View(model);
        }

        //
        // POST: /Lesson/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateLessonModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateLesson(entity);
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
        // GET: /Lesson/Delete/5

        public ActionResult Delete(int id)
        {
            var data = _crudManager.GetById<Lesson>(id);
            return View(data);
        }

        //
        // POST: /Lesson/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Lesson>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
