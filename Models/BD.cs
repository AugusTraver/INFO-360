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

    }
}