using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Models.XML.Factura
{
    [XmlRoot(ElementName = "detalles")]
    public class Detalles
    {
        [XmlElement(ElementName = "detalle")]
        public Detalle Detalle { get; set; }
    }
}
