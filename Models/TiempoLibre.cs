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
        public int ID { get; private set; }

        [JsonProperty]
        public DateTime Dia { get;private set; }

        [JsonProperty]
        public int Horas { get;private set; }

        [JsonProperty]
        public int IDusuario { get;private set; }

        public TiempoLibre()
        {

        }

    }
}