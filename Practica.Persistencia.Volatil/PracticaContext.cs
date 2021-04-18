using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Practica.Entidades;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Practica.Persistencia.Volatil
{
    public partial class PracticaContext 
    {


        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<Usuario> Usuarios
        {
            get
            {
                byte[] aux;
                if (_httpContextAccessor.HttpContext.Session.TryGetValue("usuarios", out aux))
                    return (List<Usuario>)this.ByteArrayToObject(aux);
                else
                    return new List<Usuario>();
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.Set("usuarios", this.ObjectToByteArray(value));
            }
        }

        public PracticaContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;            
        }


        // Convert an object to a byte array
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }

    }
}
