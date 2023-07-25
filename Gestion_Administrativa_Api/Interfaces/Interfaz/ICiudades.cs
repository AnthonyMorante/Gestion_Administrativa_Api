using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface ICiudades
    {
        Task<IEnumerable<Ciudades>> listar(Guid idProvincia);
    }

    public class CiudadesI : ICiudades
    {


        private readonly _context _context;

        public CiudadesI(_context context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Ciudades>> listar(Guid idProvincia)
        {
            try
            {


                return await _context.Ciudades.Where(x => x.Activo == true && x.IdProvincia == idProvincia).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
