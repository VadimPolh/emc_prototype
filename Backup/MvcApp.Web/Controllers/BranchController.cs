using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class BranchController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public BranchController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /Branch/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Branch>();
            return View(data);
        }

        //
        // GET: /Branch/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Branch>(id);
            return View(data);
        }

        //
        // GET: /Branch/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingBranch();
            return View(model);
        }

        //
        // POST: /Branch/Create

        [HttpPost]
        public ActionResult Create(CreateBranchModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateBranch(entity);
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
        // GET: /Branch/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _manager.GetModelForEditingBranch(id);
            return View(model);
        }

        //
        // POST: /Branch/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateBranchModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateBranch(entity);
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
        // GET: /Branch/Delete/5

        public ActionResult Delete(int id)
        {
            var data = _crudManager.GetById<Branch>(id);
            return View(data);
        }

        //
        // POST: /Branch/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Branch>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
