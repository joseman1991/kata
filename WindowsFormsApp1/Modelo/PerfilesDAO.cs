using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modelo
{
   public class PerfilesDAO : Conexion
    {
        private List<Perfiles> myVar;

        public PerfilesDAO()
        {
            ListaPerfiles = new List<Perfiles>();
        }

        public List<Perfiles> ListaPerfiles
        {
            get { return myVar; }
            set { myVar = value; }
        }


        public void obtenerPerfiles()
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = "select * from perfiles";
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Perfiles p = new Perfiles();
                p.idperfil = lector.GetInt32(0);
                p.descripcion = lector.GetString(1);
                ListaPerfiles.Add(p);
            }
            desconectar();
        }


    }
}
