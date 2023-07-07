using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorPresupuestoMaterial : RepositorioGenerico<JuncalProveedorPresupuestoMateriale>, IRepositorioJuncalProveedorPresupuestoMaterial
    {
        public RepositorioJuncalProveedorPresupuestoMaterial(JuncalContext db) : base(db)
        {
        }
    }
}
