using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IFacturas
    {
    }

    public class FacturasI:IFacturas
    {



        private readonly _context _context;

        public FacturasI(_context context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Establecimientos>> listar(Guid idEmpresa)
        {
            try
            {


                return await _context.Establecimientos.Where(x => x.IdEmpresa == idEmpresa && x.Activo == true)
                    .OrderByDescending(x => x.Predeterminado == true)
                    .ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
