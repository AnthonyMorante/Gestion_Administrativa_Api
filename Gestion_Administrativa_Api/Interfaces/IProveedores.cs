using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces
{
    public interface IProveedores
    {
        Task<string> insertar(Proveedores _proveedores);
        Task<IEnumerable<Proveedores>> listar(bool? activo);
        Task<string> editar(Proveedores _proveedores)
    }


    public class ProveedoresI : IProveedores
    {


        private readonly _context _context;

        public ProveedoresI(_context context)
        {
            _context = context;
        }




        public async Task<IEnumerable<Proveedores>> listar(bool? activo)
        {
            try
            {


                return  await _context.Proveedores.ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<string> insertar(Proveedores _proveedores)
        {
            try
            {

                _context.Add(_proveedores);
                await _context.SaveChangesAsync();
                return "ok";

            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public async Task<string> editar(Proveedores _proveedores)
        {
            try
            {

                var consulta = await _context.Proveedores.FindAsync(_proveedores.IdProveedor);
                _context.Update(consulta);
                await _context.SaveChangesAsync();
                return "ok";

            }
            catch (Exception ex)
            {

                throw;
            }
        }










    }
}