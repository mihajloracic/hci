using HCIprojekat.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HCIprojekat.dao
{
    class EtiketaVrstaDao
    {

        static String myConnectionString = "server=localhost;uid=root;" + "pwd=root;database=hci;";
        MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);

        EtiketaDao etiketaDao;
        VrstaDao vrstaDao;

        public EtiketaVrstaDao()
        {
            etiketaDao = new EtiketaDao();
            vrstaDao = new VrstaDao();
        }

        public void insert(model.EtiketaVrsta etiketaVrsta)
        {
            string command = "INSERT INTO etiketa_vrsta(id_etiketa,id_vrsta) VALUES(";
            command += etiketaVrsta.FieldEtiketa.Id + ",";
            command += etiketaVrsta.FieldVrsta.Id  + ")";
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(command, conn);
            cmd.ExecuteReader();
            conn.Close();
        }
        public List<EtiketaVrsta> readEtikete()
        {
            List<EtiketaVrsta> retVal = new List<EtiketaVrsta>();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from etiketa_vrsta", conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                EtiketaVrsta etiketaVrsta = new EtiketaVrsta();
                etiketaVrsta.Id = Int32.Parse(reader["id"].ToString());
                etiketaVrsta.FieldEtiketa = etiketaDao.getById(Int32.Parse(reader["id_etiketa"].ToString()));
                etiketaVrsta.FieldVrsta = vrstaDao.getById(Int32.Parse(reader["id_vrsta"].ToString()));
                etiketaVrsta.NazivEtikete = etiketaVrsta.FieldEtiketa.Naziv;
                etiketaVrsta.NazivVrste = etiketaVrsta.FieldVrsta.Naziv;
                retVal.Add(etiketaVrsta);
            }
            conn.Close();
            return retVal;
        }
        public void delete(int id)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("delete from etiketa_vrsta where id = " + id, conn);
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
            }catch(Exception e)
            {

            }
            
        }
        void deleteVrsta(int id)
        {
            string sMessageBoxText = "UPOZORENJE. Brisanje ove vrste dovodi i do izmena u drugim tabelama. Da li želite da nastavite?";
            string sCaption = "Upozorenje";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    MySqlCommand cmd = new MySqlCommand("delete from vrsta where tip_vrste = " + id, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    delete(id);
                    /* ... */
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;
            }
        }

    }
}
