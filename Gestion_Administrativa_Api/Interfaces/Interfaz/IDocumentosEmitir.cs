using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IDocumentosEmitir
    {
        Task<IEnumerable<DocumentosEmitir>> listar(int codigo);
    }
    public class DocumentosEmitirI : IDocumentosEmitir
    {


        private readonly _context _context;

        public DocumentosEmitirI(_context context)
        {
            _context = context;
        }



        public async Task<IEnumerable<DocumentosEmitir>> listar(int codigo)
        {
            try
            {


                return await _context.DocumentosEmitir
                    .Include(x=>x.IdTipoDocumentoNavigation)
                    .Where(x => x.IdTipoDocumentoNavigation.Codigo == codigo && x.Activo == true)
                    .OrderBy(x => x.Nombre)
                    .ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }




    }

}
