using System.Xml.Serialization;

namespace GestionAdministrativa_Api.Models.Factura
{
    [XmlRoot(ElementName = "impuestos")]
    public class Impuestos
    {
        [XmlElement(ElementName = "impuesto")]
        public Impuesto Impuesto { get; set; }
    }
}
