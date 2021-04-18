using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Practica.Persistencia.Interfaz;
using Practica.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Practica.Persistencia.EntityFramework.MySql.Repositories;
using Practica.Persistencia.Interfaz.Repositories;

namespace Practica.Persistencia.EntityFramework.MySql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PracticaContext _context;


        public UnitOfWork()
        {
            _context = new PracticaContext();

            Usuarios = new UsuarioRepository(_context);

            _context.ChangeTracker.LazyLoadingEnabled = false;
        }

        public void SetEntityTracking(bool tracking)
        {
            _context.ChangeTracker.QueryTrackingBehavior = tracking ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
        }


        public IUsuarioRepository Usuarios { get; }


        public int GuardarCambios()
        {
            return _context.SaveChanges();
        }

        public void DescartarCambios()
        {
            var entries = _context.ChangeTracker
                           .Entries()
                           .Where(e => e.State != EntityState.Unchanged)
                           .ToArray();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }

        }

        public List<string> ListaCambios()
        {
            var res = new List<string>();
            foreach (EntityEntry e in _context.ChangeTracker.Entries())
            {
                if (e != null && e.State != EntityState.Unchanged)
                    res.Add(e.Entity.ToString() + " " + e.State.ToString());
            }
            return res;
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}