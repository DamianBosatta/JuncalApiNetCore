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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

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
            CreatePasswordhHash(userReq.Contraseña, out byte[] passwordHash, out byte[] passwordSalt);
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


        public string InicioSesion(LoginRequerido userReq)
        {
            var usuario = _uow.RepositorioJuncalUsuario.GetByCondition(u => u.Usuario == userReq.Usuario && u.Isdeleted==false);

            if (usuario.Usuario != userReq.Usuario)
            {
                return " El Usuario No Existe";
            }

            if (!VerificarPassworHash(userReq.Password, usuario.PasswordHash, usuario.PasswordSalt))
            {
                return "Password Incorrecto";

            }

            string token = CreateToken(usuario);

            return token;



        }

        private string CreateToken(JuncalUsuario user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Usuario),
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

    }
}
