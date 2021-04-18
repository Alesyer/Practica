using Practica.Persistencia.Interfaz.Repositories;
using Practica.Entidades;

namespace Practica.Persistencia.EntityFramework.MySql.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly PracticaContext _context;

        public UsuarioRepository(PracticaContext context) : base(context)
        {
            _context = context;
        }
    }
}
