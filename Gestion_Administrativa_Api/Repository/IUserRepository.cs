using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Repository
{
    public interface IUserRepository
    {

        Task<UsuarioEmpresas> listarUsuario(string identificacion);

        Task<UsuarioEmpresas> ExisteUsuario(string usuario, string clave);

        class UserRepository : IUserRepository
        {

            private readonly _context _context;

            public UserRepository(_context context)
            {
                _context = context;

            }




            public async Task<UsuarioEmpresas> listarUsuario(string username)
            {
                try
                {

                    var consulta = await _context.UsuarioEmpresas
                        .Include(x=>x.IdEmpresaNavigation)
                        .Include(x=>x.IdUsuarioNavigation)
                        .Where(x => x.IdUsuarioNavigation.Nombre.Equals(username)).FirstAsync();


                    if (consulta != null)
                    {

                        return consulta;

                    }
                    return consulta;


                }
                catch (Exception)
                {

                    throw;
                }
            }

            public async Task<UsuarioEmpresas> ExisteUsuario(string name, string password)
            {
                try
                {

                    var consulta = await _context.UsuarioEmpresas
                        .Include(x=>x.IdUsuarioNavigation)
                        .Include(x=>x.IdEmpresaNavigation)
                        .Where(x => x.IdUsuarioNavigation.Nombre.Equals(name) && x.IdUsuarioNavigation.Clave.Equals(password) && x.IdUsuarioNavigation.Activo.Equals(true)).FirstOrDefaultAsync();



                    if (consulta != null)
                    {

                        return consulta;

                    }
                    return consulta;


                }
                catch (Exception)
                {

                    throw;
                }
            }



        }

    }
}
