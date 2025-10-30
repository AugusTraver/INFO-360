using System.Collections.Generic;
using System.Linq;
using System.Web;
using INFO_360.Models;
using Newtonsoft.Json;

namespace INFO_360.Models
{
    public class TiempoLibre
    {
        [JsonProperty]
        public int ID { get; set; }

        [JsonProperty]
        public DateTime Día { get; set; }

        [JsonProperty]
        public int Horas { get; set; }

        [JsonProperty]
        public int IDusuario { get; set; }

        public TiempoLibre()
        {

        }

    }
}