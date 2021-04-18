using Practica.Persistencia.Interfaz.Repositories;
using System;
using System.Collections.Generic;

namespace Practica.Persistencia.Interfaz
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; }


        void SetEntityTracking(bool tracking);

        int GuardarCambios();

        void DescartarCambios();

        List<string> ListaCambios();

    }
}