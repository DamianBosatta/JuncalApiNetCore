using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Servicios.Facturar;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorCuentaCorrienteController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ProveedorCuentaCorrienteController> _logger; 
        private readonly IFacturarServicio _facturarServicio;

        public ProveedorCuentaCorrienteController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ProveedorCuentaCorrienteController> logger,IFacturarServicio facturarServicio)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _facturarServicio= facturarServicio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorCuentaCorrienteRespuesta>>> GetProveedorCcMovimiento(bool adelantoMaterial) // Parametro Para get de cuenta corriente Material o Dinero
        {
            try
            {
                var ListaProveedorCcMovimiento =  _uow.RepositorioJuncalProveedorCuentaCorriente.GetProveedorCuentasCorrientes(0,adelantoMaterial);



                if (ListaProveedorCcMovimiento.Any())
                {
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaProveedorCcMovimiento });
                }

                return Ok(new { success = false, message = "La Lista Está Vacia", result = new List<ProveedorCuentaCorrienteRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetProveedorCcMovimiento");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("Proveedor/{idProveedor?}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorCuentaCorrienteRespuesta>>> GetProveedorCcMovimientoForIdProveedor(int idProveedor,bool esMaterial)
        {
            try
            {
                var ListaProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetProveedorCuentasCorrientes(idProveedor, esMaterial);



                if (ListaProveedorCcMovimiento.Any())
                {
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaProveedorCcMovimiento });
                }

                return Ok(new { success = false, message = "La Lista Está Vacia", result = new List<ProveedorCuentaCorrienteRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetProveedorCcMovimientoForIdProveedor");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdProveedorCcMovimiento(int id)
        {
            try
            {
                var ProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetById(id);

                if (ProveedorCcMovimiento is null)
                {
                    return Ok(new { success = false, message = "No Se Encontró La C.C Del Proveedor", result = new ProveedorCuentaCorrienteRespuesta() });
                }

                var ProveedorCcMovimientoRespuesta = _mapper.Map<ProveedorCuentaCorrienteRespuesta>(ProveedorCcMovimiento);

                return Ok(new { success = true, message = "C.C Proveedor Encontrada", result = ProveedorCcMovimientoRespuesta });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetByIdProveedorCcMovimiento");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public ActionResult CargarProveedorCcMovimiento([FromBody] ProveedorCuentaCorrienteRequerido ProveedorCcMovimientoReq)
        {
            try
            {

                var ProveedorCuentaCorriente = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCcMovimientoReq);

                if(ProveedorCuentaCorriente.MaterialBool== true)
                {
                    var ultimoMov = _uow.RepositorioJuncalProveedorCuentaCorriente.GetAllByCondition(x => x.IdProveedor == ProveedorCuentaCorriente.IdProveedor && x.MaterialBool == true && x.IdMaterial == ProveedorCuentaCorriente.IdMaterial)
                                      .OrderByDescending(x => x.Id)
                                      .FirstOrDefault();

                    if(ultimoMov!=null)
                    {
                        ProveedorCuentaCorriente.Total = ultimoMov.Total + ProveedorCuentaCorriente.Peso;
                        _uow.RepositorioJuncalProveedorCuentaCorriente.Insert(ProveedorCuentaCorriente);

                        var ProveedorCuentaCorrienteRes = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCuentaCorriente);
                        return Ok(new { success = true, message = "El movimientos de Proveedor Fue Creada Con Exito", result = ProveedorCuentaCorrienteRes });
                    }
                    else
                    {
                        ProveedorCuentaCorriente.Total = ProveedorCuentaCorriente.Peso;
                        _uow.RepositorioJuncalProveedorCuentaCorriente.Insert(ProveedorCuentaCorriente);

                        var ProveedorCuentaCorrienteRes = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCuentaCorriente);

                        return Ok(new { success = true, message = "El movimientos de Proveedor Fue Creada Con Exito", result = ProveedorCuentaCorrienteRes });
                    }


                }
                else
                {
                    var ultimoMov = _uow.RepositorioJuncalProveedorCuentaCorriente.GetAllByCondition(x => x.IdProveedor == ProveedorCuentaCorriente.IdProveedor && x.MaterialBool == false)
                                      .OrderByDescending(x => x.Id)
                                      .FirstOrDefault();

                    if(ultimoMov != null)
                    {
                        ProveedorCuentaCorriente.Total = ultimoMov.Total + ProveedorCuentaCorriente.Importe;
                        _uow.RepositorioJuncalProveedorCuentaCorriente.Insert(ProveedorCuentaCorriente);

                        var ProveedorCuentaCorrienteRes = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCuentaCorriente);
                        return Ok(new { success = true, message = "El movimientos de Proveedor Fue Creada Con Exito", result = ProveedorCuentaCorrienteRes });
                    }
                    else
                    {
                        ProveedorCuentaCorriente.Total = ProveedorCuentaCorriente.Importe;
                     _uow.RepositorioJuncalProveedorCuentaCorriente.Insert(ProveedorCuentaCorriente);

                        var ProveedorCuentaCorrienteRes = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCuentaCorriente);
                        return Ok(new { success = true, message = "El movimientos de Proveedor Fue Creada Con Exito", result = ProveedorCuentaCorrienteRes });
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CargarProveedorCcMovimiento");
                return StatusCode(500, "Error interno del servidor");
            }

        }
        [Route("Facturar/")]
        [HttpPost]
        public ActionResult FacturarOrdenProveedor([FromBody] FacturarOrdenRequerido ordenRequerida )
        {
            try
            {
                //Obtenemos el pendiente
                var pendiente = _uow.RepositorioJuncalCuentaCorrientePendiente.GetByCondition(x => x.Id == ordenRequerida.idPendiente);
                if (pendiente != null)
                {
                    // obtenemos la cuenta corriente
                    var cuentaCorriente = _facturarServicio.FacturarRemitoExterno(ordenRequerida);
                    if(cuentaCorriente == null)
                    {
                        pendiente.Pendiente = false;
                        var respuestaPendiente = _uow.RepositorioJuncalCuentaCorrientePendiente.Update(pendiente);
                        if (respuestaPendiente)
                        {
                            return Ok(new { success = true, message = "Se actualizo la cuenta materiales del Proveedor", result = true });
                        }
                        
                    }else
                    {
                        pendiente.Pendiente = false;
                        var respuestaPendiente = _uow.RepositorioJuncalCuentaCorrientePendiente.Update(pendiente);
                        var respuestaProveedorCC = _uow.RepositorioJuncalProveedorCuentaCorriente.Insert(cuentaCorriente);
                        if(respuestaPendiente && respuestaProveedorCC)
                        {
                            return Ok(new { success = true, message = "Se actualizo la cuenta materiales y y cuenta corriente del Proveedor ", result = true });
                        }
                        
                    }
                    
                    
                }

            return NotFound("No se puedo encontrar cuenta corriente con la orden recibida");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en insertar orden en cuenta corriente proveedor");
                return StatusCode(500, "Error interno del servidor");
            }
        }
        

            [HttpPut("{id}")]
        public async Task<IActionResult> EditProveedorCcMovimiento(int id, ProveedorCuentaCorrienteRequerido ProveedorCcMovimientoReq)
        {
            try
            {
                var ProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetById(id);

                if (ProveedorCcMovimiento != null)
                {
                    ProveedorCcMovimiento = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCcMovimientoReq);
                    _uow.RepositorioJuncalProveedorCuentaCorriente.Update(ProveedorCcMovimiento);

                    var ProveedorCcMovimientoRes = _mapper.Map<ProveedorCuentaCorrienteRespuesta>(ProveedorCcMovimiento);

                    return Ok(new { success = true, message = "La CC Del Proveedor Ha Sido Actualizado", result = ProveedorCcMovimientoRes });
                }

                return Ok(new { success = false, message = "No Se Encontró La CC Del Proveedor", result = new ProveedorCuentaCorrienteRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en EditProveedorCcMovimiento");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
