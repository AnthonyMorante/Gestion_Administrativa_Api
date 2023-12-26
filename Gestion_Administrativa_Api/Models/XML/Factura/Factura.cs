using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Models.XML.Factura
{
    [XmlRoot(ElementName = "factura")]
    public class Factura
    {

        [XmlElement(ElementName = "infoTributaria")]
        public InfoTributaria InfoTributaria { get; set; }
        [XmlElement(ElementName = "infoFactura")]
        public InfoFactura InfoFactura { get; set; }
        [XmlElement(ElementName = "detalles")]
        public List<Detalles> Detalles { get; set; }
        [XmlElement(ElementName = "infoAdicional")]
        public InfoAdicional InfoAdicional { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }
}
