using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public GroupController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /Group/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Group>();
            return View(data);
        }

        //
        // GET: /Group/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Group>(id);
            return View(data);
        }

        //
        // GET: /Group/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingGroup();
            return View(model);
        }

        //
        // POST: /Group/Create

        [HttpPost]
        public ActionResult Create(CreateGroupModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateGroup(entity);
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
        // GET: /Group/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _manager.GetModelForEditingGroup(id);
            return View(model);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateGroupModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateGroup(entity);
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
        // GET: /Group/Delete/5

        public ActionResult Delete(int id)
        {
            var data = _crudManager.GetById<Group>(id);
            return View(data);
        }

        //
        // POST: /Group/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Group>(id);
            }
            catch
            {
                //return View();
            }
            return RedirectToAction("Index");
        }
    }
}
