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

        public List<JuncalRemitosReclamado> GetReclamos()
        {
            var query = from reclamos in _db.JuncalRemitosReclamados.Where(a=>a.IsDeleted==0 )
                        join aceria in _db.JuncalAceria.Where(a=>a.Isdeleted==false)
                        on reclamos.IdAceria equals aceria.Id
                        join remito in _db.JuncalOrdens.Where(a=>a.Isdeleted==false)
                        on reclamos.IdRemito equals remito.Id
                        join estadoReclamo in _db.JuncalEstadosReclamos.Where(a=>a.Isdelete==false)
                        on reclamos.IdEstadoReclamo equals estadoReclamo.Id
                        join usuarioReclamo in _db.JuncalUsuarios.Where(a=>a.Isdeleted==false)
                        on reclamos.IdUsuarioReclamo equals usuarioReclamo.Id
                        join usuarioIngresado in _db.JuncalUsuarios.Where(a => a.Isdeleted == false)
                        on reclamos.IdUsuarioIngreso equals usuarioIngresado.Id
                        join usuarioFinalizado in _db.JuncalUsuarios.Where(a => a.Isdeleted == false)
                        on reclamos.IdUsuarioFinalizado equals usuarioFinalizado.Id
                        select new { reclamos,aceria,remito,estadoReclamo,usuarioReclamo,usuarioIngresado,
                        usuarioFinalizado};

            List<JuncalRemitosReclamado> listaRespuesta = new List<JuncalRemitosReclamado>();

            foreach(var objQuery in query)
            {
                listaRespuesta.Add(new JuncalRemitosReclamado(objQuery.reclamos.Id, objQuery.reclamos.IdEstadoReclamo,
                   objQuery.reclamos.IdRemito, (DateTime)objQuery.reclamos.Fecha, objQuery.reclamos.Observacion,
                   (DateTime)objQuery.reclamos.FechaReclamo, objQuery.reclamos.ObservacionReclamo, (DateTime)objQuery.reclamos.FechaFinalizado,
                   objQuery.reclamos.ObservacionFinalizado, (int)objQuery.reclamos.IdUsuarioReclamo,(int)objQuery.reclamos.IdUsuarioIngreso,(int)objQuery.reclamos.IdUsuarioFinalizado,
                   objQuery.reclamos.IdAceria,objQuery.estadoReclamo.Nombre,objQuery.remito.Remito, objQuery.usuarioReclamo.Apellido, objQuery.usuarioFinalizado.Apellido,
                   objQuery.usuarioIngresado.Apellido,
                   objQuery.aceria.Nombre));


            }


            return listaRespuesta;

        }


    }
}
