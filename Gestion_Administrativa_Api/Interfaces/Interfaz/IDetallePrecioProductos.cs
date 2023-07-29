using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IDetallePrecioProductos
    {
        Task<IEnumerable<DetallePrecioProductos>> listar(Guid idProducto);
    }

    public class DetallePrecioProductosI : IDetallePrecioProductos
    {


        private readonly _context _context;

        public DetallePrecioProductosI(_context context)
        {
            _context = context;
        }



        public async Task<IEnumerable<DetallePrecioProductos>> listar(Guid idProducto)
        {
            try
            {


                return await _context.DetallePrecioProductos.Where(x => x.IdProducto == idProducto /*&& x.Activo*/ == true)
                    .OrderByDescending(x => x.TotalIva)
                    .ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }




    }
}
