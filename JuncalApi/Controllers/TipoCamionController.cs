using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCamionController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public TipoCamionController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCamionRespuesta>>> GetTipoCamiones()
        {

            var ListaTipoCamiones = _uow.RepositorioJuncalTipoCamion.GetAll().ToList();

            if (ListaTipoCamiones.Count() > 0)
            {
                List<TipoCamionRespuesta> listaTipoCamionesRespuesta = _mapper.Map<List<TipoCamionRespuesta>>(ListaTipoCamiones);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaTipoCamionesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<TipoCamionRespuesta>() == null });


        }


        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdTipoCamion(int id)
        {
            var tipoCamion = _uow.RepositorioJuncalTipoCamion.GetById(id);

            if (tipoCamion is null)
            {
                return Ok(new { success = false, message = "No Se Encontro El Tipo Camion", result = new TipoCamionRespuesta() == null });
            }
            TipoCamionRespuesta TipoCamionRes = new TipoCamionRespuesta();

            _mapper.Map(tipoCamion, TipoCamionRes);

            return Ok(new { success = true, message = "Tipo Camion Encontrado", result = TipoCamionRes });

        }


        [HttpPost]
        public ActionResult CargarTipoCamion([FromBody] TipoCamionRequerido tipoCamionReq)
        {
            var tipoCamion = _uow.RepositorioJuncalTipoCamion.GetByCondition(c => c.Nombre.Equals(tipoCamionReq.Nombre));

            if (tipoCamion is null)
            {
                JuncalTipoCamion tipoCamionNuevo = _mapper.Map<JuncalTipoCamion>(tipoCamionReq);
                _uow.RepositorioJuncalTipoCamion.Insert(tipoCamionNuevo);
                return Ok(new { success = true, message = "El Tipo De Camion Fue Creado Con Exito", result = tipoCamionNuevo });
            }

            return Ok(new { success = false, message = " El Tipo Camion Ya Esta Cargado ", result = tipoCamion });

        }




        [HttpPut("{id}")]
        public async Task<IActionResult> EditTipoCamion(int id, TipoCamionRequerido tipoCamionEdit)
        {
            var tipoCamion = _uow.RepositorioJuncalTipoCamion.GetById(id);

            if (tipoCamion != null)
            {
                tipoCamion = _mapper.Map(tipoCamionEdit, tipoCamion);
                _uow.RepositorioJuncalTipoCamion.Update(tipoCamion);
                return Ok(new { success = true, message = "El Tipo De Camion fue Actualizado", result = tipoCamion });
            }

            return Ok(new { success = false, message = "El Tipo De Camion No Fue Encontrado ", result = new JuncalTipoCamion() == null });


        }




    }
}
