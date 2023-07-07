namespace JuncalApi.Modelos.Codigos_Utiles
{
    public static class Codigos
    {
        #region BASE DE DATO
        
        public static string NombreBaseDeDato = "JuncalApiDB";

        public static string KeyBaseDeDato = "appsettings.json";

        #endregion

        #region IDENTIFICADORES DE LOS ESTADOS DE ORDEN

        public static int Enviado = 1;

        public static int Facturado = 2;

        public static int FacYReclamado = 3;

        public static int Cobrado = 4;

        #endregion

        #region IDENTIFICADORES DE RECLAMOS

        public static int PendienteReclamo = 1;

        public static int Reclamado = 2 ;

        public static int Finalizado = 3;

        #endregion

    }
}
