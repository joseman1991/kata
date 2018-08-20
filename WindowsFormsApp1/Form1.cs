using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Modelo;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Perfiles> listaPerfiles;
        private PerfilesDAO pdao;
        private UsuariosDAO udao;
        public Form1()
        {
            InitializeComponent();
            pdao = new PerfilesDAO();
            udao = new UsuariosDAO();
            addImagen();
            user.Text = "kata";
            clave.Text = "123456";
        }


        private void addImagen()
        {
            pictureBox1.Image = Image.FromFile(@"recursos/log.png");
            try
            {
                pdao.obtenerPerfiles();
                listaPerfiles = pdao.ListaPerfiles;
                for (int i = 0; i < listaPerfiles.Count; i++)
                {
                    comboBox1.Items.Add(listaPerfiles[i].descripcion);
                }
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                if (listaPerfiles.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
                user.Focus();
            }
            catch (SqlException e)
            {
                MessageBox.Show(this, "Error" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void eventoCerrar(object senser, FormClosedEventArgs e)
        {
            this.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Usuarios u = new Usuarios();
            u.nombreusuario = user.Text;
            u.clave = clave.Text;
            u.idperfil = listaPerfiles[comboBox1.SelectedIndex].idperfil;
            try
            {
                u = udao.login(u);
                if (u != null)
                {                    
                    if (u.idperfil == 0)
                    {
                        MessageBox.Show(this, "Error " + "No tienes este perfil asignado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (u.clave!=null)
                    {
                        Bienvanido b = new Bienvanido();
                        b.FormClosed += new FormClosedEventHandler(eventoCerrar);
                        this.Hide();
                        user.Clear();
                        clave.Clear();
                        b.Show();                        
                    }
                    else
                    {
                        MessageBox.Show(this, "Error " + "Contraseña incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Error" + "Usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, "Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }           

        }
    }
}
