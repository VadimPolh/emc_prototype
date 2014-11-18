using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public CourseController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /Course/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Course>();
            return View(data);
        }

        //
        // GET: /Course/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Course>(id);
            return View(data);
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingCourse();
            return View(model);
        }

        //
        // POST: /Course/Create

        [HttpPost]
        public ActionResult Create(CreateCourseModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateCourse(entity);
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
        // GET: /Course/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _manager.GetModelForEditingCourse(id);
            return View(model);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateCourseModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateCourse(entity);
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
        // GET: /Course/Delete/5

        public ActionResult Delete(int id)
        {
            var data = _crudManager.GetById<Course>(id);
            return View(data);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Course>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
