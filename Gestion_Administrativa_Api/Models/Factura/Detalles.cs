using System.Xml.Serialization;

namespace GestionAdministrativa_Api.Models.Factura
{
    [XmlRoot(ElementName = "detalles")]
    public class Detalles
    {
        [XmlElement(ElementName = "detalle")]
        public Detalle Detalle { get; set; }
    }
}
