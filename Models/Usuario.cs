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
        public string Nombre { get; private set; }

        [JsonProperty]
        public string Username { get; private set; }

        [JsonProperty]
        public string Contraseña { get; private set; }

        [JsonProperty]
        public string Foto { get; private set; }

        [JsonProperty]
        public string Email { get; private set; }
        [JsonProperty]
        public int TiempoLibreTotal { get; private set; }

        public Usuario(int pID, string pEmail, string pUsername, string pNombre, string pContraseña, string pFoto)
        {
            ID = pID;
            Email = pEmail;
            Nombre = pNombre;
            Username = pUsername;
            Contraseña = pContraseña;
            Foto = pFoto;
        }
        public Usuario(string pEmail, string pUsername, string pNombre, string pContraseña, string pFoto)
        {
            Email = pEmail;
            Nombre = pNombre;
            Username = pUsername;
            Contraseña = pContraseña;
            Foto = pFoto;
        }

        public List<Tarea> ObtenerTareas()
        {
            return BD.ObtenerTareas(ID);
        }


    }
}