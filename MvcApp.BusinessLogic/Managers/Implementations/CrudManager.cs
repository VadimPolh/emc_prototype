using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MvcApp.BusinessLogic.Managers.Base;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.BusinessLogic.Repositories.Interfaces;
using MvcApp.Dal.Interfaces;
using MvcApp.DalEntities.Interfaces;

namespace MvcApp.BusinessLogic.Managers.Implementations
{
    public class CrudManager : BaseManager, ICrudManager
    {
        public CrudManager(IContext context, IBaseRepository baseRepository) : base(context, baseRepository)
        {
        }

        public virtual T CreateOrUpdate<T>(T entity) where T : class, IObject
        {
            var result = BaseRepository.ToEntity(entity);
            Commit();
            return result;
        }

        public T GetById<T>(int id, params Expression<Func<T, object>>[] includes) where T : class, IObject
        {
            return BaseRepository.FindEntity<T>(id, includes);
        }

        public List<T> Get<T>(params Expression<Func<T, object>>[] includes) where T : class, IObject
        {
            return BaseRepository.QueryWithNoTracking<T>(includes).ToList();
        }

        public void Delete<T>(int id) where T : class, IObject
        {
            var e = BaseRepository.FindEntity<T>(id);
            BaseRepository.RemoveEntity(e);
            Commit();
        }

        public void Delete<T>(T entity) where T : class, IObject
        {
            var e = BaseRepository.FindEntity<T>(entity.Id);
            BaseRepository.RemoveEntity(e);
            Commit();
        }
    }
}
