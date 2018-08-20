using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Modelo;

namespace WindowsFormsApp1
{
    public partial class Lector : Form
    {


        private int myVar;





        private LectoresDAO udao;
        public Lector()
        {
            opcion = 0;
            InitializeComponent();
            udao = new LectoresDAO();
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
            pictureBox1.Image = Image.FromFile(@"recursos/lector.png");
            try
            {
               

                dataGridView1.DataSource = udao.obtenerRegistros("");
                this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                n1.Focus();
            }
            catch (SqlException e)
            {
                MessageBox.Show(this, "Error" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public void inicializarCodigo()
        {
            try
            {
                nu.Text = udao.idlectores().ToString();
            }
            catch (SqlNullValueException ex)
            {
                nu.Text = "1";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lectores u = new Lectores();

            u.idlectores =int.Parse(nu.Text);
            u.nombre1 = n1.Text;
            u.nombre2 = n2.Text;
            u.apellido1 = a1.Text;
            u.apellido2 = n2.Text;
            u.direccion = dir.Text;
            u.telefono = textBox1.Text;
            try
            {
                switch (opcion)
                {
                    case 0:
                        udao.registrarLector(u);
                        MessageBox.Show(this, "Lector registrado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case 1:
                        udao.actualizarLector(u);
                        MessageBox.Show(this, "Lector actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case 2:
                        udao.eliminarrLector(u);
                        MessageBox.Show(this, "Lector eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
                DataTable dt = (DataTable)dataGridView1.DataSource;
                if(dt!=null)
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
                Lectores u = udao.ObtenerLector(valor);
                nu.Text = u.idlectores.ToString();
                n1.Text = u.nombre1;
                n2.Text = u.nombre2;
                a1.Text = u.apellido1;
                a2.Text = u.apellido2;
                dir.Text = u.direccion;
                textBox1.Text = u.telefono;

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
