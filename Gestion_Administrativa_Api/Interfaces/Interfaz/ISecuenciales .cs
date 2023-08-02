using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;
using static Gestion_Administrativa_Api.Interfaces.Interfaz.IpuntoEmisiones;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface ISecuenciales
    {
        Task<IEnumerable<Secuenciales>> listar(Guid idEmpresa);
    }


    public class SecuencialesI : ISecuenciales
    {


        private readonly _context _context;

        public SecuencialesI(_context context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Secuenciales>> listar(Guid idEmpresa)
        {
            try
            {


                return await _context.Secuenciales.Where(x => x.IdEmpresa == idEmpresa && x.Activo == true)
                    .ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }




    }
}
