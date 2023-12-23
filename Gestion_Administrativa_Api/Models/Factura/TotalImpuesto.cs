using System.Xml.Serialization;

namespace GestionAdministrativa_Api.Models.Factura
{
    [XmlRoot(ElementName = "totalImpuesto")]
    public class TotalImpuesto
    {
        [XmlElement(ElementName = "codigo")]
        public string Codigo { get; set; }
        [XmlElement(ElementName = "codigoPorcentaje")]
        public string CodigoPorcentaje { get; set; }
        [XmlElement(ElementName = "descuentoAdicional")]
        public string DescuentoAdicional { get; set; }
        [XmlElement(ElementName = "baseImponible")]
        public string BaseImponible { get; set; }
        [XmlElement(ElementName = "valor")]
        public string Valor { get; set; }
    }
}
