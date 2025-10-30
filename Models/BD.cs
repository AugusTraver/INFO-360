using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;



namespace INFO_360.Models
{
    public static class BD
    {
        private static string _connectionString = "Server=localhost;Database=StartTime;Integrated Security=True;TrustServerCertificate=True;";

        public static string ObtenerContraseña(string texto)
        {
            string ans;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Contraseña from Usuarios where Nombre = @pNom";
                ans = connection.QueryFirstOrDefault<string>(query, new { pNom = texto });
            }

            return ans;
        }
        public static Usuario IniciarSesion(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE username = @Username AND password = @Passwod";
                Usuario usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Username = username, Passwod = password });
                return usuario;
            }
        }
        public static bool Registrarse(Usuario usuario)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                bool SeRegistro = true;
                string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE nombre = @PNombre ";
                int count = connection.QueryFirstOrDefault<int>(checkQuery, new { PNombre = usuario.Nombre });
                if (count != 0)
                {
                    SeRegistro = false;
                    return SeRegistro;
                }

                string query = "INSERT INTO Usuarios (Username, Contraseña, Nombre,Foto) VALUES (@Pusername, @Pcontraseña, @Pnombre, @Pfoto";
                connection.Execute(query, new
                {
                    Pusername = usuario.Username,
                    Pcontaseña = usuario.Contraseña,
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
                string query = "INSERT INTO Tarea (Titulo,Finalizada,Descripcion,Duracion,IDusuario) VALUE (@T,@F,@Des,@Dur,@I)";
                connection.Query(query, new { T = TareaInsert.Titulo, F = TareaInsert.Descripcion, Des = TareaInsert.Descripcion, Dur = TareaInsert.Duracion, I = TareaInsert.IDusuario });
            }
        }

    }
}