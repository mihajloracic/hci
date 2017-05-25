using HCIprojekat.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIprojekat.dao
{
    class TipVrstaDao
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        String myConnectionString = "server=localhost;uid=root;" + "pwd=admin;database=hci;";
        public List<TipVrsta> readTipVrsta()
        {
            List<TipVrsta> retVal = new List<TipVrsta>();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from tip_vrsta", conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TipVrsta tipVrsta = new TipVrsta();
                tipVrsta.Id = Int32.Parse(reader["id"].ToString());
                tipVrsta.Ime = reader["ime"].ToString();
                tipVrsta.Opis = reader["opis"].ToString();
                tipVrsta.Ikonica = reader["ikonica"].ToString();
                retVal.Add(tipVrsta);
            }
            conn.Close();
            return retVal;
        }
        public void deleteTipOnly(int id)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE vrsta SET tip_vrste = null WHERE tip_vrste = " + id, conn);
            Console.WriteLine("KOMANDA delete from etiketa where = " + id);
            cmd.ExecuteReader();
            conn.Close();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            cmd = new MySqlCommand("delete from tip_vrsta where id = " + id, conn);
            Console.WriteLine("KOMANDA delete from etiketa where = " + id);
            cmd.ExecuteReader();
            conn.Close();
        }
        public void deleteFromVrsta(int id)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from vrsta where tip_vrste = " + id, conn);
            cmd.ExecuteReader();
            conn.Close();
            deleteTipOnly(id);
        }
        
    }
}
