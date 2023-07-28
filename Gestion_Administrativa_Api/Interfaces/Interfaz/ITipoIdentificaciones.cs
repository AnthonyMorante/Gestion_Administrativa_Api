using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface ITipoIdentificaciones
    {

        Task<IEnumerable<TipoIdentificaciones>> listar();
    }

    public class TipoIdentificacionesI : ITipoIdentificaciones
    {


        private readonly _context _context;

        public TipoIdentificacionesI(_context context)
        {
            _context = context;
        }



        public async Task<IEnumerable<TipoIdentificaciones>> listar()
        {
            try
            {


                return await _context.TipoIdentificaciones.Where(x => x.Activo == true)
                    .OrderBy(x=>x.Activo==true)
                    .ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }




    }
}
