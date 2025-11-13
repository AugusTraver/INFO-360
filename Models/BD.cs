using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;



namespace INFO_360.Models
{
    public static class BD
    {
        private static string _connectionString = @"Server=localhost;
DataBase=StartTime; Integrated Security=True; TrustServerCertificate=True;";
      
       public static Usuario IniciarSesion(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "IniciarSesion";
                Usuario usuario = connection.QueryFirstOrDefault<Usuario>(storedProcedure, new { Username = username, Password = password },
                 commandType: CommandType.StoredProcedure
                );
                return usuario;
            }
        }

        public static bool Registrarse(Usuario usuario)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                bool SeRegistro = true;
<<<<<<< HEAD
                string storedProcedure = "Registrarse";
             int registrado = connection.QueryFirstOrDefault<int>(storedProcedure, new { PNombre = usuario.Nombre });
               
               
                connection.QueryFirstOrDefault(storedProcedure, new
=======
                string checkQuery = "SELECT COUNT(*) FROM Usuario WHERE Username = @AUsername";
                int count = connection.QueryFirstOrDefault<int>(checkQuery, new { AUsername = usuario.Username });
                if (count != 0)
>>>>>>> d737e0490360873812dca37cdf5e2297f0b0617a
                {
                    UsuarioNombre = usuario.Nombre,
                UsuarioEmail = usuario.Email,
                UsuarioContraseña = usuario.Contraseña,
                UsuarioUsername = usuario.Username,
                UsuarioFoto = usuario.Foto
                },

                commandType: CommandType.StoredProcedure
                );
                 SeRegistro = (registrado == 0);
        
                return SeRegistro;
            }
        }
        public static List<Tarea> ObtenerTareas(int idU)
<<<<<<< HEAD
        {
             List<Tarea> tareasusu = null;
=======
        { 
>>>>>>> d737e0490360873812dca37cdf5e2297f0b0617a
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "ObtenerTareas";
                 tareasusu = connection.Query<Tarea>(storedProcedure, new { idu = idU } ,  commandType: CommandType.StoredProcedure).ToList() ;

                return tareasusu;
            }
            
            
        }
        public static Tarea ObtenerTarea(int iDT)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "ObtenerTarea";
                Tarea tareasusu = connection.QueryFirstOrDefault(storedProcedure, new { idT = iDT },
                commandType: CommandType.StoredProcedure
                );
                return tareasusu;
            }
        }

        public static List<Alarmas> ObtenerAlarmas(int idU)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "ObtenerAlarmas";
                List<Alarmas> tareasusu = connection.Query<Alarmas>(storedProcedure, new { idu = idU }, 
                commandType: CommandType.StoredProcedure
                ).ToList();
                return tareasusu;
            }
        }

        public static Alarmas ObtenerAlarma(int iDA)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "ObtenerAlarma";
                Alarmas alarmas1 = connection.QueryFirstOrDefault(storedProcedure, new { IDA = iDA }
                , commandType: CommandType.StoredProcedure
                );
                return alarmas1;
            }
        }
        public static void CrearTarea(Tarea TareaInsert)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "CrearTarea";
                connection.Execute(storedProcedure, new { T = TareaInsert.Titulo, F = TareaInsert.Finalizado, Des = TareaInsert.Descripcion, Dur = TareaInsert.Duracion, I = TareaInsert.IDusuario },
                commandType: CommandType.StoredProcedure
                );
            }
        }
        public static void CrearAlarma(Alarmas alarma)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "CrearAlarma";
                connection.Query(storedProcedure, new { T = alarma.Tipo, Dia = alarma.Dia, Des = alarma.Duracion, Dur = alarma.Activo, I = alarma.IDusuario },
                  commandType: CommandType.StoredProcedure
                );
            }
        }

        public static void BorrarAlarma(int idAlarmaa)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "BorrarAlarma";
                connection.Query(storedProcedure, new { idAlarma = idAlarmaa },
                  commandType: CommandType.StoredProcedure
                );
            }
        }
        public static void BorrarTarea(int IDT)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "DELETE FROM Tarea WHERE ID = @IDt";
                connection.Query(storedProcedure, new { IDt = IDT },
                  commandType: CommandType.StoredProcedure
                );
            }
        }
        public static void ActualizarTarea(Tarea tarea, int PD)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string SP = "ActualizarTarea"; 
        

                connection.Execute(SP, new
                {
                    Titulo = tarea.Titulo,
                    Finalizado = tarea.Finalizado,
                    Descripcion = tarea.Descripcion,
                    Duracion = tarea.Duracion,
                    IDusuario = tarea.IDusuario,
                    ID = PD
                },
                commandType: CommandType.StoredProcedure);
            }
        }

        public static List<TiempoLibre> ObtenerTiempoLibre(int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "ObtenerTiempoLibre";
                List<TiempoLibre> tiempos = connection.Query<TiempoLibre>(storedProcedure, new { idUsuario = idUsuario },
                 commandType: CommandType.StoredProcedure
                ).ToList();
                return tiempos;
            }
        }

        public static void CrearTiempoLibre(TiempoLibre tiempo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO TiempoLibre (Dia, Horas, IDusuario) VALUES (@Dia, @Horas, @IDusuario)";
                connection.Execute(query, new { Dia = tiempo.Dia, Horas = tiempo.Horas, IDusuario = tiempo.IDusuario });
            }
        }

        public static void BorrarTiempoLibre(int idTiempo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM TiempoLibre WHERE ID = @idTiempo";
                connection.Execute(query, new { idTiempo = idTiempo });
            }
        }
    }
}



