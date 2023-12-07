using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorListaPrecio:RepositorioGenerico<JuncalProveedorListaprecio>, IRepositorioJuncalProveedorListaPrecio
    {
        public RepositorioJuncalProveedorListaPrecio(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        #region OBTENER LISTA PRECIO POR ID
        public List<ProveedorListaPrecioRespuesta> ObtenerListaPrecioPorId(int id)
        {
            var listaPreciosActivas = _db.JuncalProveedorListaprecios
                                     .Where(lp => lp.Activo == true &&
                                      lp.FechaVigencia <= DateTime.Now &&
                                      lp.FechaVencimiento > DateTime.Now &&
                                      !lp.IsDeleted);


            var proveedoresJoin = listaPreciosActivas
                .Join(_db.JuncalProveedors.Where(p => !p.Isdeleted && p.Id == id),
                      lp => lp.IdProveedor,
                      proveedor => proveedor.Id,
                      (lp, proveedor) => new { ListaPrecio = lp, Proveedor = proveedor })
                .DefaultIfEmpty();

            var usuariosJoin = proveedoresJoin
                .GroupJoin(_db.JuncalUsuarios.Where(u => !u.Isdeleted),
                           pair => pair.ListaPrecio.IdUsuario,
                           usuario => usuario.Id,
                           (pair, usuarios) => new { ListaProveedor = pair, Usuarios = usuarios })
                .SelectMany(x => x.Usuarios.DefaultIfEmpty(),
                            (x, usuario) => new { ListaProveedor = x.ListaProveedor, Usuario = usuario });

            var query = usuariosJoin
                .Select(result => new ProveedorListaPrecioRespuesta
                {
                    Id = result.ListaProveedor.ListaPrecio.Id,
                    Nombre = result.ListaProveedor.ListaPrecio.Nombre,
                    IdProveedor = result.ListaProveedor.ListaPrecio.IdProveedor,
                    FechaVigencia = result.ListaProveedor.ListaPrecio.FechaVigencia,
                    FechaVencimiento = result.ListaProveedor.ListaPrecio.FechaVencimiento,
                    IdUsuario = result.ListaProveedor.ListaPrecio.IdUsuario,
                    Activo = result.ListaProveedor.ListaPrecio.Activo,
                    NombreProveedor = result.ListaProveedor.Proveedor != null ? result.ListaProveedor.Proveedor.Nombre : "Sin proveedor",
                    NombreUsuario = result.Usuario != null ? result.Usuario.Nombre : "Sin usuario"
                });

            return query.ToList();
        }

        #endregion
    }
}
