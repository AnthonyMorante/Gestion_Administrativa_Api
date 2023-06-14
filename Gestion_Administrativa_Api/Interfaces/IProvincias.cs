using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces
{
    public interface IProvincias
    {
        Task<IEnumerable<Provincias>> listar();

    }

    public class ProvinciasI : IProvincias
    {


        private readonly _context _context;

        public ProvinciasI(_context context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Provincias>> listar()
        {
            try
            {


                return await _context.Provincias.Where(x => x.Activo == true).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }

}
