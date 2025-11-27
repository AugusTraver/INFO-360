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
        public List<Tarea> ListaTareas { get; set; }
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
        public Usuario(string pEmail, string pUsername, string pContraseña, string pNombre, string pFoto)
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
        public void ActualizarTarea(Tarea T, int ID)
        {
            BD.ActualizarTarea(T, ID);
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

        public Dictionary<DateTime, Dictionary<double, Tarea>> OrganizarSemana(Dictionary<double, Tarea> temporales)
        {             // --- COMENTADO POR SI ESTOY RINDIENDO CAE ---

            Dictionary<DateTime, Dictionary<double, Tarea>> agendaSemanal = new Dictionary<DateTime, Dictionary<double, Tarea>>();

            List<TiempoLibre> tiemposLibres = BD.ObtenerTiempoLibre(this.ID);

            if (tiemposLibres == null || tiemposLibres.Count == 0 || temporales == null || temporales.Count == 0)
            {
                return agendaSemanal;
            }

            DateTime hoy = DateTime.Today;
            DateTime finSemana = hoy.AddDays(7);

            //Encontrar el tiempo libre en la semana

            List<TiempoLibre> tlXsemana = new List<TiempoLibre>();
            for (int i = 0; i < tiemposLibres.Count; i++)
            {
                TiempoLibre t = tiemposLibres[i];
                if (t.Dia >= hoy && t.Dia < finSemana)
                {
                    tlXsemana.Add(t);
                }
            }

            //Ordena de mayor a menor los TLs
            for (int i = 0; i < tlXsemana.Count - 1; i++)
            {
                for (int j = i + 1; j < tlXsemana.Count; j++)
                {
                    if (tlXsemana[i].Dia > tlXsemana[j].Dia)
                    {
                        TiempoLibre a = tlXsemana[i];
                        tlXsemana[i] = tlXsemana[j];
                        tlXsemana[j] = a;
                    }
                }
            }

            //Copia las tareas a una lista
            List<Tarea> tareasPendientes = new List<Tarea>();
            double[] clavesTemporales = new double[temporales.Keys.Count];
            temporales.Keys.CopyTo(clavesTemporales, 0);             //El 0 es de donde arranca (creo)
            for (int i = 0; i < clavesTemporales.Length; i++)
            {
                double clave = clavesTemporales[i];
                Tarea t = temporales[clave];
                tareasPendientes.Add(t);
            }

            //Pone cada tarea en un día
            for (int i = 0; i < tlXsemana.Count; i++)
            {
                TiempoLibre tl = tlXsemana[i];
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

                        horaActual += tarea.Duracion;
                        horasDisponibles -= tarea.Duracion;
                        tareasPendientes.RemoveAt(j);
                    }
                    else
                    {
                        double duracionParcial = Math.Min(horasDisponibles, 20.0 - horaActual);

                        if (duracionParcial > 0)
                        {
                            double fin = horaActual + duracionParcial;

                            for (double h = horaActual; h < fin; h += 0.25)
                            {
                                agendaDia[h] = tarea;
                            }

                            tarea.modificarDur((int)duracionParcial);
                            horaActual += duracionParcial;
                            horasDisponibles -= duracionParcial;
                        }

                        if (tarea.Duracion <= 0)
                        {
                            tareasPendientes.RemoveAt(j);
                        }
                        else
                        {
                            j++;
                        }
                    }
                }

                agendaSemanal[tl.Dia] = agendaDia;
            }


            return agendaSemanal;
        }

        public void guardarTL(DateTime Dia, int Horas, int IDu){


            TiempoLibre tl = new TiempoLibre(Dia, Horas, IDu);
            BD.CrearTiempoLibre(tl);


        }
    



    }
}




