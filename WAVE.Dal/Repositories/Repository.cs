using System.Linq;
using NHibernate;
using NHibernate.Linq;
using WAVE.Dal.Entities;
using WAVE.Dal.Modules;
using WAVE.Dal.Infrastructure;

namespace WAVE.Dal.Repositories
{
    public class Repository<T> : IIntKeyedRepository<T> where T : class
    {
        
        private readonly ISession _session;

        public Repository()
        {
            _session = NHibernateSessionPerRequest.GetCurrentSession();
        }

        #region IRepository<T> Members

        public bool Add(T entity)
        {
            _session.Save(entity);
            return true;
        }

        public bool Add(System.Collections.Generic.IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                _session.Save(item);
            }
            return true;
        }

        public bool Update(T entity)
        {
            _session.Update(entity);
            return true;
        }

        public bool Delete(T entity)
        {
            _session.Delete(entity);
            return true;
        }

        public bool Delete(System.Collections.Generic.IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                _session.Delete(entity);
            }
            return true;
        }

        #endregion

        #region IIntKeyedRepository<T> Members

        public T FindBy(int id)
        {
            return _session.Get<T>(id);
        }
        public T FindByEager(int id)
        {
            T entity = FindBy(id);
            NHibernateUtil.Initialize(entity);
            return entity;
        }

        #endregion

        #region IReadOnlyRepository<T> Members

        public IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public T FindBy(System.Linq.Expressions.Expression<System.Func<T, bool>> expression)
        {
            return FilterBy(expression).Single();
        }

        public IQueryable<T> FilterBy(System.Linq.Expressions.Expression<System.Func<T, bool>> expression)
        {
            return All().Where(expression).AsQueryable();
        }

        #endregion

    }

}