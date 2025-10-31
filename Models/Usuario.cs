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
        public Dictionary<string, TiempoLibre> TiempoLibrexDia { get; private set; }
        [JsonProperty]
        public List<Tarea> ListaTareas { get; private set; }
        [JsonProperty]
        public List<Alarmas> ListaAlarmas { get; private set; }
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

        public void CrearTarea(Tarea t)
        {
            BD.CrearTarea(t);
        }
        public void CrearAlarma(Alarmas a)
        {
            BD.CrearAlarma(a);
        }
        public void BorrarAlarma(int idAlarma)
        {
            BD.BorrarAlarma(idAlarma);
        }
        public void BorrarTarea(int T)
        {
            BD.BorrarTarea(T);
        }
        public void ActualizarAlarma(Alarmas a)
        {
            BD.ActualizarAlarma(a);
        }
        public void ActualizarTarea(Tarea T)
        {
            BD.ActualizarTarea(T);
        }
        public int CalcularTiempoLibre()
        {
            int TiempoLibreTotal = 0;
            foreach (TiempoLibre a in TiempoLibrexDia.Values)
            {
                TiempoLibreTotal += a.Horas;
            }

            foreach (Tarea a in ListaTareas)
            {
                TiempoLibreTotal -= a.Duracion;
            }
            return TiempoLibreTotal;
        }

        public Dictionary<double, Tarea> OrganizarDía(Dictionary<double, Tarea> Pendientes)
        {
            Dictionary<double, Tarea> agenda = new Dictionary<double, Tarea>();

            foreach (double t in Pendientes.Keys)
            {
                double c = Pendientes[1].Duracion * 4.0;

                for (double i = t; i <= c; i += 0.25)
                {
                    if (Pendientes.ContainsKey(i))
                    {
                        agenda.Add(i, Pendientes[i]);
                    }
                    else
                    {
                        agenda.Add(i, null);
                    }
                }
            }

            return agenda;
        }

    }
}




