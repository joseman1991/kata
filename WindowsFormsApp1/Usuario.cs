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

    public partial class Usuario : Form
    {
        private int myVar;



        private List<Perfiles> listaPerfiles;
        private PerfilesDAO pdao;
        private UsuariosDAO udao;
        public Usuario()
        {
            opcion = 0;
            InitializeComponent();
            pdao = new PerfilesDAO();
            udao = new UsuariosDAO();
            addImagen();
        }

        public int opcion
        {
            get { return myVar; }
            set
            {
                if (value > 0)
                {
                    lblbusca.Show();
                    busca.Show();
                    if (value == 1)
                    {
                        button1.Text = "Actualizar";
                    }
                    else
                    {
                        button1.Text = "Eliminar";
                    }
                    nu.Enabled = false;
                }
                myVar = value;
            }
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
                    idp.Items.Add(listaPerfiles[i].descripcion);
                }
                idp.DropDownStyle = ComboBoxStyle.DropDownList;
                if (listaPerfiles.Count > 0)
                {
                    idp.SelectedIndex = 0;
                }
                dataGridView1.DataSource = udao.obtenerRegistros("");
                this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                n1.Focus();
            }
            catch (SqlException e)
            {
                MessageBox.Show(this, "Error" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Usuarios u = new Usuarios();
            u.clave = clave.Text;
            u.nombreusuario = nu.Text;
            u.nombre1 = n1.Text;
            u.nombre2 = n2.Text;
            u.apellido1 = a1.Text;
            u.apellido2 = n2.Text;
            u.direccion = dir.Text;
            u.idperfil = listaPerfiles[idp.SelectedIndex].idperfil;
            try
            {
                switch (opcion)
                {
                    case 0:
                        udao.registrarUsuario(u);
                        MessageBox.Show(this, "Usuario registrado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case 1:
                        udao.actualizarUsuario(u);
                        MessageBox.Show(this, "Usuario actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case 2:
                        udao.eliminarrUsuario(u);
                        MessageBox.Show(this, "Usuario eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
                DataTable dt = (DataTable)dataGridView1.DataSource;
                dt.Clear();
                dataGridView1.DataSource = udao.obtenerRegistros("");
                this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, "Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (opcion > 0)
            {

                string valor = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                Usuarios u = udao.ObtenerUsuario(valor);
                clave.Text = u.clave;
                nu.Text = u.nombreusuario;
                n1.Text = u.nombre1;
                n2.Text = u.nombre2;
                a1.Text = u.apellido1;
                a2.Text = u.apellido2;
                dir.Text = u.direccion;
                for (int i = 0; i < idp.Items.Count; i++)
                {
                    if (listaPerfiles[i].idperfil == u.idperfil)
                    {
                        idp.SelectedItem = listaPerfiles[i].descripcion;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                dt.Clear();
                dataGridView1.DataSource = udao.obtenerRegistros(busca.Text);
                this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, "Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
