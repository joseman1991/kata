using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modelo
{
    public class LibrosDAO : Conexion
    {

        public Libros ObtenerLibro(string nu)
        {
            Libros u = null;
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = "select * from Libros where idLibro=@user";
            comando.Parameters.AddWithValue("@user", nu);
            lector = comando.ExecuteReader();
            if (lector.Read())
            {
                u = new Libros();
                int i = 0;
                u.idlibro = lector.GetInt32(i++);
                u.titulo = lector.GetString(i++);
                u.editorial = lector.GetString(i++);
                u.autor = lector.GetString(i++);
                byte[] imgData = (byte[])lector.GetValue(i++);
                Image newImage = null;
                // Trata la información de la imagen para poder trasladarla al picturebox
                using (MemoryStream ms = new MemoryStream(imgData, 0, imgData.Length))
                {
                    ms.Write(imgData, 0, imgData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                u.portada = new System.Windows.Forms.PictureBox();
                u.portada.Image = newImage;        

            }
            desconectar();
            return u;
        }

        public int idLibros()
        {
            int id = 1;
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = "select max(idLibro) from Libros";
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
            comando.CommandText = "select idlibro,nombre,editorial,autor from Libros where nombre like @nomb or autor like @ape";
            comando.Parameters.AddWithValue("@nomb", busca + "%");
            comando.Parameters.AddWithValue("@ape", busca + "%");
            lector = comando.ExecuteReader();
            tabla.Load(lector);
            desconectar();
            lector.Close();
            return tabla;
        }

        public void registrarLector(Libros u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = " INSERT INTO Libros values(@user,@n1,@n2,@a1,@dir)";
            comando.Parameters.AddWithValue("@user", u.idlibro);
            comando.Parameters.AddWithValue("@n1", u.titulo);
            comando.Parameters.AddWithValue("@n2", u.editorial);
            comando.Parameters.AddWithValue("@a1", u.autor);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();          
            u.portada.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);           
            comando.Parameters.AddWithValue("@dir", ms.GetBuffer());
            comando.ExecuteNonQuery();
            desconectar();
        }


        public void actualizarLector(Libros u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = " update Libros set  nombre=@n2,editorial=@a1,autor=@a2,foto=@dir where idlibro=@n1";            
            comando.Parameters.AddWithValue("@n1", u.idlibro);
            comando.Parameters.AddWithValue("@n2", u.titulo);
            comando.Parameters.AddWithValue("@a1", u.editorial);
            comando.Parameters.AddWithValue("@a2", u.autor);           
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            u.portada.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            comando.Parameters.AddWithValue("@dir", ms.GetBuffer());
            comando.ExecuteNonQuery();
            desconectar();
        }

        public void eliminarrLector(Libros u)
        {
            conetar();
            comando = conexion.CreateCommand();
            comando.CommandText = " delete from Libros  where idLibro=@user";
            comando.Parameters.AddWithValue("@user", u.idlibro);
            comando.ExecuteNonQuery();
            desconectar();
        }
    }
}
