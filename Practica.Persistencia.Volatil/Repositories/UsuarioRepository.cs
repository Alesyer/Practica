using Practica.Persistencia.Interfaz.Repositories;
using Practica.Entidades;
using System.Linq.Expressions;
using Practica.Persistencia.Interfaz;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace Practica.Persistencia.Volatil.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PracticaContext _context;
        public  List<Usuario> _usuarios;
        public UsuarioRepository(PracticaContext context)
        {
            _context = context;
            _usuarios = context.Usuarios;
        }




        public Usuario ObtenerPorId(int id)
        {
            return _usuarios.Find(a=> a.Id == id);
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _usuarios;
        }

        public IEnumerable<Usuario> Filtrar(Expression<Func<Usuario, bool>> predicate)
        {
            return _usuarios.FindAll(predicate.Compile().Invoke);
        }

        public int FiltroContar(Expression<Func<Usuario, bool>> predicate)
        {
            return _usuarios.FindAll(predicate.Compile().Invoke).Count();
        }

        public Usuario FiltrarPrimero(Expression<Func<Usuario, bool>> predicate)
        {
            return _usuarios.FirstOrDefault(predicate.Compile().Invoke);
        }

        public IEnumerable<Usuario> FiltrarOrdenadoPaginado(Expression<Func<Usuario, bool>> predicate, bool ordenAscendente, string columnaOrdenacion, int limiteInferior, int elementosPorPagina)
        {
            PropertyInfo prop = typeof(Usuario).GetProperty(columnaOrdenacion);
            if (predicate != null)
            {
                if (ordenAscendente)
                    return _usuarios.Where(predicate.Compile()).OrderBy(x => prop.GetValue(x, null)).Skip(limiteInferior).Take(elementosPorPagina);
                else
                    return _usuarios.Where(predicate.Compile()).OrderByDescending(x => prop.GetValue(x, null)).Skip(limiteInferior).Take(elementosPorPagina);
            }
            else
            {
                if (ordenAscendente)
                    return _usuarios.OrderBy(x => prop.GetValue(x, null)).Skip(limiteInferior).Take(elementosPorPagina);
                else
                    return _usuarios.OrderByDescending(x => prop.GetValue(x, null)).Skip(limiteInferior).Take(elementosPorPagina);
            }

        }

        public void Insertar(Usuario entity)
        {
            _usuarios.Add(entity);
            _context.Usuarios = _usuarios;
        }

        public void InsertarVarios(IEnumerable<Usuario> entities)
        {
            _usuarios.AddRange(entities);

            _context.Usuarios = _usuarios;
        }

        public void Modificar(Usuario entity)
        {
            var indx = _usuarios.FindIndex(a => a.Id == entity.Id);
            if (indx >= 0)
                _usuarios[indx] = entity;
        }

        public void ModificarVarios(IEnumerable<Usuario> entities)
        {
            foreach (var entitie in entities)
                Modificar(entitie);
        }

        public void Eliminar(Usuario entity)
        {
            _usuarios.Remove(entity);
        }

        public void EliminarVarios(IEnumerable<Usuario> entities)
        {
            foreach (var entitie in entities)
                Eliminar(entitie);
        }

      
    }
}
