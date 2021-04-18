
using Practica.Persistencia.Interfaz;
using Practica.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Practica.Persistencia.Volatil.Repositories;
using Practica.Persistencia.Interfaz.Repositories;
using Microsoft.AspNetCore.Http;

namespace Practica.Persistencia.Volatil
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PracticaContext _context;

        public UnitOfWork(IHttpContextAccessor httpContextAccessor)
        {
            _context = new PracticaContext(httpContextAccessor);
            Usuarios = new UsuarioRepository(_context);
        }

        public void SetEntityTracking(bool tracking)
        {
        }


        public IUsuarioRepository Usuarios { get; }


        public int GuardarCambios()
        {
            return 1;
        }

        public void DescartarCambios()
        {

        }

        public List<string> ListaCambios()
        {
            var res = new List<string>();
            return res;
        }

        public void Dispose()
        {
        }


    }
}