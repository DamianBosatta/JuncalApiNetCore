namespace JuncalApi.Modelos.Item
{
    public class ItemRemito
    {
        public JuncalOrden RemitoOrden { get; set; }

        public JuncalAcerium Aceria { get; set; }

        public JuncalContrato Contrato { get; set; }

        public JuncalCamion Camion { get; set; }

        public JuncalChofer Chofer { get; set; }

        public JuncalTransportistum Transportista { get; set; }

        public JuncalAcoplado Acoplado { get; set; }

        public JuncalEstado Estado { get; set; }

        public JuncalProveedor Proveedor { get; set; }

        public JuncalDireccionProveedor DireccionProveedores { get; set; }


        public ItemRemito() { }


        public ItemRemito(JuncalOrden remito, JuncalAcerium aceria, JuncalContrato contrato, JuncalCamion camion, JuncalChofer chofer, JuncalTransportistum transportista, JuncalAcoplado acoplado, JuncalEstado estado, JuncalProveedor proveedor, JuncalDireccionProveedor direccionProveedores):this()
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

        public string? Observacion { get { return RemitoOrden is null ? string.Empty : RemitoOrden.Observaciones is null ? string.Empty : RemitoOrden.Observaciones.ToString().Trim();} }

        public string? NombreAceria { get { return Aceria is null ? string.Empty : Aceria.Nombre is null ? string.Empty : Aceria.Nombre.ToString().Trim();} }

        public string? DireccionAceria { get { return Aceria is null ? string.Empty : Aceria.Direccion is null ? string.Empty : Aceria.Direccion.ToString().Trim(); } }

        public string? CuitAceria { get { return Aceria is null ? string.Empty : Aceria.Cuit is null ? string.Empty : Aceria.Cuit.ToString().Trim(); } }

        public string? CodigoProveedorAceria { get { return Aceria is null ? string.Empty : Aceria.CodProveedor is null ? string.Empty : Aceria.CodProveedor.ToString().Trim();} }

        public string? NumeroContrato { get { return Contrato is null ? string.Empty : Contrato.Numero is null ? string.Empty : Contrato.Numero.ToString().Trim(); } }

        public string? PatenteCamion { get { return Camion is null ? string.Empty : Camion.Patente is null ? string.Empty : Camion.Patente.ToString().Trim(); } }

        public string? NombreChofer { get { return Chofer is null ? string.Empty : Chofer.Nombre is null ? string.Empty : Chofer.Nombre.ToString().Trim(); } }

        public int LicenciaChofer { get { return Chofer is null ? 0 : Chofer.Dni; } }

        public string? NombreTransportista { get { return Transportista is null ? string.Empty : Transportista.Nombre is null ? string.Empty : Transportista.Nombre.ToString().Trim(); } }

        public string? PatenteAcoplado { get { return Acoplado is null ? string.Empty : Acoplado.Patente is null ? string.Empty : Acoplado.Patente.ToString().Trim();} }

        public int IdEstado { get { return Estado is null ? 0 : Estado.Id; } }

        public string? DescripcionEstado { get { return Estado is null ? string.Empty : Estado.Nombre is null ? string.Empty : Estado.Nombre.ToString().Trim(); } }

        public string? NombreProveedor { get { return Proveedor is null ? string.Empty : Proveedor.Nombre is null ? string.Empty : Proveedor.Nombre.ToString().Trim(); } }

        public string? DireccionProveedor { get { return DireccionProveedores is null ? string.Empty : DireccionProveedores.Direccion is null ? string.Empty : DireccionProveedores.Direccion.ToString().Trim(); } }
    }
}
