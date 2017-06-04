using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIprojekat.model
{
    class Vrsta
    {
        int id;
        string naziv;
        string opis;
        TipVrsta tipVrsta;
        string status_ugrozenosti;
        int turisticki_prihod;
        string slika;
        bool opasna;
        bool iucn_lista;
        string turisticki_status;
        bool koristiRoditeljSliku;

        public Vrsta()
        {

        }

        

        public int Id { get => id; set => id = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public string Opis { get => opis; set => opis = value; }
        public string Status_ugrozenosti { get => status_ugrozenosti; set => status_ugrozenosti = value; }
        public int Turisticki_prihod { get => turisticki_prihod; set => turisticki_prihod = value; }
        public string Slika { get => slika; set => slika = value; }
        public bool Opasna { get => opasna; set => opasna = value; }
        public bool Iucn_lista { get => iucn_lista; set => iucn_lista = value; }
        public string Turisticki_status { get => turisticki_status; set => turisticki_status = value; }
        public bool KoristiRoditeljSliku { get => koristiRoditeljSliku; set => koristiRoditeljSliku = value; }
        internal TipVrsta TipVrsta { get => tipVrsta; set => tipVrsta = value; }
    }
}
