using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modelo
{
    public class Perfiles
    {
        private int myVar;
        private string myVar2;

        public string descripcion
        {
            get { return myVar2; }
            set { myVar2 = value; }
        }


        public int idperfil
        {
            get { return myVar; }
            set { myVar = value; }
        }



    }
}
