using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using JuncalApi.Modelos.Codigos_Utiles;

namespace JuncalApi.Servicios
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly IUnidadDeTrabajo _uow;
        public IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicioUsuario> _logger;

     

        public ServicioUsuario(IConfiguration configuration,IUnidadDeTrabajo uow,IMapper mapper,ILogger<ServicioUsuario> logger)
        {
            _configuration = configuration;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        #region REGISTRO DE USUARIO
        public JuncalUsuario RegistroUsuario(UsuarioRequerido userReq)
        {
            try
            {
                JuncalUsuario user = new JuncalUsuario();

                CreatePasswordhHash(userReq.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.Usuario = userReq.Usuario;
                user.Dni = (int)userReq.Dni;
                user.Nombre = userReq.Nombre;
                user.Apellido = userReq.Apellido;
                user.IdRol = CodigosUtiles.Usuario;
                user.Email = userReq.Email;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en el método RegistroUsuario(Servicio Usuario): {ErrorMessage}", ex.Message);
                throw; 
            }
        }
        #endregion

        #region CAMBIAR CONTRASEÑA
        public JuncalUsuario CambiarPassword(JuncalUsuario usuario, string password)
        {
            try
            {
                if (VerificarPassworHash(password, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    // El password proporcionado es el mismo que el actual, no se realiza ningún cambio
                    return new JuncalUsuario();
                }

                CreatePasswordhHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                usuario.PasswordHash = passwordHash;
                usuario.PasswordSalt = passwordSalt;

                return usuario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en el método CambiarPassword(Servicio Usuario): {ErrorMessage}", ex.Message);
                throw; 
            }
        }

        #endregion

        #region CREAR TOKEN
        public string CreateToken(JuncalUsuario user, DateTime expiraToken)
        {
            try
            {
                List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Usuario),
            new Claim(ClaimTypes.Role, ConvertirRolString((int)user.IdRol)),
        };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: expiraToken,
                    signingCredentials: creds
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en el método CreateToken(Servicio Usuario): {ErrorMessage}", ex.Message);
                throw;
            }
        }

        #endregion

        #region CREAR HASH DE CONTRASEÑA
        public void CreatePasswordhHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en el método CreatePasswordhHash(Servicio Usuario): {ErrorMessage}", ex.Message);
                throw; 
            }
        }
        #endregion

        #region VERIFICAR HASH DE CONTRASEÑA
        public bool VerificarPassworHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(passwordHash);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en el método VerificarPassworHash(Servicio Usaurio): {ErrorMessage}", ex.Message);
                throw; 
            }
        }
        #endregion

        #region CONVERTIR ROL A CADENA
        public string ConvertirRolString(int idRol)
        {
            try
            {
                var rol = idRol == CodigosUtiles.Administrador ? "Administrador" : "Usuario";
                return rol;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en el método ConvertirRolString(Servicio Usuario): {ErrorMessage}", ex.Message);
                throw; 
            }
        }
        #endregion

    }
}
