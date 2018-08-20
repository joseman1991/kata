using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modelo
{
    public class Lectores
    {
        private string myVar;
        private string myV;
        private string myVar2;
        private string myVar3;
        private string myVar4;
        private string myVar5;
        private string myVar7;
        private int myVar6;

        public int idlectores
        {
            get { return myVar6; }
            set { myVar6 = value; }
        }


        public string clave
        {
            get { return myVar7; }
            set { myVar7 = value; }
        }


        public string direccion
        {
            get { return myVar3; }
            set { myVar3 = value; }
        }


        public string apellido2
        {
            get { return myVar4; }
            set { myVar4 = value; }
        }


        public string apellido1
        {
            get { return myVar2; }
            set { myVar2 = value; }
        }


        public string nombre2
        {
            get { return myVar5; }
            set { myVar5 = value; }
        }

        public string nombre1
        {
            get { return myV; }
            set { myV = value; }
        }


        public string telefono
        {
            get { return myVar; }
            set { myVar = value; }
        }
    }
}
