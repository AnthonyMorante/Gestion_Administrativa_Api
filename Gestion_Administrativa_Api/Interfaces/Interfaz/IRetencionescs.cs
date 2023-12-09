using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using System.Text;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IRetencionescs
    {



    }


    public class RetencionesI : IRetencionescs
    {


        private readonly _context _context;

        public RetencionesI(_context context)
        {
            _context = context;
        }


        public async Task<dynamic> generarXml(RetencionDto ? _retencionDto)
        {
            try
            {
                var factura = new factura_V1_0_0();
                var totalImpuestoList = new List<totalImpuesto_V1_0_0>();
                var totalImpuesto = new totalImpuesto_V1_0_0();
                totalImpuesto.codigo = 2;
                totalImpuesto.codigoPorcentaje = 2;
                totalImpuesto.baseImponible = _factura.Subtotal12;
                totalImpuesto.valor = _factura.Iva12;
                totalImpuestoList.Add(totalImpuesto);
                var infoTributaria = _mapper.Map<infoTributaria_V1_0_0>(_factura);
                var infoFactura = _mapper.Map<infoFactura_V1_0_0>(_factura);
                var detalleFactura = _mapper.Map<List<detalle_V1_0_0>>(_facturaDto.detalleFactura);
                var infoAdicional = _mapper.Map<List<detAdicional_V1_0_0>>(_factura.InformacionAdicional);
                var formaPago = _mapper.Map<List<pago_V1_0_0>>(_factura.DetalleFormaPagos);
                factura.infoTributaria = infoTributaria;
                factura.infoFactura = infoFactura;
                factura.infoFactura.totalConImpuestos = totalImpuestoList;
                factura.infoFactura.pagos = formaPago;
                factura.detalles = detalleFactura;
                factura.infoAdicional = infoAdicional;

                XmlSerializerNamespaces serialize = new XmlSerializerNamespaces();
                serialize.Add("", "");
                XmlSerializer oXmlSerializar = new XmlSerializer(typeof(factura_V1_0_0));
                string xmlFactura = "";
                using (var stream = new System.IO.StringWriter())
                {
                    using (XmlWriter writter = XmlWriter.Create(stream))
                    {


                        oXmlSerializar.Serialize(writter, factura, serialize);

                        xmlFactura = stream.ToString();
                    }
                }
                XDocument doc = XDocument.Parse(xmlFactura);
                doc.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
                var ruta = $"/Facturacion/XML_FIRMADOS/{_facturaDto.idEmpresa}";
                if (!Directory.Exists($"{Tools.rootPath}{ruta}")) Directory.CreateDirectory($"{Tools.rootPath}{ruta}");
                _factura.Ruta = $"{ruta}/{_factura.ClaveAcceso}.xml"; ;
                _factura_V1_0_0 = factura;
                return new { estado = true, documento = doc };


            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
