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
    public class RolesController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public RolesController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesRespuesta>>> GetRoles()
        {

            var ListaRoles = _uow.RepositorioJuncalRole.GetAll().ToList();

            if (ListaRoles.Count() > 0)
            {
                List<RolesRespuesta> listaRolesRespuesta = _mapper.Map<List<RolesRespuesta>>(ListaRoles);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaRolesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RolesRespuesta>() == null });


        }

        [HttpPost]
        public ActionResult CargarRoles([FromBody] RolesRequerido RolesReq)
        {
            var roles = _uow.RepositorioJuncalRole.GetByCondition(c => c.Nombre.Equals(RolesReq.Nombre));

            if (roles is null)
            {
                JuncalRole rolNuevo = _mapper.Map<JuncalRole>(RolesReq);

                _uow.RepositorioJuncalRole.Insert(rolNuevo);
                return Ok(new { success = true, message = "El Rol fue Creado Con Exito", result = rolNuevo });
            }

            else return Ok(new { success = false, message = " El Rol Ya Existe ", result = roles });

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditRoles(int id, RolesRequerido rolEdit)
        {
            var  rol = _uow.RepositorioJuncalRole.GetById(id);

            if (rol != null)
            {
                _mapper.Map(rolEdit,rol);
                _uow.RepositorioJuncalRole.Update(rol);
                return Ok(new { success = true, message = "El Rol fue actualizado", result = rol });
            }

            return Ok(new { success = false, message = "El Rol no fue encontrado ", result = new JuncalRole() == null });


        }



    }
}
