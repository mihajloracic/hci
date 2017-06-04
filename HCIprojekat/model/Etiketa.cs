using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIprojekat.model
{
    class Etiketa
    {
        private int id;
        private string naziv;
        private string boja;
        public Etiketa()
        {

        }

        public Etiketa(string naziv, string boja)
        {
            this.naziv = naziv;
            this.Boja = boja;
        }

        

        public string Naziv
        {
            get
            {
                return naziv;
            }

            set
            {
                naziv = value;
            }
        }

        public string ToString()
        {

            return id + " " + naziv + " " + Boja;
        }
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Boja { get => boja; set => boja = value; }
    }
}
