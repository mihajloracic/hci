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
    class EtiketaDao
    {
        static String myConnectionString = "server=localhost;uid=root;" + "pwd=root;database=hci;";


        MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);

        public List<Etiketa> readEtikete()
        {
            List<Etiketa> retVal = new List<Etiketa>();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from etiketa", conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Etiketa etiketa = new Etiketa();
                etiketa.Id = Int32.Parse(reader["id"].ToString());
                etiketa.Naziv = reader["naziv"].ToString();
                if(reader["boja"] != null)
                {
                    etiketa.Boja =reader["boja"].ToString();
                }
                retVal.Add(etiketa);
            }
            conn.Close();
            return retVal;
        }
        
        public void delete(int id)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from etiketa where id = " + id, conn);
            try
            {
                cmd.ExecuteReader();
            }catch(Exception e)
            {
                deleteEtiketaVrsta(id);
            }
            conn.Close();
        }
        
        public void insert(Etiketa e)
        {
            string command = "INSERT INTO etiketa(naziv,boja) VALUES(";
            command += "\"" + e.Naziv + "\",";
            command += "\"" + e.Boja + "\""+ ")";
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(command, conn);
            cmd.ExecuteReader();
            conn.Close();
        }
        public void update(Etiketa e)
        {
            string command = "UPDATE etiketa SET ";
            command += "naziv = \""+ e.Naziv + "\",";
            command += "boja = \"" + e.Boja + "\"";
            command += " where id = " + e.Id;
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(command, conn);
            cmd.ExecuteReader();
            conn.Close();
        }
        public Etiketa getById(int id)
        {
            Etiketa etiketa = new Etiketa();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from etiketa where id =" + id, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                etiketa.Id = Int32.Parse(reader["id"].ToString());
                etiketa.Naziv = reader["naziv"].ToString();
                etiketa.Boja = reader["boja"].ToString();
            }
            return etiketa;
        }
        void deleteEtiketaVrsta(int id)
        {
            string sMessageBoxText = "UPOZORENJE. Brisanje ove etikete dovodi i do izmena u drugi tabelama. Da li želite da nastavite?";
            string sCaption = "Upozorenje";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    MySqlCommand cmd = new MySqlCommand("delete from etiketa_vrsta where id_etiketa = " + id, conn);
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
