using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Models.XML.Factura
{
    [XmlRoot(ElementName = "infoAdicional")]
    public class InfoAdicional
    {
        [XmlElement(ElementName = "campoAdicional")]
        public List<CampoAdicional> CampoAdicional { get; set; }
    }
}
