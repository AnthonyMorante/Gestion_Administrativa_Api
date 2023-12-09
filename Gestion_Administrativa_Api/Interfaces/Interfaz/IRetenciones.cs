using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using System.Text;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IRetenciones
    {

        Task<string> generarXml(RetencionDto? _retencionDto);

    }


    public class RetencionesI : IRetenciones
    {


        private readonly _context _context;

        public RetencionesI(_context context)
        {
            _context = context;
        }


        public async Task<string> generarXml(RetencionDto? _retencionDto)
        {
            try
            {


                return "retenciones";

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
