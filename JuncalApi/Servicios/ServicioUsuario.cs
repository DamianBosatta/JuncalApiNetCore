using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace JuncalApi.Servicios
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly IUnidadDeTrabajo _uow;
        public IConfiguration _configuration;
        private readonly IMapper _mapper;
        public static JuncalUsuario user = new JuncalUsuario();

        public ServicioUsuario(IConfiguration configuration,IUnidadDeTrabajo uow,IMapper mapper)
        {
            _configuration = configuration;
            _uow = uow;
            _mapper = mapper;
        }


        public JuncalUsuario RegistroUsuario(UsuarioRequerido userReq)
        {
            CreatePasswordhHash(userReq.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Usuario=userReq.Usuario;
            user.Dni = userReq.Dni;
            user.Nombre=userReq.Nombre;
            user.Apellido = userReq.Apellido;
            user.IdRol=2;
            user.Email = userReq.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt =passwordSalt;

            return user;

        }

        public JuncalUsuario CambiarPassword(JuncalUsuario usuario,string password)
        {
            if (VerificarPassworHash(password, usuario.PasswordHash, usuario.PasswordSalt))
            {

               return new JuncalUsuario();

            }

            CreatePasswordhHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt=passwordSalt;

            return usuario;

        }


        public LoginRespuesta InicioSesion(LoginRequerido userReq)
        {
            var usuario = _uow.RepositorioJuncalUsuario.GetByCondition(u => u.Usuario == userReq.Usuario && u.Isdeleted==false);
            var loginRespuesta = new LoginRespuesta();
            if (usuario != null)
            {                        
                if (!VerificarPassworHash(userReq.Password, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    loginRespuesta.Token="NoPass";
                    return loginRespuesta;
                }

                string token = CreateToken(usuario);
               
                


                loginRespuesta.Usuario = usuario.Usuario;
                loginRespuesta.Dni = usuario.Dni;
                loginRespuesta.Nombre = usuario.Nombre;
                loginRespuesta.Email = usuario.Email;
                loginRespuesta.Id = usuario.Id;
                loginRespuesta.Apellido = usuario.Apellido;
                loginRespuesta.IdRol=usuario.IdRol;
                loginRespuesta.Token = token;

                return loginRespuesta;
            }

            loginRespuesta.Token = "NullUsuario";
           
            return loginRespuesta;
        }

        private string CreateToken(JuncalUsuario user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Usuario),
                new Claim(ClaimTypes.Role,ConvertirRolString((int)user.IdRol)),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials : creds

                ) ;
            var jwt= new JwtSecurityTokenHandler().WriteToken(token);


            return jwt;
        }



        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }


        public void SetRefreshToken(JuncalUsuario user, RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            //Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }



        private void CreatePasswordhHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {

                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            
            
            }




        }

        private bool VerificarPassworHash(string password , byte[] passwordHash, byte[] passwordSalt)
        {


            using (var hmac = new HMACSHA512(passwordSalt))
            {

                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash); 

            }



        }

        private string ConvertirRolString(int idRol)
        {
            var rol = string.Empty;

         rol= idRol is 1 ? rol = "Administrador" : rol = "Usuario";

            return rol;

        }

    }
}
