using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransportistaController : Controller
    {


        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public TransportistaController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportistaRespuesta>>> GetTransportistas()
        {

            var ListaTransportistas = _uow.RepositorioJuncalTransportistum.GetAllByCondition(t=>t.Isdeleted==false).ToList();   

            if (ListaTransportistas.Count() > 0)
            {
                List<TransportistaRespuesta> listaTransportistasRespuesta = _mapper.Map<List<TransportistaRespuesta>> (ListaTransportistas);
                return Ok(new { success = true, message = " La Lista Esta Lista Para Ser Utilizada", result = ListaTransportistas });

            }
            return Ok(new { success = false, message = " La Lista Esta Vacia", result = new List<TransportistaRespuesta>()== null }); ;

        }


        [HttpPost]
        public ActionResult CargarTransportista([FromBody] TransportistaRequerido transportistaReq)
        {
            var transportista = _uow.RepositorioJuncalTransportistum.GetByCondition(t => t.Cuit.Equals(transportistaReq.Cuit) && t.Isdeleted == false);

            if (transportista is null)
            {
                JuncalTransportistum TransportistaNuevo = _mapper.Map<JuncalTransportistum>(transportistaReq);
                _uow.RepositorioJuncalTransportistum.Insert(TransportistaNuevo);
                TransportistaRespuesta transportistaRes = new();
                _mapper.Map(TransportistaNuevo, transportistaRes);
                return Ok(new { success = true, message = " El Transportista Fue Creado Con Exito ", result = transportistaRes });
            }
            TransportistaRespuesta transportistaExiste = new();
            _mapper.Map(transportista, transportistaExiste);
            return Ok(new { success = false, message = " El Transportista Ya Existe ", result = transportistaExiste });

        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedTransportista(int id)
        {

            var transportista = _uow.RepositorioJuncalTransportistum.GetById(id);
            if (transportista != null && transportista.Isdeleted==false)
            {
                transportista.Isdeleted = true;
                _uow.RepositorioJuncalTransportistum.Update(transportista);
                TransportistaRespuesta transportistaRes = new();
                _mapper.Map(transportista, transportistaRes);
                return Ok(new { success = true, message = " El Transportista Fue Eliminado Con Exito ", result = transportistaRes});
            }

            return Ok(new { success = false, message = " El Transportista No Se Encontro ", result = new TransportistaRespuesta()== null });


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTransportista(int id, TransportistaRequerido transportistaEdit)
        {
            var transportista = _uow.RepositorioJuncalTransportistum.GetById(id);

            if ( transportista!= null && transportista.Isdeleted == false)
            {
                transportista = _mapper.Map(transportistaEdit,transportista);
                _uow.RepositorioJuncalTransportistum.Update(transportista);
                TransportistaRespuesta transportistaRes = new();
                _mapper.Map(transportista, transportistaRes);
                return Ok(new { success = true, message = " El Transportista Fue Actualizado Con Exito ", result = transportistaRes });
            }

            return Ok(new { success = false, message = " El Transportista No Se Encontro ", result = new TransportistaRespuesta() == null });


        }



















    }
}
