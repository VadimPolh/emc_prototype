using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MvcApp.DalEntities.Interfaces;

namespace MvcApp.BusinessLogic.Managers.Interfaces
{
    public interface ICrudManager
    {
        T CreateOrUpdate<T>(T entity) where T : class, IObject;
        T GetById<T>(int id, params Expression<Func<T, object>>[] includes) where T : class, IObject;
        List<T> Get<T>(params Expression<Func<T, object>>[] includes) where T : class, IObject;
        void Delete<T>(int id) where T : class, IObject;
        void Delete<T>(T entity) where T : class, IObject;
    }
}
