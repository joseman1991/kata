using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Modelo
{
    public class UsuariosDAO : Conexion
    {

        public Usuarios login(Usuarios u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = "select nombre1,nombre2,apellido1,apellido2,direccion from usuarios where nombreusuario=@user and clave=@clave and idperfil=@perfil";
            comando.Parameters.AddWithValue("@user", u.nombreusuario);
            comando.Parameters.AddWithValue("@clave", u.clave);
            comando.Parameters.AddWithValue("@perfil", u.idperfil);
            lector = comando.ExecuteReader();
            if (lector.Read())
            {
                int i = 0;
                u.nombre1 = lector.GetString(i++);
                u.nombre2 = lector.GetString(i++);
                u.apellido1 = lector.GetString(i++);
                u.apellido2 = lector.GetString(i++);
                u.direccion = lector.GetString(i++);
                desconectar();
                return u;
            }

            lector.Close();
            comando = null;
            comando = conexion.CreateCommand();
            comando.CommandText = "select nombre1,nombre2,apellido1,apellido2,direccion from usuarios where nombreusuario=@user and clave=@clave";
            comando.Parameters.AddWithValue("@user", u.nombreusuario);
            comando.Parameters.AddWithValue("@clave", u.clave);
            lector = comando.ExecuteReader();
            if (lector.Read())
            {
                int i = 0;
                u.idperfil = 0;
                u.nombre1 = lector.GetString(i++);
                u.nombre2 = lector.GetString(i++);
                u.apellido1 = lector.GetString(i++);
                u.apellido2 = lector.GetString(i++);
                u.direccion = lector.GetString(i++);
                desconectar();
                return u;
            }


            lector.Close();
            comando = null;
            comando = conexion.CreateCommand();
            comando.CommandText = "select nombre1,nombre2,apellido1,apellido2,direccion from usuarios where nombreusuario=@user";
            comando.Parameters.AddWithValue("@user", u.nombreusuario);
            lector = comando.ExecuteReader();
            if (lector.Read())
            {
                int i = 0;
                u.clave = null;
                u.nombre1 = lector.GetString(i++);
                u.nombre2 = lector.GetString(i++);
                u.apellido1 = lector.GetString(i++);
                u.apellido2 = lector.GetString(i++);
                u.direccion = lector.GetString(i++);               

                desconectar();
                return u;
            }
            else
            {
                u = null;
                desconectar();
                return u;
            }            
        }



        public Usuarios ObtenerUsuario(string nu)
        {
            Usuarios u=null;
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = "select * from usuarios where nombreusuario=@user";
            comando.Parameters.AddWithValue("@user", nu);            
            lector = comando.ExecuteReader();
            if (lector.Read())
            {
                u = new Usuarios();
                int i = 0;
                u.nombreusuario= lector.GetString(i++);
                u.clave = lector.GetString(i++);
                u.nombre1 = lector.GetString(i++);
                u.nombre2 = lector.GetString(i++);
                u.apellido1 = lector.GetString(i++);
                u.apellido2 = lector.GetString(i++);
                u.direccion = lector.GetString(i++);
                u.idperfil = lector.GetInt32(i++);
                desconectar();                
            }
            return u;
        }

        public DataTable obtenerRegistros(string busca)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = "select * from usuarios where nombreusuario like @nomb or apellido1 like @ape";
            comando.Parameters.AddWithValue("@nomb",busca+"%");
            comando.Parameters.AddWithValue("@ape",busca+"%");             
            lector = comando.ExecuteReader();
            tabla.Load(lector);
            desconectar();
            lector.Close();
            return tabla;
        }

        public void registrarUsuario(Usuarios u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = " INSERT INTO usuarios values(@user,@clave,@n1,@n2,@a1,@a2,@dir,@perfil)";
            comando.Parameters.AddWithValue("@user", u.nombreusuario);
            comando.Parameters.AddWithValue("@clave", u.clave);
            comando.Parameters.AddWithValue("@n1", u.nombre1);
            comando.Parameters.AddWithValue("@n2", u.nombre2);
            comando.Parameters.AddWithValue("@a1", u.apellido1);
            comando.Parameters.AddWithValue("@a2", u.apellido2);
            comando.Parameters.AddWithValue("@dir", u.direccion);
            comando.Parameters.AddWithValue("@perfil", u.idperfil);
            comando.ExecuteNonQuery();
            desconectar();
        }


        public void actualizarUsuario(Usuarios u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = " update usuarios set clave=@clave, nombre1=@n1, nombre2=@n2,apellido1=@a1,apellido2=@a2,direccion=@dir,idperfil=@perfil where nombreusuario=@user";
            comando.Parameters.AddWithValue("@user", u.nombreusuario);
            comando.Parameters.AddWithValue("@clave", u.clave);
            comando.Parameters.AddWithValue("@n1", u.nombre1);
            comando.Parameters.AddWithValue("@n2", u.nombre2);
            comando.Parameters.AddWithValue("@a1", u.apellido1);
            comando.Parameters.AddWithValue("@a2", u.apellido2);
            comando.Parameters.AddWithValue("@dir", u.direccion);
            comando.Parameters.AddWithValue("@perfil", u.idperfil);
            comando.ExecuteNonQuery();
            desconectar();
        }

        public void eliminarrUsuario(Usuarios u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = " delete from usuarios  where nombreusuario=@user";
            comando.Parameters.AddWithValue("@user", u.nombreusuario);            
            comando.ExecuteNonQuery();
            desconectar();
        }


    }
}
