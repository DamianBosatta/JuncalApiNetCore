using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrdenMaterialInternoRecogido : RepositorioGenerico<JuncalOrdenMaterialInternoRecogido>, IRepositorioJuncalOrdenMaterialInternoRecogido
    {
        public RepositorioJuncalOrdenMaterialInternoRecogido(JuncalContext db) : base(db)
        {
        }

        public List<JuncalOrdenMaterialInternoRecogido> listaMaterialesRecogidos(int idOrdenInterna)
        {

            var query = from materialRecogido in _db.JuncalOrdenMaterialInternoRecogidos.Where(a => a.IdOrdenInterno == idOrdenInterna)
                        join material in _db.JuncalMaterials on materialRecogido.IdMaterial equals material.Id
                        select new { materialRecogido, material };

            List<JuncalOrdenMaterialInternoRecogido> listaOrdenInterna = new List<JuncalOrdenMaterialInternoRecogido>();


            foreach(var q in query)
            {
                listaOrdenInterna.Add(new JuncalOrdenMaterialInternoRecogido(q.materialRecogido.Id,q.materialRecogido.IdOrdenInterno,
                q.materialRecogido.IdMaterial, q.materialRecogido.Peso, q.material.Nombre));


            }

            return listaOrdenInterna;

        }
    }
}
