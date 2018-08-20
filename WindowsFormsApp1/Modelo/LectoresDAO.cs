using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modelo
{
    public class LectoresDAO : Conexion
    {





        public Lectores ObtenerLector(string nu)
        {
            Lectores u = null;
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = "select * from Lectores where idlectores=@user";
            comando.Parameters.AddWithValue("@user", nu);
            lector = comando.ExecuteReader();
            if (lector.Read())
            {
                u = new Lectores();
                int i = 0;
                u.idlectores = lector.GetInt32(i++);
                u.nombre1 = lector.GetString(i++);
                u.nombre2 = lector.GetString(i++);
                u.apellido1 = lector.GetString(i++);
                u.apellido2 = lector.GetString(i++);
                u.direccion = lector.GetString(i++);
                u.telefono = lector.GetString(i++);
                desconectar();
            }
            return u;
        }

        public int idlectores()
        {
            int id = 1;
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = "select max(idlectores) from Lectores";
            lector = comando.ExecuteReader();
            if (lector.Read())
            {               
                    id = lector.GetInt32(0);               
            }
            desconectar();
            return id + 1;
        }


        public DataTable obtenerRegistros(string busca)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = "select * from Lectores where nombre1 like @nomb or apellido1 like @ape";
            comando.Parameters.AddWithValue("@nomb", busca + "%");
            comando.Parameters.AddWithValue("@ape", busca + "%");
            lector = comando.ExecuteReader();
            tabla.Load(lector);
            desconectar();
            lector.Close();
            return tabla;
        }

        public void registrarLector(Lectores u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = " INSERT INTO Lectores values(@user,@n1,@n2,@a1,@a2,@dir,@perfil)";
            comando.Parameters.AddWithValue("@user", u.idlectores);
            comando.Parameters.AddWithValue("@n1", u.nombre1);
            comando.Parameters.AddWithValue("@n2", u.nombre2);
            comando.Parameters.AddWithValue("@a1", u.apellido1);
            comando.Parameters.AddWithValue("@a2", u.apellido2);
            comando.Parameters.AddWithValue("@dir", u.direccion);
            comando.Parameters.AddWithValue("@perfil", u.telefono);
            comando.ExecuteNonQuery();
            desconectar();
        }


        public void actualizarLector(Lectores u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = " update Lectores set  nombre1=@n1, nombre2=@n2,apellido1=@a1,apellido2=@a2,direccion=@dir,telefono=@telefono where idlectores=@user";
            comando.Parameters.AddWithValue("@user", u.idlectores);
            comando.Parameters.AddWithValue("@n1", u.nombre1);
            comando.Parameters.AddWithValue("@n2", u.nombre2);
            comando.Parameters.AddWithValue("@a1", u.apellido1);
            comando.Parameters.AddWithValue("@a2", u.apellido2);
            comando.Parameters.AddWithValue("@dir", u.direccion);
            comando.Parameters.AddWithValue("@telefono", u.telefono);
            comando.ExecuteNonQuery();
            desconectar();
        }

        public void eliminarrLector(Lectores u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = " delete from Lectores  where idlectores=@user";
            comando.Parameters.AddWithValue("@user", u.idlectores);
            comando.ExecuteNonQuery();
            desconectar();
        }


    }
}
