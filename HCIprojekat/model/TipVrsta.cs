using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIprojekat.model
{
    class TipVrsta
    {
        int id;
        string ime;
        string opis;
        string ikonica;

        public TipVrsta(int id, string ime, string opis, string ikonica)
        {
            this.id = id;
            this.ime = ime;
            this.opis = opis;
            this.ikonica = ikonica;
        }
        public TipVrsta()
        {

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

        public string Ime
        {
            get
            {
                return ime;
            }

            set
            {
                ime = value;
            }
        }

        public string Opis
        {
            get
            {
                return opis;
            }

            set
            {
                opis = value;
            }
        }

        public string Ikonica
        {
            get
            {
                return ikonica;
            }

            set
            {
                ikonica = value;
            }
        }
    }
}
