using System.Collections.Generic;
using System.Linq;
using System.Web;
using INFO_360.Models;
using Newtonsoft.Json;

namespace INFO_360.Models
{
    public class Tarea
    {
        [JsonProperty]
        public int ID { get; set; }

        [JsonProperty]
        public string Titulo { get; private set; }

        [JsonProperty]
        public bool Finalizada { get;private set; }

        [JsonProperty]
        public string Descripcion { get;private set; }

        [JsonProperty]
        public int Duracion { get;private set; }

        [JsonProperty]
        public int IDusuario { get;private set; }
        public Tarea()
        {

        }

    }
}