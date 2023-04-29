using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Servicios;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

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

        [Route("Registro/")]
        [HttpPost]
        public ActionResult RegistrarUsuario([FromBody] UsuarioRequerido usuarioReq)
        {
           
            
           var usuarioExiste = _uow.RepositorioJuncalUsuario.GetByCondition(c => c.Usuario== usuarioReq.Usuario || c.Dni==usuarioReq.Dni && c.Isdeleted==false);

            if (usuarioExiste is null)
            {
                var usuario = _servicio.RegistroUsuario(usuarioReq);
                _uow.RepositorioJuncalUsuario.Insert(usuario);
                UsuarioRespuesta usuarioRes = new();
                _mapper.Map(usuario, usuarioRes);
                return Ok(new { success = true, message = "El Usuario fue Creado Con Exito", result = usuarioRes });
            }
           
             return Ok(new { success = false, message = " El Usuario Ya Esta Registrado ", result = new UsuarioRespuesta()==null });

        }
        /// <summary>
        /// Login de usuario
        /// </summary>
        /// <param name="userReq">usuario que se va a conectar</param>
        /// <param name="expiraToken">tiempo de expiracion del token , 
        /// tipo de date time que define el front por parametro</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login([FromBody] LoginRequerido userReq) 
        {
            DateTime expiraToken = DateTime.Now.AddHours(1);

            var usuario = _uow.RepositorioJuncalUsuario.GetByCondition(u => u.Usuario == userReq.Usuario && u.Isdeleted == false);
            var loginRespuesta = new LoginRespuesta();
            if (usuario != null)
            {
                if (!_servicio.VerificarPassworHash(userReq.Password, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    loginRespuesta.Token = "Password Incorrecto";

                    return Ok(new { success = false, message = "Password Incorrecto", result = loginRespuesta.Token });
                }

                string token = _servicio.CreateToken(usuario,expiraToken);
                var refreshToken = _servicio.GenerateRefreshToken(expiraToken);
                var cookieOptions = _servicio.SetRefreshToken(usuario, refreshToken);
                Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);



          
                loginRespuesta.Usuario = usuario.Usuario;
                loginRespuesta.Dni = usuario.Dni;
                loginRespuesta.Nombre = usuario.Nombre;
                loginRespuesta.Email = usuario.Email;
                loginRespuesta.Id = usuario.Id;
                loginRespuesta.Apellido = usuario.Apellido;
                loginRespuesta.IdRol = usuario.IdRol;
                loginRespuesta.Token = token;
                loginRespuesta.RefreshToken=usuario.RefreshToken;
                loginRespuesta.TokenCreated = usuario.TokenCreated;
                loginRespuesta.TokenExpires = usuario.TokenExpires;

                return  Ok(new { success = true, message = "Login Correcto", result = loginRespuesta });
            }

            loginRespuesta.Token = "No Se Encontro El Usuario";

            return Ok(new { success = false, message = "No Se Encontro El Usuario", result = loginRespuesta.Token });
        }
        /// <summary>
        /// End Point que renueva el token del usuario
        /// </summary>
        /// <param name="user">usuario al cual se le va a renovar el token</param>
        /// <param name="expiraToken">tiempo de expiracion del token pasarlo de tipo datetime</param>
        /// <returns></returns>
        [HttpPost("refresh-token") ,Authorize]
        public async Task<ActionResult<string>> RefreshToken(int idUser, bool logout)
        {
            var user = _uow.RepositorioJuncalUsuario.GetById(idUser);


            DateTime expiraToken = DateTime.Now;

           expiraToken = logout is true ? expiraToken = DateTime.Now.AddMinutes(1) : expiraToken = DateTime.Now.AddHours(1);


            if(user is null)
            {
                return Ok(new { success = false, message = "No Se Encontro El Usuario", result = new UsuarioRespuesta() });
            }


            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = _servicio.CreateToken(user,expiraToken);
            var refreshTokenNew = _servicio.GenerateRefreshToken(expiraToken);
            var cookieOptions = _servicio.SetRefreshToken(user, refreshTokenNew);
            Response.Cookies.Append("refreshToken", refreshTokenNew.Token, cookieOptions);

            return Ok(token);
        }


        [HttpPut("{id}"),Authorize]
        public async Task<IActionResult> EditUsuario(int id, UsuarioRequerido usuarioEdit)
        {
            var usuario = _uow.RepositorioJuncalUsuario.GetById(id);

            if (usuario != null && usuario.Isdeleted==false)
            {
                _mapper.Map(usuarioEdit, usuario);

                _uow.RepositorioJuncalUsuario.Update(usuario);
                UsuarioRespuesta usuarioRes = new();
                _mapper.Map(usuario, usuarioRes);


                return Ok(new { success = true, message = "El Usuario Fue Actualizado", result = usuarioRes });
            }

            return Ok(new { success = false, message = "El Usuario No Existe", result = new UsuarioRespuesta()==null });


        }
        [HttpPut, Authorize]
        public async Task<IActionResult> PasswordCambio(int id,string passwordNew)
        {

            var usuario = _uow.RepositorioJuncalUsuario.GetById(id);

            if (usuario != null && usuario.Isdeleted == false)
            {

                var user = _servicio.CambiarPassword(usuario, passwordNew);


                if (user.PasswordHash is null && user.PasswordSalt is null)
                {
                    return Ok(new { success = false, message = "No se puede poner el mismo password existente", result = new UsuarioRespuesta() == null });
                }
                else
                {
                    _uow.RepositorioJuncalUsuario.Update(user);
                    UsuarioRespuesta usuarioRes = new();
                    _mapper.Map(user, usuarioRes);
                    return Ok(new { success = true, message = "El Password Fue Actualizado", result = usuarioRes });

                }               
              
            }
            
                return Ok(new { success = false, message = "No se Encontro El Usuario", result = new UsuarioRespuesta() == null });
                                            
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
                UsuarioRespuesta usuarioRes = new();
                _mapper.Map(usuario, usuarioRes);

                return Ok(new { success = true, message = "El Usuario Fue Eliminado ", result = usuarioRes });


            }


            return Ok(new { success = false, message = "El Usuario No Existe ", result = new UsuarioRespuesta() == null });

        }





    }
}

