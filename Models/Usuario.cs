using System.Collections.Generic;
using System.Linq;
using System.Web;
using INFO_360.Models;
using Newtonsoft.Json;

namespace INFO_360.Models
{
    public class Usuario
    {
        [JsonProperty]
        public int ID { get; set; }

        [JsonProperty]
        public string Nombre { get; set; }

        [JsonProperty]
        public string Username { get; set; }

        [JsonProperty]
        public string Contraseña { get; set; }

        [JsonProperty]
        public string Foto { get; set; }

        [JsonProperty]
        public string Email {get;set;}

        public Usuario( string pEmail,string pUsername,string pNombre, string pContraseña, string pFoto)
        {
            Email = pEmail;
            Nombre = pNombre;
            Username = pUsername;
            Contraseña = pContraseña;
            Foto = pFoto;
        }

    }
}