using AutoMapper;
using Gestion_Administrativa_Api.Dtos;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces
{
    public interface IClientes
    {

        Task<string> insertar(ClientesDto _clientes);
        Task<IEnumerable<Clientes>> listar();
        Task<Clientes> cargar(Guid idCliente);
        Task<string> editar(Clientes _clientes);
        Task<string> eliminar(Guid idCliente);
    }


    public class ClientesI : IClientes
    {


        private readonly _context _context;
             private readonly IMapper _mapper;
  

        public ClientesI(_context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }




        public async Task<IEnumerable<Clientes>> listar()
        {
            try
            {


                return await _context.Clientes.Include(x=>x.IdCiudadNavigation).Where(x=>x.Activo==true).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Clientes> cargar(Guid idCliente)
        {
            try
            {


                return await _context.Clientes.Include(x => x.IdCiudadNavigation).Where(x => x.IdCliente== idCliente).FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public async Task<string> insertar(ClientesDto _clientes)
        {
            try
            {

                var cliente = _mapper.Map<Clientes>(_clientes);

                var repetido = await comprobarRepetido(cliente);

                if(repetido==true)
                {

                    return "repetido";

                }
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return "ok";

            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public async Task<string> editar(Clientes _clientes)
        {
            try
            {

                var repetido = await _context.Clientes.AnyAsync(x => x.IdCliente != _clientes.IdCliente && x.Identificacion == _clientes.Identificacion && x.Activo==true);
                
                if (repetido)
                {
                    return "repetido";
                }


                var consulta = await _context.Clientes.FindAsync(_clientes.IdCliente);
                var map =  _mapper.Map(_clientes,consulta);
                _mapper.Map(_clientes, consulta);
                _context.Update(consulta);
                await _context.SaveChangesAsync();
                return "ok";

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> comprobarRepetido(Clientes _clientes)
        {
            try
            {

                var consultaRepetido = await _context.Clientes.Where(x => x.Identificacion == _clientes.Identificacion && x.Activo == true).ToListAsync();
              
                if(consultaRepetido.Count > 0)
                {
                    return true;
                }

                return false;

         

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<string> eliminar(Guid idCliente)
        {
            try
            {

                var consulta = await _context.Clientes.FindAsync(idCliente);
                consulta.Activo = false;
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
