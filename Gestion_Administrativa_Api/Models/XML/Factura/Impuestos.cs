using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Models.XML.Factura
{
    [XmlRoot(ElementName = "impuestos")]
    public class Impuestos
    {
        [XmlElement(ElementName = "impuesto")]
        public Impuesto Impuesto { get; set; }
    }
}
