using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public class IpuntoEmisiones
    {

        public interface IPuntoEmisiones
        {

            Task<IEnumerable<PuntoEmisiones>> listar(PuntoEmisiones _PuntoEmisiones);
        }


        public class PuntoEmisionesI : IPuntoEmisiones
        {


            private readonly _context _context;

            public PuntoEmisionesI(_context context)
            {
                _context = context;
            }



            public async Task<IEnumerable<PuntoEmisiones>> listar(PuntoEmisiones _PuntoEmisiones)
            {
                try
                {


                    return await _context.PuntoEmisiones.Where(x => x.IdEmpresa == _PuntoEmisiones.IdEmpresa && x.Activo == true).ToListAsync();


                }
                catch (Exception ex)
                {

                    throw;
                }
            }




        }

    }
}
