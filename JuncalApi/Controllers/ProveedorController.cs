﻿using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ProveedorController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorRespuesta>>> GetProveedores()
        {

            var ListaProveedores = _uow.RepositorioJuncalProveedor.GetAllByCondition(c => c.Isdeleted == false).ToList();

            if (ListaProveedores.Count() > 0)
            {
                List<ProveedorRespuesta> listaProveedorRespuesta = _mapper.Map<List<ProveedorRespuesta>>(ListaProveedores);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaProveedorRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ProveedorRespuesta>() == null });


        }
        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdProveedor(int id)
        {
            var proveedor = _uow.RepositorioJuncalProveedor.GetById(id);

            if (proveedor is null || proveedor.Isdeleted == true)
            {
                return Ok(new { success = false, message = "No Se Encontro El Proveedor", result = new ProveedorRespuesta() == null });
            }
            
            ProveedorRespuesta proveedorRes = new ProveedorRespuesta();

            _mapper.Map(proveedor, proveedorRes);

            return Ok(new { success = true, message = "Proveedor Encontrado", result = proveedorRes });



        }


        [HttpPost]
        public ActionResult CargarProveedor([FromBody] ProveedorRequerido proveedorReq)
        {
            var proveedor = _uow.RepositorioJuncalProveedor.GetByCondition(c => c.Nombre.Equals(proveedorReq.Nombre) && c.Isdeleted == false);

            if (proveedor is null)
            {
                JuncalProveedor proveedorNuevo = _mapper.Map<JuncalProveedor>(proveedorReq);

                _uow.RepositorioJuncalProveedor.Insert(proveedorNuevo);
                ProveedorRespuesta proveedorRes = new ProveedorRespuesta();
                _mapper.Map(proveedorNuevo, proveedorRes);
                return Ok(new { success = true, message = "El Proveedor Fue Creado Con Exito", result = proveedorRes });
            }
           
            ProveedorRespuesta proveedorExiste = new ProveedorRespuesta();
            _mapper.Map(proveedor, proveedorExiste);
            return Ok(new { success = false, message = " El Proveedor Ya Existe ", result = proveedorExiste });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedProveedor(int id)
        {

            var proveedor = _uow.RepositorioJuncalProveedor.GetById(id);
            if (proveedor != null && proveedor.Isdeleted == false)
            {
                proveedor.Isdeleted = true;
                _uow.RepositorioJuncalProveedor.Update(proveedor);
                ProveedorRespuesta proveedorRes = new ProveedorRespuesta();
                _mapper.Map(proveedor, proveedorRes);

                return Ok(new { success = true, message = "El Proveedor Fue Eliminado ", result = proveedorRes });


            }


            return Ok(new { success = false, message = " El Proveedor No Fue Encontrado ", result = new ProveedorRespuesta() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProveedor(int id, ProveedorRequerido proveedorEdit)
        {
            var proveedor = _uow.RepositorioJuncalProveedor.GetById(id);

            if (proveedor != null && proveedor.Isdeleted == false)
            {
                _mapper.Map(proveedorEdit, proveedor);
                _uow.RepositorioJuncalProveedor.Update(proveedor);
                ProveedorRespuesta proveedorRes = new ProveedorRespuesta();
                _mapper.Map(proveedor, proveedorRes);
                return Ok(new { success = true, message = "El Proveedor Fue Actualizado", result = proveedorRes });
            }

            return Ok(new { success = false, message = "El Proveedor No Fue Encontrado ", result = new ProveedorRespuesta() == null });


        }





    }
}
