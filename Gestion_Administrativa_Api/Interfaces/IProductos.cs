using AutoMapper;
using Gestion_Administrativa_Api.Dtos;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces
{
    public interface IProductos
    {
        Task<IEnumerable<Productos>> listar(Guid idEmpresa);
        Task<dynamic> cargar(Guid idProducto);
        Task<string> insertar(ProductosDto _productos);
        Task<string> editar(ProductosDto _productos);
        Task<bool> comprobarRepetido(Productos _productos);
        Task<string> eliminar(Guid idProducto);
        Task<bool> desactivar(Guid idProducto, bool activo);

    }

    public class ProductosI : IProductos
    {


        private readonly _context _context;
        private readonly IMapper _mapper;


        public ProductosI(_context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        public async Task<IEnumerable<Productos>> listar(Guid idEmpresa)
        {
            try
            {


                return await _context.Productos.Include(x => x.IdIvaNavigation).Where(x=>x.Activo==true && x.IdEmpresa== idEmpresa).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> cargar(Guid idProducto)
        {
            try
            {


               return new
                {
                    productos = await _context.Productos.Include(x => x.IdIvaNavigation).FirstOrDefaultAsync(x => x.IdProducto == idProducto),
                    detalleprecioProductos = await _context.DetallePrecioProductos.Where(x => x.IdProducto == idProducto).ToListAsync()
                };


            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<bool> desactivar(Guid idProducto, bool activo)
        {
            try
            {

                var consulta = await _context.Productos.FindAsync(idProducto);
                consulta.ActivoProducto = !activo;
                _context.Update(consulta);
                await _context.SaveChangesAsync();
                return !activo;

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<string> insertar(ProductosDto _productos)
        {
            try
            {

                var producto = _mapper.Map<Productos>(_productos);
          

                var repetido = await comprobarRepetido(producto);

                if (repetido == true)
                {

                    return "repetido";

                }
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return "ok";

            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public async Task<string> editar(ProductosDto _productos)
        {
            try
            {

                var repetido = await _context.Productos.AnyAsync(x => x.IdProducto != _productos.IdProducto && x.Codigo == _productos.Codigo && x.Activo == true);

                if (repetido)
                {
                    return "repetido";
                }


                var consulta = await _context.Productos.Include(x=>x.DetallePrecioProductos).AsNoTracking().FirstOrDefaultAsync(x=>x.IdProducto == _productos.IdProducto);
                var idNuevos = _productos.DetallePrecioProductos.Select(d => d.IdDetallePrecioProducto).ToList();

                foreach (var detalle in consulta.DetallePrecioProductos.ToList())
                {
                    if (!idNuevos.Contains(detalle.IdDetallePrecioProducto))
                    {
                        consulta.DetallePrecioProductos.Remove(detalle);
                        _context.Entry(detalle).State = EntityState.Deleted;
                 
                    }

                }
                var map = _mapper.Map(_productos, consulta);
                _mapper.Map(_productos, consulta);
                _context.Update(consulta);
                await _context.SaveChangesAsync();
                return "ok";

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> comprobarRepetido(Productos _productos)
        {
            try
            {

                var consultaRepetido = await _context.Productos.Where(x => x.Codigo == _productos.Codigo && x.Activo == true).ToListAsync();

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



        public async Task<string> eliminar(Guid idProducto)
        {
            try
            {

                var consulta = await _context.Productos.FindAsync(idProducto);
                consulta.Activo = false;
                consulta.ActivoProducto = false;
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
