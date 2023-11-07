﻿using AutoMapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IClientes
    {
        Task<string> insertar(ClientesDto _clientes);

        Task<IEnumerable<Clientes>> listar(Guid idEmpresa);

        Task<Clientes> cargar(Guid idCliente);

        Task<Clientes> cargarPorIdentificacion(string identificacion);

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

        public async Task<IEnumerable<Clientes>> listar(Guid idEmpresa)
        {
            try
            {
                return await _context.Clientes.Include(x => x.IdCiudadNavigation).Where(x => x.Activo == true && x.IdEmpresa == idEmpresa).ToListAsync();
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
                return await _context.Clientes.Include(x => x.IdCiudadNavigation).Where(x => x.IdCliente == idCliente).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Clientes> cargarPorIdentificacion(string identificacion)
        {
            try
            {
                return await _context.Clientes.Include(x => x.IdCiudadNavigation).Include(x => x.IdTipoIdentificacionNavigation).Where(x => x.Identificacion == identificacion).FirstOrDefaultAsync();
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

        public async Task<string> editar(Clientes _clientes)
        {
            try
            {
                var repetido = await _context.Clientes.AnyAsync(x => x.IdCliente != _clientes.IdCliente && x.Identificacion == _clientes.Identificacion && x.Activo == true);

                if (repetido)
                {
                    return "repetido";
                }

                var consulta = await _context.Clientes.FindAsync(_clientes.IdCliente);
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

        public async Task<bool> comprobarRepetido(Clientes _clientes)
        {
            try
            {
                var consultaRepetido = await _context.Clientes.Where(x => x.Identificacion == _clientes.Identificacion && x.IdEmpresa == _clientes.IdEmpresa && x.Activo == true).ToListAsync();

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