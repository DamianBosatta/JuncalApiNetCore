using JuncalApi.Modelos;


namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalAceriaMaterial:IRepositorioGenerico<JuncalAceriaMaterial>
    {
        public List<JuncalAceriaMaterial> GetAceriaMaterialesForIdAceria(int idAceria);


    }
}
