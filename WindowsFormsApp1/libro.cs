﻿using System;
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
    public partial class libro : Form
    {


        private int myVar;

        private LibrosDAO udao =new LibrosDAO();
        public libro()
        {
            InitializeComponent();
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
                        button3.Text = "Actualizar";
                    }
                    else
                    {
                        button3.Text = "Eliminar";
                    }
                    nu.Enabled = false;
                }
                myVar = value;
            }
        }


        private void addImagen()
        {
            pictureBox1.Image = Image.FromFile(@"recursos/sin_portada.png");
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Se crea el OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            // Se muestra al usuario esperando una acción
            DialogResult result = dialog.ShowDialog();

            // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
            // la mostramos en el PictureBox de la inferfaz
            if (result == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dialog.FileName);
            }
        }


        public void inicializarCodigo()
        {
            try
            {
                nu.Text = udao.idLibros().ToString();
            }
            catch (SqlNullValueException ex)
            {
                nu.Text = "1";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Libros u = new Libros();

            u.idlibro = int.Parse(nu.Text);
            u.titulo = n1.Text;
            u.editorial = n2.Text;
            u.autor = a1.Text;
            u.portada = new PictureBox();
            u.portada.Image = pictureBox1.Image;
           
            try
            {
                switch (opcion)
                {
                    case 0:
                        udao.registrarLector(u);
                        MessageBox.Show(this, "Lector registrado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        inicializarCodigo();
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
                if (dt != null)
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
                if (valor != null)
                {
                    Libros u = udao.ObtenerLibro(valor);
                    nu.Text = u.idlibro.ToString();
                    n1.Text = u.titulo;
                    n2.Text = u.editorial;
                    a1.Text = u.autor;
                    pictureBox1.Image = u.portada.Image;
                }
               

            }
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }




    }
}
