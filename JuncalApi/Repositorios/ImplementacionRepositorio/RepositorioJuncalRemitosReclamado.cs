using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalRemitosReclamado:RepositorioGenerico<JuncalRemitosReclamado>,IRepositorioJuncalRemitosReclamado
    {
        public RepositorioJuncalRemitosReclamado(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        #region GET RECLAMOS
        public List<JuncalRemitosReclamado> GetReclamos()
        {
            var query = from reclamos in _db.JuncalRemitosReclamados.Where(a=>a.IsDeleted==0 )
                        join aceria in _db.JuncalAceria.Where(a=>a.Isdeleted==false)
                        on reclamos.IdAceria equals aceria.Id into JoinAceria
                        from jAceria in JoinAceria.DefaultIfEmpty()
                        join remito in _db.JuncalOrdens.Where(a=>a.Isdeleted==false)
                        on reclamos.IdRemito equals remito.Id into JoinRemito
                        from jRemito in JoinRemito.DefaultIfEmpty()
                        join ordenMaterial in _db.JuncalOrdenMarterials.Where(a => a.Isdeleted == false)
                        on jRemito.Id equals ordenMaterial.IdOrden into JoinOrdenMaterial
                        from jOrdenMaterial in JoinOrdenMaterial.DefaultIfEmpty()
                        join material in _db.JuncalMaterials.Where(a => a.Isdeleted == false)
                        on jOrdenMaterial.IdMaterial equals material.Id into JoinMaterial
                        from jMaterial in JoinMaterial.DefaultIfEmpty()
                        join estadoReclamo in _db.JuncalEstadosReclamos.Where(a=>a.Isdelete==false)
                        on reclamos.IdEstadoReclamo equals estadoReclamo.Id into JoinEstadoReclamo
                        from jEstadoReclamo in JoinEstadoReclamo.DefaultIfEmpty()
                        join usuarioReclamo in _db.JuncalUsuarios.Where(a=>a.Isdeleted==false)
                        on reclamos.IdUsuarioReclamo equals usuarioReclamo.Id into JoinUsuarioReclamo
                        from jUsuarioReclamo in JoinUsuarioReclamo.DefaultIfEmpty()
                        join usuarioIngresado in _db.JuncalUsuarios.Where(a => a.Isdeleted == false)
                        on reclamos.IdUsuarioIngreso equals usuarioIngresado.Id into JoinUsuarioIngresado
                        from jUsuarioIngresado in JoinUsuarioIngresado.DefaultIfEmpty()
                        join usuarioFinalizado in _db.JuncalUsuarios.Where(a => a.Isdeleted == false)
                        on reclamos.IdUsuarioFinalizado equals usuarioFinalizado.Id into JoinUsuarioFinalizado
                        from jUsuarioFinalizado in JoinUsuarioFinalizado.DefaultIfEmpty()
                        select new { reclamos,jAceria,jRemito,jEstadoReclamo,jUsuarioReclamo,jUsuarioIngresado,
                        jUsuarioFinalizado,jOrdenMaterial,jMaterial};

            List<JuncalRemitosReclamado> listaRespuesta = new List<JuncalRemitosReclamado>();

            foreach (var objQuery in query)
            {
                JuncalRemitosReclamado nuevoReclamo = new JuncalRemitosReclamado(
                    objQuery.reclamos.Id,
                    objQuery.reclamos.IdEstadoReclamo,
                    objQuery.reclamos.IdRemito,
                    objQuery.reclamos.Fecha.HasValue ? (DateTime)objQuery.reclamos.Fecha : DateTime.MinValue,
                    objQuery.reclamos.Observacion,
                    objQuery.reclamos.FechaReclamo.HasValue ? (DateTime)objQuery.reclamos.FechaReclamo : DateTime.MinValue,
                    objQuery.reclamos.ObservacionReclamo,
                    objQuery.reclamos.FechaFinalizado.HasValue ? (DateTime)objQuery.reclamos.FechaFinalizado : DateTime.MinValue,
                    objQuery.reclamos.ObservacionFinalizado,
                    objQuery.reclamos.IdUsuarioReclamo.HasValue ? (int)objQuery.reclamos.IdUsuarioReclamo : 0,
                    objQuery.reclamos.IdUsuarioIngreso.HasValue ? (int)objQuery.reclamos.IdUsuarioIngreso : 0,
                    objQuery.reclamos.IdUsuarioFinalizado.HasValue ? (int)objQuery.reclamos.IdUsuarioFinalizado : 0,
                    objQuery.reclamos.IdAceria,
                    objQuery.jEstadoReclamo?.Nombre,
                    objQuery.jRemito?.Remito,
                    objQuery.jUsuarioReclamo?.Apellido,
                    objQuery.jUsuarioFinalizado?.Apellido,
                    objQuery.jUsuarioIngresado?.Apellido,
                    objQuery.jAceria?.Nombre,
                    (int)(objQuery.jMaterial?.Id),
                    objQuery.jMaterial.Nombre

                );

                listaRespuesta.Add(nuevoReclamo);
            }



            return listaRespuesta;

        }

        #endregion

    }
}
