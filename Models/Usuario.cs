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
        public Dictionary<DateTime, TiempoLibre> TiempoLibrexDia { get; private set; }
        [JsonProperty]
        public List<Tarea> ListaTareas { get; private set; }
        [JsonProperty]
        public List<Alarmas> ListaAlarmas { get; private set; }

        public Usuario()
        {

        }
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

        public double CalcularTiempoTareas()
        {

            double tTotal = 0;
            foreach (Tarea t in ListaTareas)
            {

                tTotal += t.Duracion;
            }

            return tTotal;
        }
        public double CalcularTiempoLibre()
        {
            double TiempoLibreTotal = 0;
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

        public Dictionary<DateTime, Dictionary<double, Tarea>> OrganizarSemana(Dictionary<double, Tarea> temporales)
        {
            Dictionary<DateTime, Dictionary<double, Tarea>> agendaSemanal = new Dictionary<DateTime, Dictionary<double, Tarea>>();

            List<TiempoLibre> tiemposLibres = BD.ObtenerTiempoLibre(this.ID);

            if (tiemposLibres == null || tiemposLibres.Count == 0 || temporales == null || temporales.Count == 0)
            {
                return agendaSemanal;
            }

            DateTime hoy = DateTime.Today;
            DateTime finSemana = hoy.AddDays(7);

            List<TiempoLibre> libresSemana = new List<TiempoLibre>();
            for (int i = 0; i < tiemposLibres.Count; i++)
            {
                TiempoLibre t = tiemposLibres[i];
                if (t.Dia >= hoy && t.Dia < finSemana)
                {
                    libresSemana.Add(t);
                }
            }

            for (int i = 0; i < libresSemana.Count - 1; i++)
            {
                for (int j = i + 1; j < libresSemana.Count; j++)
                {
                    if (libresSemana[i].Dia > libresSemana[j].Dia)
                    {
                        TiempoLibre a = libresSemana[i];
                        libresSemana[i] = libresSemana[j];
                        libresSemana[j] = a;
                    }
                }
            }

            List<Tarea> tareasPendientes = new List<Tarea>();
            double[] clavesTemporales = new double[temporales.Keys.Count];
            temporales.Keys.CopyTo(clavesTemporales, 0);
            for (int i = 0; i < clavesTemporales.Length; i++)
            {
                double clave = clavesTemporales[i];
                Tarea t = temporales[clave];
                tareasPendientes.Add(t);
            }


            for (int i = 0; i < libresSemana.Count; i++)
            {
                TiempoLibre tl = libresSemana[i];
                double horasDisponibles = tl.Horas;

                Dictionary<double, Tarea> agendaDia = new Dictionary<double, Tarea>();
                double horaActual = 8.0;
                int j = 0;
                while (j < tareasPendientes.Count && horasDisponibles > 0)
                {
                    Tarea tarea = tareasPendientes[j];

                    if (tarea.Duracion <= horasDisponibles && horaActual + tarea.Duracion <= 20.0)
                    {
                        double fin = horaActual + tarea.Duracion;

                        for (double h = horaActual; h < fin; h += 0.25)
                        {
                            agendaDia[h] = tarea;
                        }

                        horaActual = horaActual + tarea.Duracion;
                        horasDisponibles = horasDisponibles - tarea.Duracion;

                        tareasPendientes.RemoveAt(j);
                    }
                    else
                    {
                        j++;
                    }
                }

                agendaSemanal[tl.Dia] = agendaDia;
            }


            return agendaSemanal;
        }


    }
}




