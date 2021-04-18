using Microsoft.AspNetCore.Mvc;
using Practica.Entidades;
using Practica.Persistencia.Interfaz;
using Practica.WebAngular8.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Practica.WebAngular8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IUnitOfWork repositorioUoW = null;

        public UsuariosController(IUnitOfWork _repositorioUoW)
        {
             this.repositorioUoW = _repositorioUoW;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return repositorioUoW.Usuarios.ObtenerTodos();
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            return new Usuario();
        }


        //POST api/<UsuariosController>
        [HttpPost]
        public void Post([FromBody] Usuario value)
        {
            value.Password = Encryption.EncriptarSHA256(value.Password);
            repositorioUoW.Usuarios.Insertar(value);
            repositorioUoW.GuardarCambios();
        }


        [HttpPost("GetAuthentication")]
        public bool GetAuthentication([FromBody] Usuario value)
        {
            value.Password = Encryption.EncriptarSHA256(value.Password);
            return repositorioUoW.Usuarios.FiltroContar(a => a.Password == value.Password && a.Email == value.Email) > 0;
        }

        //[HttpPost]
        //public void Post(bool valor)
        //{
        //    int a = 0;
        //}

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario value)
        {
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
