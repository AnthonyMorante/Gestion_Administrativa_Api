using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public class IpuntoEmisiones
    {

        public interface IPuntoEmisiones
        {

            Task<IEnumerable<PuntoEmisiones>> listar(Guid idEmpresa);
        }


        public class PuntoEmisionesI : IPuntoEmisiones
        {


            private readonly _context _context;

            public PuntoEmisionesI(_context context)
            {
                _context = context;
            }



            public async Task<IEnumerable<PuntoEmisiones>> listar(Guid idEmpresa)
            {
                try
                {


                    return await _context.PuntoEmisiones.Where(x => x.IdEmpresa == idEmpresa && x.Activo == true)
                        .OrderByDescending(x=>x.Predeterminado)
                        .ToListAsync();


                }
                catch (Exception ex)
                {

                    throw;
                }
            }




        }

    }
}
