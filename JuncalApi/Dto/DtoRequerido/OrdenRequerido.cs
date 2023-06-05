﻿namespace JuncalApi.Dto.DtoRequerido
{
    public class OrdenRequerido
    {

        public int? IdAceria { get; set; }

        public int? IdContrato { get; set; }

        public string? Remito { get; set; }

        public int? IdCamion { get; set; }

        public int? IdEstado { get; set; }

        public DateTime? Fecha { get; set; }

        public int? IdProveedor { get; set; }

        public int? IdAcoplado { get; set; }

        public string? Observaciones { get; set; }

        public int? IdDireccionProveedor { get; set; }


        public OrdenRequerido(int? idAceria, int? idContrato, string? remito, int? idCamion, int? idEstado,
        DateTime? fecha, string? observaciones, int? idDireccionProveedor, int? idProveedor, int? idAcoplado)
        {
            IdAceria = idAceria == 0 ? null : idAceria;
            IdContrato = idContrato == 0 ? null : idContrato;
            Remito = remito is null ? string.Empty : remito;
            IdCamion = idCamion == 0 ? null : idCamion;
            IdEstado = idEstado == 0 ? null : idEstado;
            Fecha = fecha is null ? new DateTime() : fecha;
            Observaciones = observaciones is null ? string.Empty : observaciones;
            IdDireccionProveedor = idDireccionProveedor == 0 ? null : idDireccionProveedor;
            IdProveedor = idProveedor == 0 ? null : idProveedor;
            IdAcoplado = idAcoplado == 0 ? null : idAcoplado;
        }
    }
}
