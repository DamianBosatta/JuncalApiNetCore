namespace JuncalApi.Modelos
{
    public partial class JuncalRemitosReclamado
    {
        public string? DescripcionEstadoReclamo { get; set; } = string.Empty;

        public string? Remito { get; set; } = string.Empty;

        public string? NombreUsuarioReclamo { get; set; } = string.Empty;

        public string? NombreUsuarioFinalizado { get; set; } = string.Empty;

        public string? NombreUsuarioIngresado { get; set; } = string.Empty;

        public string? NombreAceria { get; set; } = string.Empty;

        public JuncalRemitosReclamado() 
        {
            IsDeleted = 0;
        }

        public JuncalRemitosReclamado(int _id,int _idEstadoReclamo,int _idRemito,DateTime _fecha,string _observacion,
            DateTime _fechaReclamo,string _observacionReclamo,DateTime _fechaFinalizado,string _obsrvacionFinalizado,
            int _idUsuarioReclamo,int _idUsuarioIngreso,int _idUsuarioFinalizado,int _idAceria) : this()
        {

            Id= _id;
            IdEstadoReclamo= _idEstadoReclamo;
            IdRemito= _idRemito;
            Fecha= _fecha;
            Observacion= _observacion;
            FechaReclamo= _fechaReclamo;
            ObservacionReclamo= _observacionReclamo;
            FechaFinalizado= _fechaFinalizado;
            ObservacionFinalizado = _obsrvacionFinalizado;
            IdUsuarioReclamo= _idUsuarioReclamo;
            IdUsuarioIngreso= _idUsuarioIngreso;
            IdUsuarioFinalizado= _idUsuarioFinalizado;
            IdAceria= _idAceria;

        }

        public JuncalRemitosReclamado(int _id, int _idEstadoReclamo, int _idRemito, DateTime _fecha, string _observacion,
          DateTime _fechaReclamo, string _observacionReclamo, DateTime _fechaFinalizado, string _obsrvacionFinalizado,
          int _idUsuarioReclamo, int _idUsuarioIngreso, int _idUsuarioFinalizado, int _idAceria,
          string _descripcionEstadoReclamo,string _remito,string _nombreUsuarioReclamo,string _nombreUsuarioFinalizado,
          string _nombreUsuarioIngresado,string _nombreAceria) : 
          this(_id,_idEstadoReclamo,_idRemito, _fecha,_observacion,_fechaReclamo,_observacionReclamo,
          _fechaFinalizado,_obsrvacionFinalizado,_idUsuarioReclamo,_idUsuarioIngreso, _idUsuarioFinalizado, 
          _idAceria)
        {
            DescripcionEstadoReclamo = _descripcionEstadoReclamo;
            Remito = _remito;
            NombreUsuarioReclamo = _nombreUsuarioReclamo;
            NombreUsuarioFinalizado = _nombreUsuarioFinalizado;
            NombreUsuarioIngresado = _nombreUsuarioIngresado;
            NombreAceria = _nombreAceria;

        }




    }
}
