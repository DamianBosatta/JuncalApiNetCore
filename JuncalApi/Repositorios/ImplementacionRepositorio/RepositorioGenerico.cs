using JuncalApi.DataBase;
using JuncalApi.Repositorios.InterfaceRepositorio;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using System.Linq.Expressions;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{

    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class

    {

        protected readonly JuncalContext _db;
        protected readonly ILogger _logger;
    

        public RepositorioGenerico(JuncalContext db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public bool Delete(T model)
        {
            try
            {
                _db.Set<T>().Remove(model);
                return Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting object from the database");
                throw; 
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return _db.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all objects from the database");
                throw; 
            }
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                var query = _db.Set<T>().AsQueryable();
                query = PrepareQuery(query, predicate, orderBy);
                return query.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving objects with predicate from the database");
                throw; 
            }
        }
        public List<T> GetAllByCondition(Expression<Func<T, bool>> where)
        {
            try
            {
                return _db.Set<T>().Where(where).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving objects by condition from the database");
                throw; 
            }
        }
        public T GetByCondition(Expression<Func<T, bool>> where)
        {
            try
            {
                return _db.Set<T>().Where(where).SingleOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving object by condition from the database");
                throw; 
            }
        }

        public T GetById(int id)
        {
            try
            {
                var model = _db.Set<T>().Find(id);
                if (model == null)
                {
                    var errorMessage = $"Object with ID {id} not found in the database.";
                    _logger.LogError(errorMessage);
                    throw new NotFoundException(errorMessage);

                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving object by ID from the database");
                throw; 
            }
        }

        public bool Insert(T model)
        {
            try
            {
                _db.Add(model);
                return Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting object into the database");
                throw; 
            }
        }

        public bool InsertRange(IEnumerable<T> model)
        {
            try
            {
                _db.AddRange(model);
                return Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting a range of objects into the database");
                throw; 
            }
        }

        public bool Save()
        {
            try
            {
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving changes to the database");
                throw; 
            }
        }


        public bool Update(T model)
        {
            try
            {
                _db.Update(model);
                return Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating object in the database");
                throw; 
            }
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
