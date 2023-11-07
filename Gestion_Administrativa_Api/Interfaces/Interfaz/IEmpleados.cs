using AutoMapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IEmpleados
    {
        Task<string> insertar(EmpleadosDto _clientes);
        Task<IEnumerable<Empleados>> listar(Guid idEmpresa);
        Task<Empleados> cargar(Guid idCliente);
        Task<string> editar(Empleados _clientes);
        Task<string> eliminar(Guid idCliente);
    }

    public class EmpleadosI : IEmpleados
    {


        private readonly _context _context;
        private readonly IMapper _mapper;


        public EmpleadosI(_context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        public async Task<IEnumerable<Empleados>> listar(Guid idEmpresa)
        {
            try
            {


                return await _context.Empleados.Include(x => x.IdCiudadNavigation).Where(x => x.Activo == true && x.IdEmpresa == idEmpresa).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Empleados> cargar(Guid idCliente)
        {
            try
            {


                return await _context.Empleados.Include(x => x.IdCiudadNavigation).Where(x => x.IdEmpleado == idCliente).FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public async Task<string> insertar(EmpleadosDto _clientes)
        {
            try
            {

                var cliente = _mapper.Map<Empleados>(_clientes);
                cliente.Activo = true;
                cliente.FechaRegistro = DateTime.Now;
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




        public async Task<string> editar(Empleados _clientes)
        {
            try
            {

                var repetido = await _context.Empleados.AnyAsync(x => x.IdEmpleado != _clientes.IdEmpleado && x.Identificacion == _clientes.Identificacion && x.Activo == true);

                if (repetido)
                {
                    return "repetido";
                }


                var consulta = await _context.Empleados.FindAsync(_clientes.IdEmpleado);
                _clientes.IdEmpresa = consulta.IdEmpresa;
                _clientes.FechaRegistro = consulta.FechaRegistro;
                _clientes.Activo = consulta.Activo;
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

        public async Task<bool> comprobarRepetido(Empleados _clientes)
        {
            try
            {

                var consultaRepetido = await _context.Empleados.Where(x => x.Identificacion == _clientes.Identificacion&&x.IdEmpresa== _clientes.IdEmpresa && x.Activo == true).ToListAsync();

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

                var consulta = await _context.Empleados.FindAsync(idCliente);
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
