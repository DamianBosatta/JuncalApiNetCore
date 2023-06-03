using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCamion : RepositorioGenerico<JuncalCamion>, IRepositorioJuncalCamion
    {
      

        public RepositorioJuncalCamion(JuncalContext db) : base(db)
        {
        }


        public List<ItemCamion> GetAllCamiones()
        {
            var query = from camion in _db.JuncalCamions.Where(a=>a.Isdeleted==false)
                        join chofer in _db.JuncalChofers.Where(a=>a.Isdeleted==false)
                        on camion.IdChofer equals chofer.Id into JoinChofer
                        from chofer in JoinChofer.DefaultIfEmpty()
                        join transportista in _db.JuncalTransportista.Where(a=>a.Isdeleted==false)
                        on camion.IdTransportista equals transportista.Id into JoinTransportista
                        from transportista in JoinTransportista.DefaultIfEmpty()
                        join tipoCamion in _db.JuncalTipoCamions 
                        on camion.IdTipoCamion equals tipoCamion.Id into JoinTipoCamion
                        from tipoCamion in JoinTipoCamion.DefaultIfEmpty()
                        select new
                        {
                            camion,JoinChofer=chofer,JoinTransportista=transportista, JoinTipoCamion=tipoCamion
                        
                        };

            

            var listaQuery = new List<ItemCamion>();

            foreach (var q in query)
            {
                listaQuery.Add(new ItemCamion(q.camion, q.JoinChofer, q.JoinTransportista, q.JoinTipoCamion));
            }

            return listaQuery;

        }
    }
}
    
    
    

