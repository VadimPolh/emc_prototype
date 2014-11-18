using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;
        private readonly ITeacherManager _teacherManager;

        public TeacherController(ICrudManager crudManager, ICreateWithAdditionalManager manager, ITeacherManager teacherManager)
        {
            _crudManager = crudManager;
            _manager = manager;
            _teacherManager = teacherManager;
        }

        //
        // GET: /Model/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Teacher>();
            return View(data);
        }

        [HttpPost]
        public ActionResult SearchByFamily(string searchTeacherFamily)
        {
            var data = _teacherManager.FindByFio(searchTeacherFamily);
            return View("Index", data);
        }

        //
        // GET: /Model/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Teacher>(id);
            return View(data);
        }

        //
        // GET: /Model/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingTeacher();
            return View(model);
        }

        //
        // POST: /Model/Create

        [HttpPost]
        public ActionResult Create(CreateTeacherModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateTeacher(entity);
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
            var model = _manager.GetModelForEditingTeacher(id);
            return View(model);
        }

        //
        // POST: /Model/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateTeacherModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateTeacher(entity);
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
            var data = _crudManager.GetById<Teacher>(id);
            return View(data);
        }

        //
        // POST: /Model/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Teacher>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
