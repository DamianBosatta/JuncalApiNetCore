using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalRemitosReclamado:RepositorioGenerico<JuncalRemitosReclamado>,IRepositorioJuncalRemitosReclamado
    {
        public RepositorioJuncalRemitosReclamado(JuncalContext db) : base(db)
        {
        }

        public List<JuncalRemitosReclamado> GetReclamos(int idRemito, int idAceria)
        {
            var query = from reclamos in _db.JuncalRemitosReclamados.Where(a=>a.IsDeleted==0 )
                        select reclamos;

            query = idRemito == 0 ? query : query.Where(a => a.IdRemito == idRemito);

            query = idAceria == 0 ? query : query.Where(a => a.IdAceria == idAceria);


            return query.ToList();


        }


    }
}
