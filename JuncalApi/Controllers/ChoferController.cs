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
    public class ChoferController : Controller
    {


        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ChoferController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChoferRespuesta>>> GetChoferes()
        {

            var ListaChoferes = _uow.RepositorioJuncalChofer.GetAllByCondition(c=>c.Isdeleted==false).ToList();

            if (ListaChoferes.Count() > 0)
            {
                List<ChoferRespuesta> listaChoferesRespuesta = _mapper.Map<List<ChoferRespuesta>>(ListaChoferes);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = listaChoferesRespuesta });

            }
            
            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<ChoferRespuesta>() == null });
       

        }

        [HttpPost]
        public ActionResult CargarChofer([FromBody] ChoferRequerido choferReq)
        {
            var chofer = _uow.RepositorioJuncalChofer.GetByCondition(c=>c.Dni.Equals(choferReq.Dni) && c.Isdeleted == false);

            if (chofer is null)
            {
                JuncalChofer choferNuevo = _mapper.Map<JuncalChofer>(choferReq);

                _uow.RepositorioJuncalChofer.Insert(choferNuevo);
                return Ok(new { success = true, message = " Chofer Creado Con Exito ", result = choferNuevo });

            }
         
         return Ok(new { success = false, message = "El Chofer Ya Esta Registrado ", result = chofer });

        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedChofer(int id)
        {

            var chofer = _uow.RepositorioJuncalChofer.GetById(id);
            if (chofer != null && chofer.Isdeleted==false)
            {
                chofer.Isdeleted = true;
                _uow.RepositorioJuncalChofer.Update(chofer);
                return Ok(new { success = true, message = " Chofer Eliminado ", result = chofer.Isdeleted});
            }

            return Ok(new { success = false, message = "No Se Encontro El Chofer ", result = new ChoferRespuesta() == null });


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditChofer(int id, ChoferRequerido choferEdit)
        {
            var chofer = _uow.RepositorioJuncalChofer.GetById(id);

            if (chofer != null && chofer.Isdeleted == false)
            {
                chofer = _mapper.Map(choferEdit,chofer);
                _uow.RepositorioJuncalChofer.Update(chofer);
                return Ok(new { success = true, message = " El Chofer Ha Sido Actualizado ", result = chofer });
            }
           
            return Ok(new { success = false, message = "No Se Encontro El Chofer ", result = new ChoferRespuesta() == null });


        }

    }
}
