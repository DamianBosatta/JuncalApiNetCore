namespace JuncalApi.Modelos.Codigos_Utiles
{
    public static class CodigosUtiles
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

        #region TIPO MOVIMIENTO CUENTA CORRIENTE

        public static int Adelanto = 1;
        public static int Conciliacion = 2;
        public static int Remito = 3;
        public static int Ajuste = 4;


        #endregion

        #region ROLES

        public static int Administrador = 1;
        public static int Usuario = 2;

        #endregion

        #region TIPO ACOPLADO
        public static int RoolOff = 1;
        public static int Batea = 2;
        public static int SemiChatarrero = 3;
        public static int BarandaVolcable = 4;
        public static int Carreton = 5;



        #endregion

        #region TIPO CAMION
       
        public static int Tractor = 1;
        public static int Chasis = 2;

        #endregion

        #region ESTADO INTERNO
        public static int EnviadoInterno = 1;

        public static int RecibidoInterno = 2;


        #endregion

    }
}
