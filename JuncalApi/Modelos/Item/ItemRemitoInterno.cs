namespace JuncalApi.Modelos.Item
{
    public class ItemRemitoInterno
    {
        public JuncalOrdenInterno RemitoOrden { get; set; }

        public JuncalAcerium Aceria { get; set; }

        public JuncalContrato Contrato { get; set; }

        public JuncalCamion Camion { get; set; }

        public JuncalChofer Chofer { get; set; }

        public JuncalTransportistum Transportista { get; set; }

        public JuncalAcoplado Acoplado { get; set; }

        public JuncalEstadosInterno Estado { get; set; }

        public JuncalProveedor Proveedor { get; set; }

        public JuncalDireccionProveedor DireccionProveedores { get; set; }


        public ItemRemitoInterno() { }


        public ItemRemitoInterno(JuncalOrdenInterno remito, JuncalAcerium aceria, JuncalContrato contrato, JuncalCamion camion, JuncalChofer chofer, JuncalTransportistum transportista, JuncalAcoplado acoplado, JuncalEstadosInterno estado, JuncalProveedor proveedor, JuncalDireccionProveedor direccionProveedores) : this()
        {
            RemitoOrden = remito;
            Aceria = aceria;
            Contrato = contrato;
            Camion = camion;
            Chofer = chofer;
            Transportista = transportista;
            Acoplado = acoplado;
            Estado = estado;
            Proveedor = proveedor;
            DireccionProveedores = direccionProveedores;

        }

        public int Id { get { return RemitoOrden is null ? 0 : RemitoOrden.Id; } }

        public string? Remito { get { return RemitoOrden is null ? string.Empty : RemitoOrden.Remito is null ? string.Empty : RemitoOrden.Remito.ToString().Trim(); } }

        public DateTime FechaRemito { get { return RemitoOrden is null ? new DateTime() : RemitoOrden.Fecha; } }

        public string? Observacion { get { return RemitoOrden is null ? string.Empty : RemitoOrden.Observaciones is null ? string.Empty : RemitoOrden.Observaciones.ToString().Trim(); } }

        public int IdAceria { get { return Aceria is null ? 0 : Aceria.Id; } }

        public string? NombreAceria { get { return Aceria is null ? string.Empty : Aceria.Nombre is null ? string.Empty : Aceria.Nombre.ToString().Trim(); } }

        public string? DireccionAceria { get { return Aceria is null ? string.Empty : Aceria.Direccion is null ? string.Empty : Aceria.Direccion.ToString().Trim(); } }

        public string? CuitAceria { get { return Aceria is null ? string.Empty : Aceria.Cuit is null ? string.Empty : Aceria.Cuit.ToString().Trim(); } }

        public string? CodigoProveedorAceria { get { return Aceria is null ? string.Empty : Aceria.CodProveedor is null ? string.Empty : Aceria.CodProveedor.ToString().Trim(); } }

        public int IdContrato { get { return Contrato is null ? 0 : Contrato.Id; } }

        public string? NumeroContrato { get { return Contrato is null ? string.Empty : Contrato.Numero is null ? string.Empty : Contrato.Numero.ToString().Trim(); } }

        public int IdCamion { get { return (int)(Camion is null ? 0 : Camion.Id); } }

        public string? PatenteCamion { get { return Camion is null ? string.Empty : Camion.Patente is null ? string.Empty : Camion.Patente.ToString().Trim(); } }

        public int IdChofer { get { return Chofer is null ? 0 : Chofer.Id; } }

        public string? NombreChofer { get { return Chofer is null ? string.Empty : Chofer.Nombre is null ? string.Empty : Chofer.Nombre.ToString().Trim(); } }

        public string ApellidoChofer { get { return Chofer is null ? string.Empty : Chofer.Apellido is null ? string.Empty : Chofer.Apellido; } }

        public int LicenciaChofer { get { return Chofer is null ? 0 : Chofer.Dni; } }

        public int IdTransportista { get { return Transportista is null ? 0 : Transportista.Id; } }

        public string? NombreTransportista { get { return Transportista is null ? string.Empty : Transportista.Nombre is null ? string.Empty : Transportista.Nombre.ToString().Trim(); } }

        public int IdAcoplado { get { return Acoplado is null ? 0 : Acoplado.Id; } }

        public string? PatenteAcoplado { get { return Acoplado is null ? string.Empty : Acoplado.Patente is null ? string.Empty : Acoplado.Patente.ToString().Trim(); } }

        public int IdEstado { get { return Estado is null ? 0 : Estado.Id; } }

        public string? DescripcionEstado { get { return Estado is null ? string.Empty : Estado.Nombre is null ? string.Empty : Estado.Nombre.ToString().Trim(); } }

        public int IdProveedor { get { return Proveedor is null ? 0 : Proveedor.Id; } }

        public string? NombreProveedor { get { return Proveedor is null ? string.Empty : Proveedor.Nombre is null ? string.Empty : Proveedor.Nombre.ToString().Trim(); } }

        public int IdDireccionProveedor { get { return DireccionProveedores is null ? 0 : DireccionProveedores.Id; } }

        public string? DireccionProveedor { get { return DireccionProveedores is null ? string.Empty : DireccionProveedores.Direccion is null ? string.Empty : DireccionProveedores.Direccion.ToString().Trim(); } }
    }
}
