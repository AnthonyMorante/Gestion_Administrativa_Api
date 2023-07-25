using Gestion_Administrativa_Api.Dtos.Sri;
using Gestion_Administrativa_Api.Models;

namespace Gestion_Administrativa_Api.Interfaces.Sri
{
    public interface IDatosContribuyentes
    {

        Task<DatosContribuyentesDto> consultarContribuyente(string identificacion);
    }

    public class DatosContribuyentesI:IDatosContribuyentes
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public DatosContribuyentesI(HttpClient httpClient, IConfiguration configuratio)
        {

            _httpClient = httpClient;
            _configuration = configuratio;

        }



        public async Task<DatosContribuyentesDto> consultarContribuyente(string identificacion)
        {
            try
            {

                string url = $"{_configuration["SRI:urlConsultaRuc"]}{identificacion}";
                var consulta = await _httpClient.GetAsync(url);
                dynamic result = await consulta.Content.ReadFromJsonAsync<dynamic>();
                dynamic consultaData = result.GetProperty("contribuyente");
                DatosContribuyentesDto contribuyente = System.Text.Json.JsonSerializer.Deserialize<DatosContribuyentesDto>(consultaData.ToString());
                return contribuyente;




            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
