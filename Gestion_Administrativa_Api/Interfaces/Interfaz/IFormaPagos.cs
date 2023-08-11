using AutoMapper;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IFormaPagos
    {

        Task<IEnumerable<FormaPagos>> listar();

    }


    public class FormaPagosI : IFormaPagos
    {

        private readonly _context _context;
        private readonly IMapper _mapper;


        public FormaPagosI(_context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        public async Task<IEnumerable<FormaPagos>> listar()
        {
            try
            {


                return await _context.FormaPagos.Where(x => x.Activo == true).OrderBy(x=>x.Nombre).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
