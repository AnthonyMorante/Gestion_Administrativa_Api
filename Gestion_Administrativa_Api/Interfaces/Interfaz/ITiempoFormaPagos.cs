using AutoMapper;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface ITiempoFormaPagos
    {

        Task<IEnumerable<TiempoFormaPagos>> listar();

    }


    public class TiempoFormaPagosI : ITiempoFormaPagos
    {

        private readonly _context _context;
        private readonly IMapper _mapper;


        public TiempoFormaPagosI(_context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        public async Task<IEnumerable<TiempoFormaPagos>> listar()
        {
            try
            {


                return await _context.TiempoFormaPagos.Where(x => x.Activo == true).OrderBy(x => x.Nombre).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
