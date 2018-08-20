using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modelo
{
    public class Conexion
    {
        protected SqlConnection conexion;
        protected SqlCommand comando;
        protected SqlDataReader lector;
       
        protected DataTable tabla;

        protected Conexion()
        {
            tabla = new DataTable();
        }

        protected void conetar()
        {
            String url = "Data Source=.\\SQLEXPRESS;Database=biblioteca;Integrated Security=True;";
            conexion = new SqlConnection(url);
            conexion.Open();
        }


        protected void desconectar()
        {
            conexion.Close();
        }

    }
}
