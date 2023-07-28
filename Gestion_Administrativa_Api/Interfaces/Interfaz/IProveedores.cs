using AutoMapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IProveedores
    {
        Task<string> insertar(ProveedoresDto _clientes);
        Task<IEnumerable<Proveedores>> listar(Guid idEmpresa);
        Task<Proveedores> cargar(Guid idCliente);
        Task<string> editar(Proveedores _clientes);
        Task<string> eliminar(Guid idCliente);
    }


    public class ProveedoresI : IProveedores
    {




        private readonly _context _context;
        private readonly IMapper _mapper;


        public ProveedoresI(_context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        public async Task<IEnumerable<Proveedores>> listar(Guid idEmpresa)
        {
            try
            {


                return await _context.Proveedores.Include(x => x.IdCiudadNavigation).Where(x => x.Activo == true).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Proveedores> cargar(Guid idCliente)
        {
            try
            {


                return await _context.Proveedores.Include(x => x.IdCiudadNavigation).Where(x => x.IdProveedor == idCliente).FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public async Task<string> insertar(ProveedoresDto _clientes)
        {
            try
            {

                var cliente = _mapper.Map<Proveedores>(_clientes);

                var repetido = await comprobarRepetido(cliente);

                if (repetido == true)
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




        public async Task<string> editar(Proveedores _clientes)
        {
            try
            {

                var repetido = await _context.Proveedores.AnyAsync(x => x.IdProveedor != _clientes.IdProveedor && x.Identificacion == _clientes.Identificacion && x.Activo == true);

                if (repetido)
                {
                    return "repetido";
                }


                var consulta = await _context.Proveedores.FindAsync(_clientes.IdProveedor);
                var map = _mapper.Map(_clientes, consulta);
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

        public async Task<bool> comprobarRepetido(Proveedores _clientes)
        {
            try
            {

                var consultaRepetido = await _context.Proveedores.Where(x => x.Identificacion == _clientes.Identificacion && x.IdEmpresa == _clientes.IdEmpresa && x.Activo == true).ToListAsync();

                if (consultaRepetido.Count > 0)
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

                var consulta = await _context.Proveedores.FindAsync(idCliente);
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