using System.Linq.Expressions;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{

    public interface IRepositorioGenerico<T> where T : class
    {
        List<T> GetAllByCondition(Expression<Func<T, bool>> where);
        T GetByCondition(Expression<Func<T, bool>> where);
        List<T> GetAll(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        T GetById(int id);
        bool Insert(T model);
        public bool InsertRange(IEnumerable<T> model);
        bool Update(T model);
        bool Delete(T model);
        bool Save();

    }
}
