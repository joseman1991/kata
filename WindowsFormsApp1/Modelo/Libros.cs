using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Modelo
{
   public class Libros
    {
  
        private PictureBox myVar2;
        private string myVar3;
        private string myVar4;
        private string myVar5;
        private string myVar7;
        private int myVar6;

        public int idlibro
        {
            get { return myVar6; }
            set { myVar6 = value; }
        }


        public string titulo
        {
            get { return myVar7; }
            set { myVar7 = value; }
        }


        public string autor
        {
            get { return myVar3; }
            set { myVar3 = value; }
        }


        public string editorial
        {
            get { return myVar4; }
            set { myVar4 = value; }
        }


        public PictureBox portada
        {
            get { return myVar2; }
            set { myVar2 = value; }
        }


    }
}
