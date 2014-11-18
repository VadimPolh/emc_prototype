using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public StudentController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /Student/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Student>();
            return View(data);
        }

        //
        // GET: /Student/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Student>(id);
            return View(data);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingStudent();
            return View(model);
        }

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(CreateStudentModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateStudent(entity);
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
        // GET: /Student/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _manager.GetModelForEditingStudent(id);
            return View(model);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateStudentModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateStudent(entity);
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
        // GET: /Student/Delete/5

        public ActionResult Delete(int id)
        {
            var data = _crudManager.GetById<Student>(id);
            return View(data);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Student>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
