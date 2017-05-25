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
        private int boja;
        public Etiketa()
        {

        }

        public Etiketa(int id, string naziv, int boja)
        {
            this.id = id;
            this.naziv = naziv;
            this.boja = boja;
        }

        public int Boja
        {
            get
            {
                return boja;
            }

            set
            {
                boja = value;
            }
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

            return id + " " + naziv + " " + boja;
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
    }
}
