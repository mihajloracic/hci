using HCIprojekat.dao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HCIprojekat.model
{
    class VrstaDao
    {
        TipVrstaDao dao;
        MySql.Data.MySqlClient.MySqlConnection conn;
        String myConnectionString = "server=localhost;uid=root;" + "pwd=root;database=hci;";

        public VrstaDao()
        {
            dao = new TipVrstaDao();
        }
           
        public void update(Vrsta vrsta)
        {

        }

        public List<Vrsta> readVrsta()
        {
            List<Vrsta> retVal = new List<Vrsta>();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from vrsta", conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Vrsta vrsta = new Vrsta();
                vrsta.Id = Int32.Parse(reader["id"].ToString());
                vrsta.Naziv = reader["naziv"].ToString();
                vrsta.Opis = reader["opis"].ToString();               
                vrsta.TipVrsta = dao.getById(Int32.Parse(reader["tip_vrste"].ToString()));
                vrsta.Status_ugrozenosti = reader["status_ugrozenosti"].ToString();
                vrsta.Turisticki_prihod = Int32.Parse(reader["turisticki_prihod"].ToString());

                vrsta.Opasna = Int32.Parse(reader["opasna"].ToString()) == 0 ? false : true;
                vrsta.Iucn_lista = Int32.Parse(reader["iucn_lista"].ToString()) == 0 ? false : true;
                vrsta.Turisticki_status = reader["turisticki_status"].ToString();
                vrsta.KoristiRoditeljSliku = Int32.Parse(reader["flag"].ToString()) == 0 ? false : true;
                if (vrsta.KoristiRoditeljSliku != true)
                {
                    vrsta.Slika = reader["slika"].ToString();
                }
                else
                {
                    vrsta.Slika = vrsta.TipVrsta.Ikonica;
                }
                retVal.Add(vrsta);
            }
            conn.Close();
            return retVal;
        }
        public void deleteVrsta(int id)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();


            MySqlCommand cmd = new MySqlCommand("delete from vrsta where id = " + id, conn);
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();

            }catch(Exception e)
            {
                deleteEtiketaVrsta(id);
            }

            conn.Close();
        }
        public Vrsta getById(int id)
        {
            Vrsta vrsta = new Vrsta();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from vrsta where id =" + id, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                vrsta.Id = Int32.Parse(reader["id"].ToString());
                vrsta.Naziv = reader["naziv"].ToString();
            }
            return vrsta;
        }
        void deleteEtiketaVrsta(int id)
        {
            string sMessageBoxText = "UPOZORENJE. Brisanje ove vrste dovodi i do izmena u drugi tabelama. Da li želite da nastavite?";
            string sCaption = "Upozorenje";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    MySqlCommand cmd = new MySqlCommand("delete from etiketa_vrsta where id_vrsta = " + id, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    deleteVrsta(id);
                    /* ... */
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;
            }
        }
    }
}
