using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Models.XML.Factura
{
    [XmlRoot(ElementName = "pagos")]
    public class Pagos
    {
        [XmlElement(ElementName = "pago")]
        public Pago Pago { get; set; }
    }
}
