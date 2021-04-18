using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Practica.Persistencia.Interfaz;
using System.Reflection;

namespace Practica.Persistencia.EntityFramework.MySql
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _entities = context.Set<T>();
        }

        public T ObtenerPorId(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> ObtenerTodos()
        {
            return _entities.ToList();
        }

        public IEnumerable<T> Filtrar(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public int FiltroContar(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate).Count();
        }

        public T FiltrarPrimero(Expression<Func<T, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public IEnumerable<T> FiltrarOrdenadoPaginado(Expression<Func<T, bool>> predicate, bool ordenAscendente, string columnaOrdenacion, int limiteInferior, int elementosPorPagina)
        {
            PropertyInfo prop = typeof(T).GetProperty(columnaOrdenacion);
            if (predicate != null)
            {
                if (ordenAscendente)
                    return _entities.Where(predicate).OrderBy(x => prop.GetValue(x, null)).Skip(limiteInferior).Take(elementosPorPagina);
                else
                    return _entities.Where(predicate).OrderByDescending(x => prop.GetValue(x, null)).Skip(limiteInferior).Take(elementosPorPagina);
            }
            else
            {
                if (ordenAscendente)
                    return _entities.OrderBy(x => prop.GetValue(x, null)).Skip(limiteInferior).Take(elementosPorPagina);
                else
                    return _entities.OrderByDescending(x => prop.GetValue(x, null)).Skip(limiteInferior).Take(elementosPorPagina);
            }

        }

        public void Insertar(T entity)
        {
            _entities.Add(entity);
        }

        public void InsertarVarios(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public void Modificar(T entity)
        {
            _entities.Update(entity);
        }

        public void ModificarVarios(IEnumerable<T> entities)
        {
            _entities.UpdateRange(entities);
        }

        public void Eliminar(T entity)
        {
            _entities.Remove(entity);
        }

        public void EliminarVarios(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}