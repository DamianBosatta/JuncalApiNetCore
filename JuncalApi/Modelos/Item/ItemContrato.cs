using System;

namespace JuncalApi.Modelos.Item
{
    public class ItemContrato
    {
        public JuncalContrato Contrato { get; set; }

        public JuncalAcerium Aceria { get; set; }

        public ItemContrato() { }

        public ItemContrato(JuncalContrato _contrato , JuncalAcerium _aceria) : this() { Contrato = _contrato; Aceria = _aceria; }
     
        public int Id { get { return (int)(Contrato is null ? 0 : Contrato.Id); } }
        
        public string Nombre { get { return Contrato is null ? string.Empty : Contrato.Nombre is null ? string.Empty : Contrato.Nombre; } }
        
        public string? Numero { get { return Contrato.Numero is null ? string.Empty : Contrato.Numero is null ? string.Empty : Contrato.Numero;} }
        
        public DateTime FechaVigencia { get { return (DateTime)(Contrato is null ? new DateTime() : Contrato.FechaVigencia is null ? new DateTime() : Contrato.FechaVigencia); } }

        public DateTime FechaVencimiento { get { return (DateTime)(Contrato is null ? new DateTime() : Contrato.FechaVencimiento is null ? new DateTime() : Contrato.FechaVencimiento); } }

        public int? IdAceria { get { return Contrato is null ? 0 : Contrato.IdAceria is null ? 0 : Contrato.IdAceria; } }

        public bool Activo { get { return Contrato is null ? false : Contrato.Activo; } }

        public decimal ValorFlete { get { return Contrato is null ? 0 : Contrato.ValorFlete; } }

        public string NombreAceria { get { return Aceria is null ? string.Empty : Aceria.Nombre is null ? string.Empty : Aceria.Nombre; } }
    }
}
