using System.Web.Mvc;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.Web.Controllers
{
    public class ContainerController : Controller
    {
        private readonly ICrudManager _crudManager;
        private readonly ICreateWithAdditionalManager _manager;

        public ContainerController(ICrudManager crudManager, ICreateWithAdditionalManager manager)
        {
            _crudManager = crudManager;
            _manager = manager;
        }

        //
        // GET: /Container/

        public ActionResult Index()
        {
            var data = _crudManager.Get<Container>();
            return View(data);
        }

        //
        // GET: /Container/Details/5

        public ActionResult Details(int id)
        {
            var data = _crudManager.GetById<Container>(id);
            return View(data);
        }

        //
        // GET: /Container/Create

        public ActionResult Create()
        {
            var model = _manager.GetModelForEditingContainer();
            return View(model);
        }

        //
        // POST: /Container/Create

        [HttpPost]
        public ActionResult Create(CreateContainerModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateContainer(entity);
                    return RedirectToAction("Index");
                }
                return View(entity);
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }

        //
        // GET: /Container/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _manager.GetModelForEditingContainer(id);
            return View(model);
        }

        //
        // POST: /Container/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateContainerModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var created = _manager.CreateOrUpdateContainer(entity);
                    return RedirectToAction("Index");
                }
                return View(entity);
            }
            catch
            {
                return RedirectToAction("Edit", new{id = entity.Model.Id});
            }
        }

        //
        // GET: /Container/Delete/5

        public ActionResult Delete(int id)
        {
            var data = _crudManager.GetById<Container>(id);
            return View(data);
        }

        //
        // POST: /Container/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _crudManager.Delete<Container>(id);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
