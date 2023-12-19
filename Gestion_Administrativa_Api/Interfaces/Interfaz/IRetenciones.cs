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

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IRetenciones
    {

        Task<string> generarXml(RetencionDto? _retencionDto);

    }


    public class RetencionesI : IRetenciones
    {


        private readonly _context _context;
        private readonly IMapper _mapper;

        public RetencionesI(_context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IActionResult> guardar(RetencionDto? _retencionDto)
        {
            try
            {
                var result = new ObjectResult(null);
                var consultaEmpresa = await _context.Empresas.FindAsync(_retencionDto.idEmpresa);
                var consultaEstablecimiento = await _context.Establecimientos.FindAsync(_retencionDto.idEstablecimiento);
                if (consultaEmpresa == null) throw new Exception("No se ha encontrado la empresa");
                if (consultaEstablecimiento == null) throw new Exception("No se ha encontrado el establecimiento");
                //var factura = _mapper.Map<Facturas>(_retencionDto);
                //var detalle = _mapper.Map<List<DetalleFacturas>>(_retencionDto.detalleFactura);
                //var detallePagos = _mapper.Map<List<DetalleFormaPagos>>(_retencionDto.formaPago);
                //var detalleAdicional = _mapper.Map<List<InformacionAdicional>>(_retencionDto.informacionAdicional);
                result.StatusCode = 200;
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
