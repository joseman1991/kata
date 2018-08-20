using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp1
{
    public partial class Bienvanido : Form
    {
        public Bienvanido()
        {
            InitializeComponent();
            addImagen();
        }

        public Bienvanido(Object a, EventHandler e)
        {
            InitializeComponent();
            addImagen();

        }      

       

        private void addImagen()
        {
            pictureBox1.ImageLocation = "recursos/libro.png";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void añadirUnNuevoUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();            
            user.ShowDialog();
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            user.opcion = 1;
            user.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            user.opcion = 2;
            user.ShowDialog();
        }

        private void añdirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lector lector = new Lector();        
            lector.ShowDialog();
        }

        private void actualizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Lector lector = new Lector();
            lector.opcion = 1;
            lector.inicializarCodigo();
            lector.ShowDialog();
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Lector lector = new Lector();
            lector.opcion = 2;
            lector.inicializarCodigo();
            lector.ShowDialog();
        }
    }
}
