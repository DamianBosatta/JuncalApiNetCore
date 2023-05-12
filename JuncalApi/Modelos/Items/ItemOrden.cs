namespace JuncalApi.Modelos.Items
{
    public class ItemOrden
    {
        public JuncalOrden Orden { get; set; }
        public JuncalAcerium Aceria { get; set; }
        public JuncalContrato Contrato { get; set; }
        public JuncalCamion Camion { get; set; }
        public JuncalChofer Chofer { get; set; }
        public JuncalTransportistum Transportista { get; set; }
        public JuncalAcoplado Acoplado { get; set; }
        public JuncalEstado Estado { get; set; }
        public JuncalProveedor Proveedor { get; set; }
        public JuncalDireccionProveedor DireccionProveedor { get; set; }

        public ItemOrden()
        {

        }

        public ItemOrden(JuncalOrden pOrden,JuncalAcerium pAceria,JuncalContrato pContrato,JuncalCamion pCamion,
        JuncalChofer pChofer,JuncalTransportistum pTransportista,JuncalAcoplado pAcoplado, JuncalEstado pEstado,
        JuncalProveedor pProveedor,JuncalDireccionProveedor pDireccionProveedor)
        {
            this.Orden = pOrden;
            this.Aceria = pAceria;
            this.Contrato = pContrato;
            this.Camion = pCamion;
            this.Chofer = pChofer;
            this.Transportista = pTransportista;
            this.Acoplado = pAcoplado;
            this.Estado = pEstado;
            this.Proveedor = pProveedor;
            this.DireccionProveedor = pDireccionProveedor;

        }
      

        public int IdOrden { get { return Orden is null ? 0 : Orden.Id; } }

        public string Remito { get { return Orden is null ? string.Empty : Orden.Remito.ToString().Trim(); } }

        public DateTime FechaRemito { get { return Orden is null ? new DateTime() : Orden.Fecha; } }

        public string Observacion { get { return Orden is null ? string.Empty : Orden.Observaciones.ToString().Trim(); } }

        public string NombreAceria { get { return Aceria is null ? string.Empty : Aceria.Nombre.ToString().Trim(); } }

        public string DireccionAceria { get { return Aceria is null ? string.Empty : Aceria.Direccion.ToString().Trim(); } }

        public string CuitAceria { get { return Aceria is null ? string.Empty : Aceria.Cuit.ToString().Trim(); } }

        public string CodigoProveedor { get { return Aceria is null ? string.Empty : Aceria.CodProveedor.ToString().Trim(); } }

        public string NumeroContrato { get { return Contrato is null ? string.Empty : Contrato.Numero.ToString().Trim(); } }

        public string PatenteCamion { get { return Camion is null ? string.Empty : Camion.Patente.ToString().Trim(); } }

        public string NombreChofer { get { return Chofer is null ? string.Empty : Chofer.Nombre.ToString().Trim(); } }

        public string LicenciaChofer { get { return Chofer is null ? string.Empty : Chofer.Dni.ToString(); } }

        public string NombreTransportista { get { return Transportista is null ? string.Empty : Transportista.Nombre.ToString().Trim(); } }

        public string PatenteAcoplado { get { return Acoplado is null ? string.Empty : Acoplado.Patente.ToString().Trim(); } }

        public int IdEstado { get { return Estado is null ? 0 : Estado.Id; } }

        public string NombreEstado { get { return Estado is null ? string.Empty : Estado.Nombre.ToString().Trim(); } }

        public string NombreProveedor { get { return Proveedor is null ? string.Empty : Proveedor.Nombre.ToString().Trim(); } }

        public string ProveedorDireccion { get { return DireccionProveedor is null ? string.Empty : DireccionProveedor.Direccion.ToString().Trim(); } }






    }
}
