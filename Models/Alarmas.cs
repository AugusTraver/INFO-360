using System.Collections.Generic;
using System.Linq;
using System.Web;
using INFO_360.Models;
using Newtonsoft.Json;

namespace INFO_360.Models
{
    public class Alarma
    {
        [JsonProperty]
        public int ID { get; set; }

        [JsonProperty]
        public string Tipo { get; set; }

        [JsonProperty]
        public DateTime Día { get; set; }

        [JsonProperty]
        public int Duración { get; set; }

        [JsonProperty]
        public bool Activo { get; set; }

        [JsonProperty]
        public int IDtarea { get; set; }

        public Alarma()
        {

        }

    }
}