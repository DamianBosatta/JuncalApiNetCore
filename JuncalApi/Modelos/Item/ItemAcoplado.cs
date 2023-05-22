namespace JuncalApi.Modelos.Item
{
    public class ItemAcoplado
    {
        public JuncalAcoplado Acoplado { get; set; }

        public JuncalTipoAcoplado TipoAcoplado { get; set; }


        public ItemAcoplado() { }

        public ItemAcoplado(JuncalAcoplado _acoplado,JuncalTipoAcoplado _tipoAcoplado)
        {
            Acoplado = _acoplado;
            TipoAcoplado = _tipoAcoplado;
        }


        public int Id { get { return Acoplado is null ? 0 : Acoplado.Id; } }

        public string Patente { get { return Acoplado is null ? string.Empty : Acoplado.Patente is null ? string.Empty : Acoplado.Patente;}}

        public string? Marca { get { return Acoplado is null ? string.Empty : Acoplado.Marca is null ? string.Empty : Acoplado.Marca; } }

        public string? Año { get { return Acoplado is null ? string.Empty : Acoplado.Año is null ? string.Empty : Acoplado.Año; } }

        public int IdTipo { get { return Acoplado is null ? 0 : Acoplado.IdTipo;} }

        public string? TipoDescripcion { get { return TipoAcoplado is null ? string.Empty : TipoAcoplado.Nombre is null ? string.Empty : TipoAcoplado.Nombre;} }
    }
}
