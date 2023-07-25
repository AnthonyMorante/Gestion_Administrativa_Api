using System.Text.Json.Serialization;

namespace Gestion_Administrativa_Api.Dtos.Sri
{
    public class DatosContribuyentesDto
    {
        [JsonPropertyName("identificacion")]
        public string? Identificacion { get; set; }

        [JsonPropertyName("nombreComercial")]
        public string? NombreComercial { get; set; }

    }
}
