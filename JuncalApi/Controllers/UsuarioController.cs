using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Servicios;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IServicioUsuario _servicio;

        public UsuarioController(IUnidadDeTrabajo uow, IMapper mapper,IServicioUsuario servicio)
        {

            _mapper = mapper;
            _uow = uow;
            _servicio = servicio;
        }

        [HttpGet,Authorize]
        public async Task<ActionResult<IEnumerable<RolesRespuesta>>> GetUsuarios()
        {

            var ListaUsuarios = _uow.RepositorioJuncalUsuario.GetAllByCondition(u=>u.Isdeleted==false).ToList();

            if (ListaUsuarios.Count() > 0)
            {
                List<UsuarioRespuesta> listaUsuarioRespuesta = _mapper.Map<List<UsuarioRespuesta>>(ListaUsuarios);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaUsuarioRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<UsuarioRespuesta>() == null });


        }

        [Route("/Registro")]
        [HttpPost]
        public ActionResult RegistrarUsuario([FromBody] UsuarioRequerido usuarioReq)
        {
           
            
           var usuarioExiste = _uow.RepositorioJuncalUsuario.GetByCondition(c => c.Dni.Equals(usuarioReq.Dni) && c.Isdeleted==false || c.Usuario.Equals(usuarioReq.Usuario));

            if (usuarioExiste is null)
            {
                var usuario = _servicio.RegistroUsuario(usuarioReq);
                _uow.RepositorioJuncalUsuario.Insert(usuario);
                return Ok(new { success = true, message = "El Usuario fue Creado Con Exito", result = usuario });
            }
           
             return Ok(new { success = false, message = " El Usuario Ya Esta Registrado ", result = new JuncalUsuario()==null });

        }

        [HttpPost]
        public ActionResult Login([FromBody] LoginRequerido userReq)
        {
            var sesion = _servicio.InicioSesion(userReq);
           
            var respuesta = string.Empty;

            if (sesion.Token == "NullUsuario")
            {
                respuesta = "Usuario No Encontrado";
                return Ok(new { success = false, message = "No Se Encontro El Usuario", result = respuesta });
            }
            else if(sesion.Token == "NoPass")
            {
               respuesta = "Password Incorrecto";
                return Ok(new { success = false, message = "Password Incorrecto", result = respuesta });
            }
         
       


            return Ok(new { success = true, message = "Login Correcto", result = sesion });


        }


        [HttpPut("{id}"),Authorize]
        public async Task<IActionResult> EditUsuario(int id, UsuarioRequerido usuarioEdit)
        {
            var usuario = _uow.RepositorioJuncalUsuario.GetById(id);

            if (usuario != null && usuario.Isdeleted==false)
            { 
            usuario = _mapper.Map(usuarioEdit,usuario);
                _uow.RepositorioJuncalUsuario.Update(usuario);
                
            return Ok(new { success = true, message = "El Usuario Fue Actualizado", result = usuario });
            }

            return Ok(new { success = false, message = "El Usuario No Existe", result = new JuncalUsuario()==null });


        }

        [Route("Borrar/{id?}"),Authorize]
        [HttpPut]
        public IActionResult IsDeletedUsuario(int id)
        {

            var usuario = _uow.RepositorioJuncalUsuario.GetById(id);
            if (usuario != null && usuario.Isdeleted == false)
            {
                usuario.Isdeleted = true;
                _uow.RepositorioJuncalUsuario.Update(usuario);

                return Ok(new { success = true, message = "El Usuario Fue Eliminado ", result = usuario.Isdeleted });


            }


            return Ok(new { success = false, message = "El Usuario No Existe ", result = new JuncalUsuario() == null });

        }







    }
}

