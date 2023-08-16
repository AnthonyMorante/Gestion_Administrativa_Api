using AutoMapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Utilidades;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IFacturas
    {
        Task<string> guardar(FacturaDto? _facturaDto);
    }

    public class FacturasI:IFacturas
    {


        private readonly _context _context;
        private readonly IMapper _mapper;
        private readonly IUtilidades _IUtilidades;
        private readonly IConfiguration _configuration;


        public FacturasI(_context context, IMapper mapper, IUtilidades iUtilidades, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _IUtilidades = iUtilidades;
            _configuration = configuration;
        }






        public async Task<string> guardar(FacturaDto? _facturaDto)
        {
            try
            {


                var consultaEmpresa = await _context.Empresas.FindAsync(_facturaDto.idEmpresa);
                var consultaEstablecimiento = await _context.Establecimientos.FindAsync(_facturaDto.idEstablecimiento);



                if (consultaEmpresa == null || consultaEstablecimiento==null )
                {

                    return "null";
                }

                var factura = _mapper.Map<Facturas>(_facturaDto);
                var detalle = _mapper.Map<IEnumerable<DetalleFacturas>>(_facturaDto.detalleFactura);

               

               var claveAcceso = await _IUtilidades.claveAcceso(factura);
               factura.ClaveAcceso = claveAcceso;
               factura.TipoEmision = Convert.ToInt16(_configuration["SRI:tipoEmision"]);
               factura.Ambiente = Convert.ToInt16(_configuration["SRI:ambiente"]);
               factura.Moneda = _configuration["SRI:moneda"];
               factura.EmisorRuc = consultaEmpresa.Identificacion;
               factura.EmisorRazonSocial = consultaEmpresa.RazonSocial;
               factura.RegimenMicroempresas = consultaEmpresa.RegimenMicroempresas;
               factura.ObligadoContabilidad = consultaEmpresa.LlevaContabilidad;
               factura.AgenteRetencion = consultaEmpresa.AgenteRetencion;
               factura.RegimenRimpe = consultaEmpresa.RegimenRimpe;
               factura.IdTipoEstadoDocumento = 1;
               factura.ExentoIva = 0;
               factura.Ice = 0;
               factura.Irbpnr = 0;
               factura.Isd = 0;
               factura.DireccionMatriz = consultaEmpresa.DireccionMatriz;
               factura.DireccionEstablecimiento = consultaEstablecimiento.Direccion;
               factura.TipoDocumento = _facturaDto.codigoTipoIdentificacion;
               await _context.Facturas.AddAsync(factura);
               await _context.DetalleFacturas.AddRangeAsync(detalle);
               await _context.SaveChangesAsync();


                if (_facturaDto.formaPago.ToList().Count > 0)
                {
                    var formaPago = _mapper.Map<IEnumerable<DetalleFormaPagos>>(_facturaDto.formaPago);
                    formaPago = formaPago.Select(x =>
                    {x.IdFactura = factura.IdFactura;
                     return x;}).ToList();
                    await _context.DetalleFormaPagos.AddRangeAsync(formaPago);

                }

                if (_facturaDto.informacionAdicional.ToList().Count > 0)
                {
                    var informacionAdicional = _mapper.Map<IEnumerable<InformacionAdicional>>(_facturaDto.informacionAdicional);
                    informacionAdicional = informacionAdicional.Select(x =>
                    {
                        x.IdFactura = factura.IdFactura;
                        return x;
                    }).ToList();
                    await _context.InformacionAdicional.AddRangeAsync(informacionAdicional);

                }

                await _context.SaveChangesAsync();
                return "ok";


            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
