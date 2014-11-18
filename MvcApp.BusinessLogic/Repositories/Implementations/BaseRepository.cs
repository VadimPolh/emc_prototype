using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MvcApp.BusinessLogic.Repositories.Interfaces;
using MvcApp.Dal.Interfaces;
using MvcApp.DalEntities.Interfaces;

namespace MvcApp.BusinessLogic.Repositories.Implementations
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IContext _dataContext;

        protected IContext DataContext
        {
            get { return _dataContext; }
        }

        public BaseRepository(IContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Returns a Query instance for access to entities of the given type in the context
        /// </summary>
        /// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /// <param name="includes">Expressions for navigation properties, which should be included to query</param>
        /// <returns>A queryable for the given entity type.</returns>
        public IQueryable<TEntity> Query<TEntity>(params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IObject
        {
            return DataContext.AsQueryable(includes);
        }

        /// <summary>
        /// Returns a Query instance for access to entities of the given type in the context. Entity will not be cached in the DataContext
        /// </summary>
        /// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /// <param name="includes">Expressions for navigation properties, which should be included to query</param>
        /// <returns>A queryable for the given entity type.</returns>
        public IQueryable<TEntity> QueryWithNoTracking<TEntity>(params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IObject
        {
            return Query(includes).AsNoTracking();
        }

        /// <summary>
        /// Update or create new entity into DB in accordance with <paramref name="entity">model</paramref>
        /// </summary>
        /// <remarks>Update only simple properties, all navigation properies should be set manualy</remarks>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        /// <param name="entity">Model which will be actulalize</param>
        /// <param name="includes">Expressions for navigation properties, which should be included to requested entity</param>
        /// <returns>Updated entity</returns>
        public TEntity ToEntity<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IObject
        {
            return DataContext.ToEntity(entity, includes);
        }

        #region Attach / Detach

        /// <summary>
        /// Attach to context entity
        /// </summary>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        /// <param name="entity">Model which will be attached to context</param>
        /// <returns>Attached model</returns>
        public TEntity Attach<TEntity>(TEntity entity)
            where TEntity : class, IObject
        {
            return DataContext.Attach(entity);
        }

        /// <summary>
        /// Detach from context entity
        /// </summary>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        /// <param name="entity">Entity which will be detached from context</param>
        public void Detach<TEntity>(TEntity entity)
            where TEntity : class, IObject
        {
            DataContext.Detach(entity);
        }

        #endregion

        #region Assign

        /// <summary>
        /// Assign existing entity (Entity load from DB)
        /// </summary>
        /// <typeparam name="T">Type of root entity</typeparam>
        /// <typeparam name="TX">Type of entity which will attached to root object</typeparam>
        /// <param name="entity">Root entity whose navigation property will be set</param>
        /// <param name="reference">Expression to navigation property, where will set child object</param>
        /// <param name="model">Child object which necessary to attach (important only ID of object, other information reuqested from DataBase)</param>
        /// <param name="includes">Expressions for navigation properties, which should be included at request child object</param>
        public void Assign<T, TX>(T entity, Expression<Func<T, TX>> reference, TX model, params Expression<Func<TX, object>>[] includes)
            where T : class, IObject
            where TX : class, IObject
        {
            DataContext.Assign(entity, reference, model, includes);
        }


        /// <summary>
        /// Assign existing entity (Entity attached to data context)
        /// </summary>
        /// <typeparam name="T">Type of root entity</typeparam>
        /// <typeparam name="TX">Type of entity which will attached to root object</typeparam>
        /// <param name="entity">Root entity whose navigation property will be set</param>
        /// <param name="reference">Expression to navigation property, where will set child object</param>
        /// <param name="model">Child object which necessary to attach</param>
        /// <param name="includes">Expressions for navigation properties, which should be included at request child object</param>
        public void AssignClassifier<T, TX>(T entity,
                                               Expression<Func<T, TX>> reference,
                                               TX model,
                                               params Expression<Func<TX, object>>[] includes)
            where T : class, IObject
            where TX : class, IObject
        {
            DataContext.AssignClassifier(entity, reference, model, includes);
        }


        /// <summary>
        /// Assign existing entity collection (Entities load from DB)
        /// </summary>
        /// <typeparam name="T">Type of root entity</typeparam>
        /// <typeparam name="TX">Type of entities in collection which will attached to root object</typeparam>
        /// <param name="entity">Root entity whose navigation property will be set</param>
        /// <param name="reference">Expression to navigation property, where will set collection of child objects</param>
        /// <param name="models">Collection of Child objects which necessary to attach (important only ID of objects, other information reuqested from DataBase)</param>
        /// <param name="itemPersister">Function for override behavior for assign existing entities. Can be set for store entyty before assign.</param>
        /// <param name="deleteOrphan">Boolean parameter, should be set to TRUE if necessary remove child entities from DB, which not include to root entity after assign</param>
        public void AssignCollection<T, TX>(T entity,
                                    Expression<Func<T, ICollection<TX>>> reference,
                                    ICollection<TX> models,
                                    Func<TX, TX> itemPersister = null,
                                    bool deleteOrphan = false)
            where T : class, IObject
            where TX : class, IObject
        {
            DataContext.AssignCollection(entity, reference, models, itemPersister, deleteOrphan);
        }



        /// <summary>
        /// Assign existing entity collection (Entities attached to data context)
        /// </summary>
        /// <typeparam name="T">Type of root entity</typeparam>
        /// <typeparam name="TX">Type of entities in collection which will attached to root object</typeparam>
        /// <param name="entity">Root entity whose navigation property will be set</param>
        /// <param name="reference">Expression to navigation property, where will set collection of child objects</param>
        /// <param name="models">Collection of Child objects which necessary to attach</param>
        public void AssignClassifiersCollection<T, TX>(T entity,
                                                          Expression<Func<T, ICollection<TX>>> reference,
                                                          ICollection<TX> models)
            where T : class, IObject
            where TX : class, IObject
        {
            DataContext.AssignClassifiersCollection(entity, reference, models);
        }

        #endregion

        /// <summary>
        /// Remove entity from DataBase
        /// </summary>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        /// <param name="entity">Entity which should be removed from DB</param>
        /// <returns>Removed entity</returns>
        public void RemoveEntity<TEntity>(TEntity entity)
            where TEntity : class, IObject
        {
            DataContext.Remove(entity);
        }

        #region Find Entity

        /// <summary>
        /// Return Entity from DB by <paramref name="model">model</paramref> ID
        /// </summary>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        /// <param name="model">Model which should search in DB (important only ID of object)</param>
        /// <param name="includes">Expressions for navigation properties, which should be included to requested entity</param>
        /// <returns>Attached entity</returns>
        public TEntity FindEntity<TEntity>(TEntity model, params Expression<Func<TEntity, object>>[] includes) where TEntity : class, IObject
        {
            return DataContext.FindEntity(model, includes);
        }

        /// <summary>
        /// Return Entity from DB by model ID
        /// </summary>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        /// <param name="id">Model id which should be used to search in DB</param>
        /// <param name="includes">Expressions for navigation properties, which should be included to requested entity</param>
        /// <returns>Attached entity</returns>
        public TEntity FindEntity<TEntity>(int id, params Expression<Func<TEntity, object>>[] includes) where TEntity : class, IObject
        {
            return DataContext.FindEntity(id, includes);
        }

        #endregion
    }
}
