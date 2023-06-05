namespace JuncalApi.Dto.DtoRequerido
{
    public class LoginRequerido
    {
        public string Usuario{ get; set; }
        public string Password { get; set; }

        public LoginRequerido(string usuario, string password)
        {
            Usuario = usuario is null ? string.Empty:usuario;
            Password = password is null ? string.Empty:password;
        }
    }
}
