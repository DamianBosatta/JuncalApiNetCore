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
    public class TipoAcopladoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public TipoAcopladoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAcopladoRespuesta>>> GetTipoAcoplados()
        {

            var ListaTipoAcoplados = _uow.RepositorioJuncalTipoAcoplado.GetAll().ToList();

            if (ListaTipoAcoplados.Count() > 0)
            {
                List<TipoAcopladoRespuesta> listaTipoAcopladoRespuesta = _mapper.Map<List<TipoAcopladoRespuesta>>(ListaTipoAcoplados);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaTipoAcopladoRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<TipoAcopladoRespuesta>() == null });


        }


        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdTipoAcoplado(int id)
        {
            var tipoAcoplado = _uow.RepositorioJuncalTipoAcoplado.GetById(id);

            if (tipoAcoplado is null)
            {
                return Ok(new { success = false, message = "No Se Encontro El Tipo Acoplado", result = new TipoAcopladoRespuesta() == null });
            }
            TipoAcopladoRespuesta TipoAcopladoRes = new TipoAcopladoRespuesta();

            _mapper.Map(tipoAcoplado, TipoAcopladoRes);

            return Ok(new { success = true, message = "Tipo Acoplado Encontrado", result = TipoAcopladoRes });

        }


        [HttpPost]
        public ActionResult CargarTipoAcoplado([FromBody] TipoAcopladoRequerido tipoAcopladoReq)
        {
            var tipoAcoplado = _uow.RepositorioJuncalTipoAcoplado.GetByCondition(c => c.Nombre.Equals(tipoAcopladoReq.Nombre));

            if (tipoAcoplado is null)
            {
                JuncalTipoAcoplado tipoAcopladoNuevo = _mapper.Map<JuncalTipoAcoplado>(tipoAcopladoReq);
                _uow.RepositorioJuncalTipoAcoplado.Insert(tipoAcopladoNuevo);
                return Ok(new { success = true, message = "El Tipo De Acoplado Fue Creado Con Exito", result = tipoAcopladoNuevo });
            }

            return Ok(new { success = false, message = " El Tipo Acoplado Ya Esta Cargado ", result = tipoAcoplado });

        }




        [HttpPut("{id}")]
        public async Task<IActionResult> EditTipoAcoplado(int id, TipoAcopladoRequerido tipoAcopladoEditado)
        {
            var tipoAcoplado = _uow.RepositorioJuncalTipoAcoplado.GetById(id);

            if (tipoAcoplado != null)
            {
                tipoAcoplado = _mapper.Map(tipoAcopladoEditado, tipoAcoplado);
                _uow.RepositorioJuncalTipoAcoplado.Update(tipoAcoplado);
                return Ok(new { success = true, message = "El Tipo De Acoplado fue Actualizado", result = tipoAcoplado });
            }

            return Ok(new { success = false, message = "El Tipo De Acoplado No Fue Encontrado ", result = new JuncalTipoAcoplado() == null });


        }
    }
}
