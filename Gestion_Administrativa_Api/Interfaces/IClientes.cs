using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces
{
    public interface IClientes
    {
    }


    public class ClientesI : IClientes
    {


        private readonly _context _context;

        public ClientesI(_context context)
        {
            _context = context;
        }




        public async Task<IEnumerable<Clientes>> listar(bool? activo)
        {
            try
            {


                return await _context.Clientes.ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<string> insertar(Clientes _Clientes)
        {
            try
            {

                _context.Add(_Clientes);
                await _context.SaveChangesAsync();
                return "ok";

            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public async Task<string> editar(Clientes _Clientes)
        {
            try
            {

                var consulta = await _context.Clientes.FindAsync(_Clientes.IdCliente);
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
