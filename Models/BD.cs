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
                Usuario usuario = connection.QueryFirstOrDefault<Usuario>(storedProcedure, new { PUserName = username, PContraseña = password },
                 commandType: CommandType.StoredProcedure);
                return usuario;
            }
        }
        public static bool Registrarse(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "Registrarse";


                int resultado = connection.Execute(storedProcedure, new
                {
                    UsuarioNombre = usuario.Nombre,
                    UsuarioEmail = usuario.Email,
                    UsuarioContraseña = usuario.Contraseña,
                    UsuarioUsername = usuario.Username,
                    UsuarioFoto = usuario.Foto
                },
                commandType: CommandType.StoredProcedure);


                return resultado > 0;
            }
        }
        public static List<Tarea> ObtenerTareas(int widU)
        {
            List<Tarea> tareasusu = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "ObtenerTareas";
                tareasusu = connection.Query<Tarea>(storedProcedure, new { IdU = widU }, commandType: CommandType.StoredProcedure).ToList();

                return tareasusu;
            }


        }
        public static Tarea ObtenerTarea(int iDT)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "ObtenerTarea";
                Tarea tareasusu = connection.QueryFirstOrDefault(storedProcedure, new { Idt = iDT },
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

        public static void ActualizarAlarma(Alarmas alarma)  //Falta Hacer el StoreProcedure de este
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Alarmas SET Tipo = @T,Dia = @F,Duracion = @Des,Activo = @Dur,IDusuario = @I WHERE id = @wasd";
                connection.Query(query, new { T = alarma.Tipo, F = alarma.Dia, Des = alarma.Duracion, Dur = alarma.Activo, I = alarma.IDusuario, wsad = alarma.ID });


            }
        }

        public static void ActualizarTarea(Tarea tarea, int PD)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "ActualizarTarea";


                connection.Execute(storedProcedure, new
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
                string storedProcedure = "CrearTiempoLibre";

                connection.Execute(
                    storedProcedure,
                    new
                    {
                        Dia = tiempo.Dia,
                        Horas = tiempo.Horas,
                        IDusuario = tiempo.IDusuario
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public static void BorrarTiempoLibre(int idTiempo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string storedProcedure = "BorrarTiempoLibre";

                connection.Execute(
                    storedProcedure,
                    new { idTiempo = idTiempo },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

    }
}



