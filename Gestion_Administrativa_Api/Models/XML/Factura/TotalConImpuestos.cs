using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Models.XML.Factura
{
    [XmlRoot(ElementName = "totalConImpuestos")]
    public class TotalConImpuestos
    {
        [XmlElement(ElementName = "totalImpuesto")]
        public TotalImpuesto TotalImpuesto { get; set; }
    }
}
