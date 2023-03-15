using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Servicios
{
    public interface IServicioUsuario
    {

        public string InicioSesion(LoginRequerido userReq);
        public JuncalUsuario RegistroUsuario(UsuarioRequerido userReq);

    }
}
