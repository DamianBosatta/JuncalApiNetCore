using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AceriaController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public AceriaController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AceriaRespuesta>>> GetAcerias()
        {

            var ListaAcerias = _uow.RepositorioJuncalAcerium.GetAllByCondition(c => c.Isdeleted == false).ToList();

            if (ListaAcerias.Count() > 0)
            {
                List<AceriaRespuesta> listaAceriasRespuesta = _mapper.Map<List<AceriaRespuesta>>(ListaAcerias);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaAceriasRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<AceriaRespuesta>() == null });


        }
        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdAceria(int id)
        {
            var aceria = _uow.RepositorioJuncalAcerium.GetById(id);

            if (aceria is null|| aceria.Isdeleted== true)
            {
                return Ok(new { success = false, message = "No Se Encontro La Aceria", result = new AceriaRespuesta() == null });
            }
            AceriaRespuesta aceriaRes = new AceriaRespuesta();

            _mapper.Map(aceria, aceriaRes);

            return Ok(new { success = true, message = "Aceria Encontrada", result = aceriaRes });



        }


        [HttpPost]
        public ActionResult CargarAceria([FromBody] AceriaRequerido aceriaReq)
        {
            var aceria = _uow.RepositorioJuncalAcerium.GetByCondition(c => c.Cuit.Equals(aceriaReq.Cuit)&& c.Isdeleted==false);

            if (aceria is null)
            {
                JuncalAcerium aceriaNuevo = _mapper.Map<JuncalAcerium>(aceriaReq);

                _uow.RepositorioJuncalAcerium.Insert(aceriaNuevo);
                return Ok(new { success = true, message = "La Aceria fue Creada Con Exito", result = aceriaNuevo });
            }
            AceriaRespuesta aceriaRes = new AceriaRespuesta();

            _mapper.Map(aceria, aceriaRes);
            return Ok(new { success = false, message = " La Aceria Ya Existe ", result = aceriaRes });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedAceria(int id)
        {

            var aceria = _uow.RepositorioJuncalAcerium.GetById(id);
            if (aceria != null && aceria.Isdeleted == false)
            {
                aceria.Isdeleted = true;
                _uow.RepositorioJuncalAcerium.Update(aceria);

                AceriaRespuesta aceriaRes = new AceriaRespuesta();

                _mapper.Map(aceria, aceriaRes);

                return Ok(new { success = true, message = "La Aceria Fue Eliminada ", result = aceriaRes });


            }


            return Ok(new { success = false, message = "La Aceria no fue encontrada", result = new AceriaRespuesta() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAceria(int id, AceriaRequerido aceriaEdit)
        {
            var aceria = _uow.RepositorioJuncalAcerium.GetById(id);
            
            if (aceria != null && aceria.Isdeleted == false)
            {
                _mapper.Map(aceriaEdit,aceria);
                _uow.RepositorioJuncalAcerium.Update(aceria);
                AceriaRespuesta aceriaRes = new AceriaRespuesta();
                _mapper.Map(aceria, aceriaRes);
                return Ok(new { success = true, message = "La Aceria fue actualizada", result = aceriaRes });
            }

            return Ok(new { success = false, message = "La Aceria  no fue encontrada ", result = new AceriaRespuesta() == null });


        }
    }
}
