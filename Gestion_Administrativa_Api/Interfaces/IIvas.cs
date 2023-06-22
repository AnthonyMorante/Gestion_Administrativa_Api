using AutoMapper;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces
{
    public interface IIvas
    {

        Task<IEnumerable<Ivas>> listar();
    }

    public class IvasI : IIvas
    {

        private readonly _context _context;
        private readonly IMapper _mapper;


        public IvasI(_context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        public async Task<IEnumerable<Ivas>> listar()
        {
            try
            {


                return await _context.Ivas.Where(x=>x.Activo==true).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
