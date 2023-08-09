using JuncalApi.DataBase;
using JuncalApi.Repositorios.InterfaceRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{

    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class

    {

        protected readonly JuncalContext _db;

        public RepositorioGenerico(JuncalContext db)
        {
            _db = db;
        }


        public bool Delete(T model)
        {
            _db.Set<T>().Remove(model);
            return Save();
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate =
       null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var query = _db.Set<T>().AsQueryable();
            query = PrepareQuery(query, predicate, orderBy);
            return query.ToList();
        }

        public List<T> GetAllByCondition(Expression<Func<T, bool>>
       where)
        {
            return _db.Set<T>().Where(where).ToList();
        }
        public T GetByCondition(Expression<Func<T, bool>> where)
        {
            return _db.Set<T>().Where(where).SingleOrDefault();
        }

        public T GetById(int id)
        {
            var model = _db.Set<T>().Find(id);
            if (model == null)
                throw new Exception("Objeto no encontrado en la BD");
            return model;
        }

        public bool Insert(T model)
        {
            _db.Add(model);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        

        public bool Update(T model)
        {
            //Context.Set<T>().Update(model);
            _db.Update(model);
            return Save();
        }

        protected IQueryable<T> PrepareQuery(
        IQueryable<T> query,
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                query = orderBy(query);
            return query;
        }
    }
}
