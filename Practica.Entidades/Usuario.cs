using System;
using System.Collections.Generic;

#nullable disable

namespace Practica.Entidades
{
    [Serializable]
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
