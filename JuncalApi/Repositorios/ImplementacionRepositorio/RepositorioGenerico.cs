using JuncalApi.DataBase;
using JuncalApi.Repositorios.InterfaceRepositorio;
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
            catch (Exception)
            {
             _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
             " A Continuacion Encontraras Mas Informacion -> ->");
             throw new InvalidOperationException("Excepcion En DataBase Al Hacer Delete(Repositorio Generico) ");
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return _db.Set<T>().ToList();
            }
            catch (Exception)
            {
             _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
             " A Continuacion Encontraras Mas Informacion -> ->");
             throw new InvalidOperationException("Excepcion En DataBase Al Hacer GetAll(Repositorio Generico) ");
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
            catch (Exception)
            {
             _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
             " A Continuacion Encontraras Mas Informacion -> ->");
             throw new InvalidOperationException("Excepcion En DataBase Al Hacer GetAll(Repositorio Generico) ");
            }
        }
        public List<T> GetAllByCondition(Expression<Func<T, bool>> where)
        {
            try
            {
                return _db.Set<T>().Where(where).ToList();
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
             " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En DataBase Al Hacer GetAllByCondition(Repositorio Generico) ");
            }
        }
        public T GetByCondition(Expression<Func<T, bool>> where)
        {
            try
            {
                return _db.Set<T>().Where(where).SingleOrDefault();
            }
            catch (Exception)
            {
             _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
             " A Continuacion Encontraras Mas Informacion -> ->");
            throw new InvalidOperationException("Excepcion En DataBase Al Hacer GetByCondition(Repositorio Generico) ");
            }
        }

        public T GetById(int id)
        {
            try
            {
                var model = _db.Set<T>().Find(id);
                if (model == null)
                {
                    var errorMessage = $"Objeto Con el Id: {id} No Se Encontro";
                _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
                 " A Continuacion Encontraras Mas Informacion -> ->");
                 throw new NotFoundException(errorMessage);

                }
                return model;
            }
            catch (Exception)
            {
            _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
            " A Continuacion Encontraras Mas Informacion -> ->");
            throw new InvalidOperationException("Excepcion En DataBase Al Hacer GetById(Repositorio Generico) ");
            }
        }

        public bool Insert(T model)
        {
            try
            {
                _db.Add(model);
                return Save();
            }
            catch (Exception)
            {
            _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
            " A Continuacion Encontraras Mas Informacion -> ->");
            throw new InvalidOperationException("Excepcion En DataBase Al Hacer Insert(Repositorio Generico)");
            }
        }

        public bool InsertRange(IEnumerable<T> model)
        {
            try
            {
                _db.AddRange(model);
                return Save();
            }
            catch (Exception)
            {
             _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
            " A Continuacion Encontraras Mas Informacion -> ->");
             throw new InvalidOperationException("Excepcion En DataBase Al Hacer InsertRange(Repositorio Generico) ");
            }
        }

        public bool Save()
        {
            try
            {
                return _db.SaveChanges() > 0;
            }
            catch (Exception)
            {
             _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
             " A Continuacion Encontraras Mas Informacion -> ->");
             throw new InvalidOperationException("Excepcion En DataBase Al Hacer Save(Repositorio Generico) ");
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
             _logger.LogError("ATENCION!! Capturamos Error En El Repositorio Generico" +
             " A Continuacion Encontraras Mas Informacion -> ->");
             throw new InvalidOperationException("Excepcion En DataBase Al Hacer Update(Repositorio Generico) ");
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
