using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorListaPreciosMateriales:RepositorioGenerico<JuncalProveedorListapreciosMateriale>, IRepositorioJuncalProveedorListaPreciosMateriales
    {
        public RepositorioJuncalProveedorListaPreciosMateriales(JuncalContext db) : base(db)
        {
        }

        public List<ProveedorListaPrecioMaterialRespuesta> GetListaPreciosMateriales()
        {
            var query = (from listaprecMaterial in _db.JuncalProveedorListapreciosMateriales
                         join materiales in _db.JuncalMaterials.Where(a => a.Isdeleted == false)
                         on listaprecMaterial.IdMaterialJuncal equals materiales.Id into JoinMateriales
                         from _Materiales in JoinMateriales.DefaultIfEmpty()
                         join listaprecio in _db.JuncalProveedorListaprecios.Where(a => a.IsDeleted == false)
                         on listaprecMaterial.IdProveedorListaprecios equals listaprecio.Id into JoinListaPrecio
                         from _ListaPrecio in JoinListaPrecio.DefaultIfEmpty()
                         select new ProveedorListaPrecioMaterialRespuesta
                         {
                          Id= listaprecMaterial.Id,
                          IdProveedorListaprecios=listaprecMaterial.IdProveedorListaprecios,
                          IdMaterialJuncal=listaprecMaterial.IdMaterialJuncal,
                          Nombre=listaprecMaterial.Nombre,
                          Precio= listaprecMaterial.Precio,
                          NombreMaterial=_Materiales.Nombre,
                          NombreListaPrecio=_ListaPrecio.Nombre
                         });


            return query.ToList();


        }
    }
}
