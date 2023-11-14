﻿using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorListaPrecio:RepositorioGenerico<JuncalProveedorListaprecio>, IRepositorioJuncalProveedorListaPrecio
    {
        public RepositorioJuncalProveedorListaPrecio(JuncalContext db) : base(db)
        {
        }

        public List<ProveedorListaPrecioRespuesta> GetListaPrecioForId(int id)
        {
            var query = (from listaPrecio in _db.JuncalProveedorListaprecios.Where(a => a.Activo== true)
                         join proveedor in _db.JuncalProveedors.Where(a => a.Isdeleted == false && a.Id==id)
                         on listaPrecio.IdProveedor equals proveedor.Id into ProveedorJoin
                         from Proveedor in ProveedorJoin.DefaultIfEmpty()
                         join usuario in _db.JuncalUsuarios.Where(a => a.Isdeleted == false)
                          on listaPrecio.IdUsuario equals usuario.Id into UsuarioJoin
                         from Usuario in UsuarioJoin.DefaultIfEmpty()
                         select new ProveedorListaPrecioRespuesta
                         {
                             Id= listaPrecio.Id,
                             IdProveedor= listaPrecio.IdProveedor,
                             FechaVigencia=listaPrecio.FechaVigencia,
                             FechaVencimiento=listaPrecio.FechaVencimiento,
                             IdUsuario=listaPrecio.IdUsuario,
                             Activo=listaPrecio.Activo,
                             NombreProveedor= Proveedor.Nombre,
                             NombreUsuario=Usuario.Nombre

                         });


            return query.ToList();


        }
    }
}
