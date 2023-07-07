using JuncalApi.Modelos;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalRemitosReclamado:IRepositorioGenerico<JuncalRemitosReclamado>
    {
        public List<JuncalRemitosReclamado> GetReclamos(int idRemito , int idAceria);
    }
}
