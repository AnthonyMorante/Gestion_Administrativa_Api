using System.Xml.Serialization;

namespace GestionAdministrativa_Api.Models.Factura
{
    [XmlRoot(ElementName = "campoAdicional")]
    public class CampoAdicional
    {
        [XmlAttribute(AttributeName = "nombre")]
        public string Nombre { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}
