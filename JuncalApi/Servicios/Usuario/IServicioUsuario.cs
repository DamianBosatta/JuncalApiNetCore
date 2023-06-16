using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Servicios
{
    public interface IServicioUsuario
    {

  
        public JuncalUsuario RegistroUsuario(UsuarioRequerido userReq);
        public JuncalUsuario CambiarPassword(JuncalUsuario usuario, string password);
        public string CreateToken(JuncalUsuario user, DateTime expiraToken);
        public void CreatePasswordhHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerificarPassworHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public string ConvertirRolString(int idRol);


    }
}
