using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public static string ObtenerContraseña(string texto)
        {
            string ans;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Contraseña from Usuario where Username = @pNom";
                ans = connection.QueryFirstOrDefault<string>(query, new { pNom = texto });
            }

            return ans;
        }
        public static Usuario IniciarSesion(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuario WHERE Username = @Username AND Contraseña = @Password";
                Usuario usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Username = username, Password = password });
                return usuario;
            }
        }

        public static bool Registrarse(Usuario usuario)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                bool SeRegistro = true;
                string checkQuery = "SELECT COUNT(*) FROM Usuario WHERE Username = @AUsername";
                int count = connection.QueryFirstOrDefault<int>(checkQuery, new { AUsername = usuario.Username });
                if (count != 0)
                {
                    SeRegistro = false;
                    return SeRegistro;
                }

                string query = "INSERT INTO Usuario (Email, Username, Contraseña, Nombre, Foto) VALUES (@Pemail, @Pusername, @Pcontraseña, @Pnombre, @Pfoto)";
                connection.Execute(query, new
                {
                    Pemail = usuario.Email,
                    Pusername = usuario.Username,
                    Pcontraseña = usuario.Contraseña,
                    Pnombre = usuario.Nombre,
                    Pfoto = usuario.Foto,

                });
                return SeRegistro;
            }
        }
        public static List<Tarea> ObtenerTareas(int idU)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tarea WHERE IDusuario = @idu";
                List<Tarea> tareasusu = connection.Query<Tarea>(query, new { idu = idU }).ToList();
                return tareasusu;
            }
        }
        public static Tarea ObtenerTarea(int iDT)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tarea WHERE ID = @idT";
                Tarea tareasusu = connection.QueryFirstOrDefault(query, new { idT = iDT });
                return tareasusu;
            }
        }

        public static List<Alarmas> ObtenerAlarmas(int idU)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Alarmas WHERE IDusuario = @idu";
                List<Alarmas> tareasusu = connection.Query<Alarmas>(query, new { idu = idU }).ToList();
                return tareasusu;
            }
        }

        public static Alarmas ObtenerAlarma(int iDA)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Alarmas WHERE ID = @IDA";
                Alarmas alarmas1 = connection.QueryFirstOrDefault(query, new { IDA = iDA });
                return alarmas1;
            }
        }
        public static void CrearTarea(Tarea TareaInsert)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Tarea (Titulo,Finalizado,Descripcion,Duracion,IDusuario) VALUES (@T,@F,@Des,@Dur,@I)";
                connection.Execute(query, new { T = TareaInsert.Titulo, F = TareaInsert.Finalizado, Des = TareaInsert.Descripcion, Dur = TareaInsert.Duracion, I = TareaInsert.IDusuario });
            }
        }
        public static void CrearAlarma(Alarmas alarma)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Alarmas (Tipo,Dia,Duracion,Activo,IDusuario) VALUES (@T,@F,@Des,@Dur,@I)";
                connection.Query(query, new { T = alarma.Tipo, F = alarma.Dia, Des = alarma.Duracion, Dur = alarma.Activo, I = alarma.IDusuario });
            }
        }

        public static void BorrarAlarma(int idAlarmaa)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Alarmas WHERE ID = @idAlarma";
                connection.Query(query, new { idAlarma = idAlarmaa });
            }
        }
        public static void BorrarTarea(int IDT)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Tarea WHERE ID = @IDt";
                connection.Query(query, new { IDt = IDT });
            }
        }
        public static void ActualizarAlarma(Alarmas alarma)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Alarmas SET Tipo = @T,Dia = @F,Duracion = @Des,Activo = @Dur,IDusuario = @I WHERE id = @wasd";
                connection.Query(query, new { T = alarma.Tipo, F = alarma.Dia, Des = alarma.Duracion, Dur = alarma.Activo, I = alarma.IDusuario, wsad = alarma.ID });
            }
        }
        public static void ActualizarTarea(Tarea tarea)
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
                    ID = tarea.ID
                },
                commandType: CommandType.StoredProcedure);
            }
        }

        public static List<TiempoLibre> ObtenerTiempoLibre(int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TiempoLibre WHERE IDusuario = @idUsuario";
                List<TiempoLibre> tiempos = connection.Query<TiempoLibre>(query, new { idUsuario = idUsuario }).ToList();
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



