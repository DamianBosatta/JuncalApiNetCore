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
                        join chofer in _db.JuncalChofers
                        on camion.IdChofer equals chofer.Id
                        join transportista in _db.JuncalTransportista
                        on camion.IdTransportista equals transportista.Id
                        join tipoCamion in _db.JuncalTipoCamions
                        on camion.IdTipoCamion equals tipoCamion.Id
                        select new
                        {
                            camion,chofer,transportista, tipoCamion
                        
                        };

            

            var listaQuery = new List<ItemCamion>();

            foreach (var q in query)
            {
                listaQuery.Add(new ItemCamion(q.camion, q.chofer, q.transportista, q.tipoCamion));
            }

            return listaQuery;

        }
    }
}
    
    
    

