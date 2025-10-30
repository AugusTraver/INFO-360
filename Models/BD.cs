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
            connection.Open();
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

    }
}