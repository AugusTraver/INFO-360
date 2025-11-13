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
        public bool Finalizado { get; private set; }

        [JsonProperty]
        public string Descripcion { get; private set; }

        [JsonProperty]
        public int Duracion { get; private set; }

        [JsonProperty]
        public int IDusuario { get; private set; }


        public Tarea(string Ptitulo, bool Pfin, string Pdesc, int Pdur, int idU)
        {
            Titulo = Ptitulo;
            Finalizado = Pfin;
            Descripcion = Pdesc;
            Duracion = Pdur;
            IDusuario = idU;
        }
        public Tarea()
        {

        }
              public Tarea(string Ptitulo, int Pdur)
        {
            Titulo = Ptitulo;
            Duracion = Pdur;
        }
    }
}