using System.Xml.Serialization;

namespace GestionAdministrativa_Api.Models.Factura
{
    [XmlRoot(ElementName = "totalConImpuestos")]
    public class TotalConImpuestos
    {
        [XmlElement(ElementName = "totalImpuesto")]
        public TotalImpuesto TotalImpuesto { get; set; }
    }
}
