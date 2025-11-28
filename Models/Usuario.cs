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
{
    // -- COMENTADO POR SI ESTOY RINDIENDO EL CAE --
    //relevante para entender la función: https://docs.google.com/spreadsheets/d/1uqw3PreoGD4LIFHwlWqppqMp9pgVlC-ieNxSh6B4FXQ/edit?gid=0#gid=0

    Dictionary<DateTime, Dictionary<double, Tarea>> agendaSemanal = new Dictionary<DateTime, Dictionary<double, Tarea>>();

    List<TiempoLibre> tiemposLibres = BD.ObtenerTiempoLibre(this.ID);

    if (tiemposLibres == null || tiemposLibres.Count == 0 || //Hay TL
        temporales == null || temporales.Count == 0)//Hay Tareas
    {
        return agendaSemanal;
    }

    DateTime hoy = DateTime.Today;
    DateTime finSemana = hoy.AddDays(7);

    List<TiempoLibre> tlXsemana = new List<TiempoLibre>();

    for (int i = 0; i < tiemposLibres.Count; i++)
    {
        TiempoLibre tl = tiemposLibres[i];

        if (tl.Dia >= hoy && tl.Dia < finSemana) // verificar si es esta semana
        {
            tlXsemana.Add(tl);
        }
    }

    // Ordenar TL por fecha y hora
    for (int i = 0; i < tlXsemana.Count - 1; i++)
    {
        for (int j = i + 1; j < tlXsemana.Count; j++)
        {
            if (tlXsemana[i].Dia > tlXsemana[j].Dia)
            {
                TiempoLibre aux = tlXsemana[i];
                tlXsemana[i] = tlXsemana[j];
                tlXsemana[j] = aux;
            }
        }
    }

    // Pasar tareas temporales a lista
    List<Tarea> tareasPendientes = new List<Tarea>();
    foreach (double a in temporales.Keys)
    {
        tareasPendientes.Add(temporales[a]);
    }

    for (int i = 0; i < tlXsemana.Count; i++) // Recorrer cada bloque de tiempo libre 
    {
        TiempoLibre tl = tlXsemana[i];

        // La fecha del día SIN hora para indexar agendaSemanal
        DateTime fechaDia = tl.Dia.Date;

        // Asegurar que exista entrada para ese día
        if (!agendaSemanal.ContainsKey(fechaDia))
        {
            agendaSemanal[fechaDia] = new Dictionary<double, Tarea>();
        }

        Dictionary<double, Tarea> agendaDia = agendaSemanal[fechaDia];

        // Calcular hora de inicio real (sale del DateTime completo)
        double horaActual = tl.Dia.Hour + (tl.Dia.Minute / 60.0);

        // Calcular hora de fin del TL
        double horaFinTL = horaActual + tl.Horas;

        // Quedan horas disponibles
        double horasDisponibles = tl.Horas;

        int idxTarea = 0;

        // Colocar tareas dentro del bloque de tiempo libre
        while (idxTarea < tareasPendientes.Count && horasDisponibles > 0)
        {
            Tarea t = tareasPendientes[idxTarea];

            // Si la tarea entra completa dentro del bloque
            if (t.Duracion <= horasDisponibles)
            {
                double horaFin = horaActual + t.Duracion;

                // Asignar cada 15 minutos dentro del bloque
                for (double h = horaActual; h < horaFin; h += 0.25)
                {
                    agendaDia[h] = t;
                }

                horaActual = horaFin;
                horasDisponibles -= t.Duracion;
                tareasPendientes.RemoveAt(idxTarea);
            }
            else // Si solo entra una parte de la tarea
            {
                double duracionPosible = horasDisponibles;

                double horaFin = horaActual + duracionPosible;

                for (double h = horaActual; h < horaFin; h += 0.25)
                {
                    agendaDia[h] = t;
                }

                // Reducir duración restante
                t.modificarDur((int)duracionPosible);

                horaActual = horaFin;
                horasDisponibles = 0;

                if (t.Duracion <= 0)
                {
                    tareasPendientes.RemoveAt(idxTarea);
                }
                else
                {
                    idxTarea++;
                }
            }
        }
    }

    return agendaSemanal;
}
     
        public void guardarTL(DateTime Dia, int Horas, int IDu){


            TiempoLibre tl = new TiempoLibre(Dia, Horas, IDu);
            BD.CrearTiempoLibre(tl);


        }
    



    }
}




