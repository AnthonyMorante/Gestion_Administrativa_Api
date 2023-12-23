using System.Xml.Serialization;

namespace GestionAdministrativa_Api.Models.Factura
{
    [XmlRoot(ElementName = "pagos")]
    public class Pagos
    {
        [XmlElement(ElementName = "pago")]
        public Pago Pago { get; set; }
    }
}
