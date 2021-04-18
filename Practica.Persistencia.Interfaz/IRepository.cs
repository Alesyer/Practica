using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Practica.Persistencia.Interfaz
{
    public interface IRepository<T> where T : class
    {
        T ObtenerPorId(int id);

        IEnumerable<T> ObtenerTodos();

        IEnumerable<T> Filtrar(Expression<Func<T, bool>> predicate);

        int FiltroContar(Expression<Func<T, bool>> predicate);

        T FiltrarPrimero(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FiltrarOrdenadoPaginado(Expression<Func<T, bool>> predicate, bool ordenAscendente, string columnaOrdenacion, int limiteInferior, int elementosPorPagina);

        void Insertar(T entity);

        void InsertarVarios(IEnumerable<T> entities);


        void Modificar(T entity);

        void ModificarVarios(IEnumerable<T> entities);

        void Eliminar(T entity);

        void EliminarVarios(IEnumerable<T> entities);
    }
}