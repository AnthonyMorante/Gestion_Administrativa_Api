using Gestion_Administrativa_Api.Models;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V1;

namespace Gestion_Administrativa_Api.Interfaces.Utilidades
{
    public interface IUtilidades
    {
        Task<string> modulo11(string claveAcceso);
        Task<string> claveAcceso(Facturas? _factura);
    }

    public class UtilidadesI:IUtilidades
    {


        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public UtilidadesI(HttpClient httpClient, IConfiguration configuratio)
        {

            _httpClient = httpClient;
            _configuration = configuratio;

        }


        public async Task <string> modulo11(string claveAcceso)
        {


            try
            {


                char[] ClaveAcceso = claveAcceso.ToCharArray();
                int[] ValoresMultiplicados = new int[claveAcceso.Length];
                var sumaValores = 0;
                var verificador = 0;
                int multiplicando = 1;
                Array.Reverse(ClaveAcceso);

                for (int i = 0; i < ClaveAcceso.Length; i++)
                {
                    multiplicando++;
                    ValoresMultiplicados[i] = Convert.ToInt32(ClaveAcceso[i].ToString()) * multiplicando;
                    if (multiplicando == 7) multiplicando = 1;
                }

                sumaValores = ValoresMultiplicados.Sum();
                verificador = 11 - sumaValores % 11;

                Console.WriteLine(claveAcceso + verificador);

                if (verificador == 10)
                {
                    verificador = 1;
                }

                if (verificador == 11)
                {
                    verificador = 0;
                }
                var claveAccesoFinal = claveAcceso + verificador;
                return claveAccesoFinal;

            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<string> claveAcceso(Facturas ?_factura)
        {


            try
            {

                string fechaFormateada = _factura.FechaEmision?.ToString("ddMMyyyy");
                string tipoDocumento = _factura.TipoDocumento.ToString().PadLeft(2, '0');
                string ruc = _factura.ReceptorRuc;
                int ambiente = Convert.ToInt16 (_configuration["SRI:ambiente"]);
                string establecimiento = _factura.Establecimiento.ToString().PadLeft(3, '0');
                string puntoEmision = _factura.Establecimiento.ToString().PadLeft(3, '0');
                string secuencial = _factura.Establecimiento.ToString().PadLeft(9, '0');
                int numeroRandom = this.numerosRandom();
                int tipoEmision = Convert.ToInt16(_configuration["SRI:tipoEmision"]);
                string claveAcceso = $"{fechaFormateada}{tipoDocumento}{ruc}{ambiente}{establecimiento}{puntoEmision}{secuencial}{numeroRandom}{tipoEmision}";
                string claveAccesoVerificador = await modulo11(claveAcceso);
                return claveAccesoVerificador;

            }
            catch (Exception)
            {

                throw;
            }

        }

        private int numerosRandom()
        {
            try
            {
               
                    Random random = new Random();
                    int randomNumber = 0;

                    for (int i = 0; i < 8; i++)
                    {
                     randomNumber = random.Next(10000000, 99999999);
             

                }

                return randomNumber;
                

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
