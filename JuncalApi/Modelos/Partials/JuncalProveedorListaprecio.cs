using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace JuncalApi.Modelos;
public partial class JuncalProveedorListaprecio
{
    [NotMapped]
    public string NombreProveedor {  get; set; }= string.Empty;
    [NotMapped]
    public string NombreUsuario {  get; set; }= string.Empty;   


    public JuncalProveedorListaprecio()
    {


    }

    public JuncalProveedorListaprecio(int _id , int _idProveedor,DateTime _FechaVigencia,DateTime _FechaVencimiento,int _idUsuario,bool _Activo) : this()
    {
        Id = _id ;
        IdProveedor= _idProveedor ;
        FechaVigencia = _FechaVigencia;
        FechaVencimiento = _FechaVencimiento;
        IdUsuario= _idUsuario ;
        Activo= _Activo ;
    }


    public JuncalProveedorListaprecio(int _id, int _idProveedor, DateTime _FechaVigencia, DateTime _FechaVencimiento, int _idUsuario, bool _Activo,string _nombreProveedor,string _nombreUsuario) 
     :this( _id,_idProveedor,_FechaVigencia,_FechaVencimiento,_idUsuario, _Activo)
    {
       NombreProveedor = _nombreProveedor ;
       NombreUsuario= _nombreUsuario ;

    }


}
