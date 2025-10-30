using System.Collections.Generic;
using System.Linq;
using System.Web;
using INFO_360.Models;
using Newtonsoft.Json;

namespace INFO_360.Models
{
    public class Alarmas
    {
        [JsonProperty]
        public int ID { get; set; }

        [JsonProperty]
        public string Tipo { get; private set; }
          [JsonProperty]
        public string Nombre { get; private set; }

        [JsonProperty]
        public DateTime Dia { get;private set; }

        [JsonProperty]
        public int Duracion { get;private set; }

        [JsonProperty]
        public bool Activo { get;private set; }

        [JsonProperty]
        public int IDusuario { get;private set; }

        public Alarmas()
        {

        }

    }
}