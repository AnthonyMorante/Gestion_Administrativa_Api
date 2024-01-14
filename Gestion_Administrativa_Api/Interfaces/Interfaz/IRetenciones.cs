using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using System.Text;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Gestion_Administrativa_Api.Interfaces.Utilidades;
using Microsoft.EntityFrameworkCore;
using static Gestion_Administrativa_Api.Utilities.Tools;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IRetenciones
    {

        Task<IActionResult> guardar(RetencionDto? _retencionDto);


    }


    public class RetencionesI : IRetenciones
    {


        private readonly _context _context;
        private readonly IMapper _mapper;
        private readonly IUtilidades _IUtilidades;
        public RetencionesI(_context context, IMapper mapper, IUtilidades iUtilidades)
        {
            _context = context;
            _mapper = mapper;
            _IUtilidades = iUtilidades;
        }


        public async Task<IActionResult> guardar(RetencionDto? _retencionDto)
        {
            try
            {
                var result = new ObjectResult(null);
                var consultaEmpresa = await _context.Empresas.FindAsync(_retencionDto?.idEmpresa);
                var consultaEstablecimiento = await _context.Establecimientos.FindAsync(_retencionDto?.idEstablecimiento);
                if (consultaEmpresa == null) throw new Exception("No se ha encontrado la empresa");
                if (consultaEstablecimiento == null) throw new Exception("No se ha encontrado el establecimiento");
                var retenciones = _mapper.Map<Retenciones>(_retencionDto);
                var claveAcceso = await _IUtilidades.claveAccesoRetencion(retenciones);
                retenciones.ClaveAcceso = claveAcceso;
                retenciones.ObligadoContabilidad = consultaEmpresa.LlevaContabilidad;
                retenciones.EmisorRuc = consultaEmpresa.Identificacion;
                retenciones.DireccionMatriz = consultaEmpresa.DireccionMatriz;
                retenciones.EmisorNombreComercial = consultaEmpresa.RazonSocial;
                retenciones.EmisorRazonSocial = consultaEmpresa.RazonSocial;
                retenciones.PeriodoFiscal = _retencionDto?.fechaEmision!.Value.ToString("MM-yyyy");
                var informacionAdicionales = _mapper.Map<List<InformacionAdicionalRetencion>>(_retencionDto?.infoAdicional);
                var impuestos = _mapper.Map<List<ImpuestoRetenciones>>(_retencionDto?.impuestos);
                retenciones.InformacionAdicionalRetencion = informacionAdicionales;
                retenciones.ImpuestoRetenciones = impuestos;
                await _context.AddAsync(retenciones);
                await _context.SaveChangesAsync();
                result.StatusCode = 200;
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public async Task<IActionResult> guardar(FacturaDto? _facturaDto)
        //{
        //    var result = new ObjectResult("");
        //    try
        //    {
        //        var consultaEmpresa = await _context.Empresas.FindAsync(_facturaDto.idEmpresa);
        //        var consultaEstablecimiento = await _context.Establecimientos.FindAsync(_facturaDto.idEstablecimiento);

        //        if (consultaEmpresa == null) throw new Exception("No se ha encontrado la empresa");
        //        if (consultaEstablecimiento == null) throw new Exception("No se ha encontrado el establecimiento");

        //        var factura = _mapper.Map<Facturas>(_facturaDto);
        //        var detalle = _mapper.Map<List<DetalleFacturas>>(_facturaDto.detalleFactura);
        //        var detallePagos = _mapper.Map<List<DetalleFormaPagos>>(_facturaDto.formaPago);
        //        var detalleAdicional = _mapper.Map<List<InformacionAdicional>>(_facturaDto.informacionAdicional);
        //        factura.EmisorRuc = consultaEmpresa.Identificacion;
        //        var claveAcceso = await _IUtilidades.claveAcceso(factura);
        //        factura.ClaveAcceso = claveAcceso;
        //        factura.TipoEmision = Convert.ToInt16(_configuration["SRI:tipoEmision"]);
        //        factura.Ambiente = Convert.ToInt16(_configuration["SRI:ambiente"]);
        //        factura.Moneda = _configuration["SRI:moneda"];
        //        factura.EmisorRuc = consultaEmpresa.Identificacion;
        //        factura.EmisorRazonSocial = consultaEmpresa.RazonSocial;
        //        factura.RegimenMicroempresas = consultaEmpresa.RegimenMicroEmpresas;
        //        factura.ObligadoContabilidad = consultaEmpresa.LlevaContabilidad;
        //        factura.AgenteRetencion = consultaEmpresa.AgenteRetencion;
        //        factura.RegimenRimpe = consultaEmpresa.RegimenRimpe;
        //        factura.IdTipoEstadoDocumento = 1;
        //        factura.IdTipoEstadoSri = 1;
        //        factura.ExentoIva = 0;
        //        factura.Ice = 0;
        //        factura.Irbpnr = 0;
        //        factura.Isd = 0;
        //        factura.DireccionMatriz = consultaEmpresa.DireccionMatriz;
        //        factura.DireccionEstablecimiento = consultaEstablecimiento.Direccion;
        //        factura.DetalleFormaPagos = detallePagos;
        //        factura.InformacionAdicional = detalleAdicional;
        //        factura.DetalleFacturas = detalle;
        //        factura.FechaAutorizacion = null;
        //        _context.Facturas.Add(factura);
        //        //await _context.SaveChangesAsync();
        //        //await _context.DetalleFacturas.AddRangeAsync(detalle);

        //        if (_facturaDto.idDocumentoEmitir == Guid.Parse("246e7fef-4260-4522-9861-b38c7499ce67"))
        //        {
        //            foreach (var item in detalle)
        //            {
        //                var consultaProducto = await _context.Productos.FindAsync(item.IdProducto);

        //                if (consultaProducto != null)
        //                {
        //                    consultaProducto.Cantidad -= item.Cantidad;
        //                    _context.Productos.Update(consultaProducto);
        //                }
        //            }
        //            var consultaSecuencial = await _context.Secuenciales.FirstOrDefaultAsync(x => x.IdEmpresa == consultaEmpresa.IdEmpresa && x.IdTipoDocumento == _facturaDto.idTipoDocumento);
        //            consultaSecuencial.Nombre = consultaSecuencial.Nombre + 1;
        //            _context.Secuenciales.Update(consultaSecuencial);
        //            await _context.SaveChangesAsync();
        //            var enviadoSri = await enviarSri(factura.ClaveAcceso);
        //            if (enviadoSri == true)
        //            {
        //                string sql = @"UPDATE facturas SET ""idTipoEstadoSri""=6
        //                               WHERE ""claveAcceso"" = @claveAcceso;";
        //                await _dapper.ExecuteScalarAsync(sql, new { claveAcceso = factura.ClaveAcceso });
        //            }
        //        }
        //        result.StatusCode = 200;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.StatusCode = 500;
        //        result.Value = ex.Message;
        //        return result;
        //    }
        //}

    }
}
