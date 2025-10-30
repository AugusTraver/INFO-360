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
        public string Título { get; set; }

        [JsonProperty]
        public bool Finalizada { get; set; }

        [JsonProperty]
        public string Descripción { get; set; }

        [JsonProperty]
        public int Duración { get; set; }

        [JsonProperty]
        public int IDtarea { get; set; }

        public Tarea()
        {

        }

    }
}