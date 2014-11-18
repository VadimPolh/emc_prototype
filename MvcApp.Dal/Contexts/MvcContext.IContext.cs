using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using MvcApp.Dal.DomainExtensions;
using MvcApp.Dal.Interfaces;
using MvcApp.DalEntities.Interfaces;

namespace MvcApp.Dal.Contexts
{
    public partial class MvcContext : IContext
    {
        public IQueryable<T> AsQueryable<T>(params Expression<Func<T, object>>[] includes) where T : class, IObject
        {
            IQueryable<T> query = this.Set<T>();

            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public T ToEntity<T>(T model, params Expression<Func<T, object>>[] includes) where T : class, IObject
        {
            T entity;

            if (model.IsNew())
            {
                entity = Set<T>().Add(Set<T>().Create());
                Entry(entity).CurrentValues.SetValues(model);
                //entity.Id = Guid.NewGuid();
            }
            else
            {
                entity = FindEntity(model.Id, includes);
                if (entity == null)
                {
                    throw new ObjectNotFoundException();
                }
                //http://stackoverflow.com/questions/4402586/optimisticconcurrencyexception-does-not-work-in-entity-framework-in-certain-situ
                if (!model.EntityVersion.ByteArrayCompare(entity.EntityVersion))
                {
                    throw new OptimisticConcurrencyException();
                }
                Entry(entity).CurrentValues.SetValues(model);
            }

            return entity;
        }

        #region Attach / Detach

        public T Attach<T>(T model) where T : class
        {
            var attachedEntity = Set<T>().Local.FirstOrDefault(e => e == model);
            if (attachedEntity != null)
            {
                return attachedEntity;
            }
            else
            {
                return Set<T>().Attach(model);
            }
        }

        public void Detach<T>(T model) where T : class
        {
            ObjectContext.Detach(model);
        }

        #endregion

        #region ASssign

        protected void Assign<T, TX>(T entity,
                         Expression<Func<T, TX>> reference,
                         TX model,
                         Func<TX, TX> itemPersister,
                         params Expression<Func<TX, object>>[] includes)
            where T : class, IObject
            where TX : class, IObject
        {
            DbEntityEntry<T> entityEntry = Entry(entity);
            DbReferenceEntry<T, TX> referenceEntry = entityEntry.Reference(reference);

            if (model != null)
            {
                model = itemPersister(model);
            }

            if (entityEntry.State == EntityState.Added)
            {
                referenceEntry.CurrentValue = model;
                IncludeRelations(referenceEntry, includes);
                return;
            }

            if (!referenceEntry.IsLoaded)
            {
                referenceEntry.Load();
            }

            IncludeRelations(referenceEntry, includes);

            if (referenceEntry.CurrentValue.Compare(model))
            {
                return;
            }

            referenceEntry.CurrentValue = model;
        }


        public void Assign<T, TX>(T entity,
                                 Expression<Func<T, TX>> reference,
                                 TX model,
                                 params Expression<Func<TX, object>>[] includes)
            where T : class, IObject
            where TX : class, IObject
        {
            Assign(entity, reference, model, x => FindEntity(x), includes);
        }


        public void AssignClassifier<T, TX>(T entity,
                         Expression<Func<T, TX>> reference,
                         TX model,
                         params Expression<Func<TX, object>>[] includes)
            where T : class, IObject
            where TX : class, IObject
        {
            Assign(entity, reference, model, x => Attach(x), includes);
        }


        public void AssignCollection<T, TX>(T entity,
                                           Expression<Func<T, ICollection<TX>>> reference,
                                           ICollection<TX> models,
                                           Func<TX, TX> itemPersister = null,
                                           bool deleteOrphan = false)
            where T : class, IObject
            where TX : class, IObject
        {
            if (models == null)
            {
                return;
            }

            DbEntityEntry<T> entityEntry = Entry(entity);
            DbCollectionEntry<T, TX> relationEntry = entityEntry.Collection(reference);

            var newItems = new List<TX>();
            if (itemPersister != null)
            {
                Func<TX, TX> persist = m =>
                {
                    var saved = itemPersister(m);
                    newItems.Add(saved);
                    return saved;
                };

                models = models.Select(m => m.IsNew() ? persist(m) : itemPersister(m)).ToList();
            }
            else
            {
                models = models.Select(m => FindEntity(m)).ToList();
            }

            if (entityEntry.State == EntityState.Added)
            {
                relationEntry.CurrentValue = models;
                return;
            }

            if (!relationEntry.IsLoaded)
            {
                relationEntry.Load();
            }

            if (relationEntry.CurrentValue == null)
            {
                relationEntry.CurrentValue = models;
                return;
            }

            int[] id = models.Select(x => x.Id).ToArray();
            var entities = Set<TX>().Where(x => id.Contains(x.Id)).AsEnumerable();
            Dictionary<int, TX> modelsHash = entities
                .Concat(newItems)
                .Distinct()
                .ToDictionary(x => x.Id);

            foreach (TX x in relationEntry.CurrentValue.ToList())
            {
                if (modelsHash.ContainsKey(x.Id))
                {
                    modelsHash.Remove(x.Id);
                }
                else
                {
                    relationEntry.CurrentValue.Remove(x);
                    if (deleteOrphan)
                    {
                        Set<TX>().Remove(x);
                    }
                }
            }

            foreach (TX x in modelsHash.Values)
            {
                relationEntry.CurrentValue.Add(x);
            }
        }


        public void AssignClassifiersCollection<T, TX>(T entity,
                                           Expression<Func<T, ICollection<TX>>> reference,
                                           ICollection<TX> models)
            where T : class, IObject
            where TX : class, IObject
        {
            AssignCollection(entity, reference, models, x => Attach(x));
        }

        #endregion

        public T Remove<T>(T value) where T : class, IObject
        {
            this.Set<T>().Remove(value);
            return value;
        }

        #region FindEntity

        public T FindEntity<T>(int id, params Expression<Func<T, object>>[] includes) where T : class, IObject
        {
            T entity;

            if (includes == null || includes.Length < 1)
            {
                entity = this.Set<T>().Find(id);
            }
            else
            {
                entity = AsQueryable(includes).FirstOrDefault(x => x.Id == id);
            }

            return entity;
        }

        public T FindEntity<T>(T model, params Expression<Func<T, object>>[] includes)
            where T : class, IObject
        {
            T entity;

            if (includes == null || includes.Length < 1)
            {
                entity = this.Set<T>().Find(model.Id);
            }
            else
            {
                entity = AsQueryable(includes).FirstOrDefault(x => x.Id == model.Id);
            }

            return entity;
        }

        #endregion

        #region Commit and clear cache

        public void Commit()
        {
            SaveChanges();
        }

        public void ClearCache()
        {
            var objectStateEntries = ObjectContext

            .ObjectStateManager

            .GetObjectStateEntries(~EntityState.Detached);

            foreach (var objectStateEntry in objectStateEntries)
            {
                if (objectStateEntry.State != EntityState.Detached && objectStateEntry.Entity != null)
                {
                    ObjectContext.Detach(objectStateEntry.Entity);
                }
            }
        }

        #endregion

        #region Private

        private ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }

        private void IncludeRelations<T, TX>(DbReferenceEntry<T, TX> referenceEntry, Expression<Func<TX, object>>[] includes)
            where T : class, IObject
            where TX : class, IObject
        {
            if (referenceEntry.CurrentValue == null)
                return;

            foreach (var include in includes)
            {
                var type = (((MemberExpression)include.Body).Member as System.Reflection.PropertyInfo);
                //collection
                if (type != null &&
                    type.PropertyType.IsGenericType &&
                    type.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    var name = ((MemberExpression)include.Body).Member.Name;
                    DbCollectionEntry includedEntry = Entry(referenceEntry.CurrentValue).Collection(name);
                    if (!includedEntry.IsLoaded)
                    {
                        includedEntry.Load();
                    }
                }
                //navigation property
                else
                {
                    DbReferenceEntry includedEntry = Entry(referenceEntry.CurrentValue).Reference(include);
                    if (!includedEntry.IsLoaded)
                    {
                        includedEntry.Load();
                    }
                }
            }
        }

        #endregion
        
    }
}
