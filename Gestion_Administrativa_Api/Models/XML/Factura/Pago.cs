using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Models.XML.Factura
{
    [XmlRoot(ElementName = "pago")]
    public class Pago
    {
        [XmlElement(ElementName = "formaPago")]
        public string FormaPago { get; set; }
        [XmlElement(ElementName = "total")]
        public string Total { get; set; }
        [XmlElement(ElementName = "plazo")]
        public string Plazo { get; set; }
        [XmlElement(ElementName = "unidadTiempo")]
        public string UnidadTiempo { get; set; }
    }
}
