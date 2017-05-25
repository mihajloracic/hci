using HCIprojekat.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIprojekat.dao
{
    class EtiketaDao
    {
        static String myConnectionString = "server=localhost;uid=root;" + "pwd=admin;database=hci;";


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
                retVal.Add(etiketa);
                etiketa.Naziv = reader["naziv"].ToString();
                if(reader["boja"] != null)
                {
                    etiketa.Boja = Int32.Parse(reader["boja"].ToString());
                }

            }
            conn.Close();
            return retVal;
        }
        public bool checkDelete(int id)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) as broj FROM vrsta where id_etiketa = " + id, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (Int32.Parse(reader["broj"].ToString()) != 0)
            {
                return false;
            }
            return true;
            conn.Close();
        }
        public void deleteEtiketaOnly(int id)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE vrsta SET id_etiketa = null WHERE id_etiketa = " + id, conn);
            Console.WriteLine("KOMANDA delete from etiketa where = " + id);
            cmd.ExecuteReader();
            conn.Close();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
             cmd = new MySqlCommand("delete from etiketa where id = " + id, conn);
            Console.WriteLine("KOMANDA delete from etiketa where = " + id);
            cmd.ExecuteReader();
            conn.Close();
        }
        public void delete(int id)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from vrsta where id_etiketa = " + id, conn);
            cmd.ExecuteReader();
            conn.Close();
            deleteEtiketaOnly(id);
        }
        public void deleteFromVrsta(int id)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from vrsta where id_etiketa = " + id, conn);
            cmd.ExecuteReader();
            conn.Close();
            deleteEtiketaOnly(id);
        }
        public void insert(Etiketa e)
        {
            string command = "INSERT INTO etiketa VALUES(";
            command += e.Id + ",";
            command += "\"" + e.Naziv + "\",";
            command += e.Boja + ")";
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
            command += "boja = " + e.Boja;
            command += " where id = " + e.Id;
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(command, conn);
            cmd.ExecuteReader();
            conn.Close();
        }
        
    }
}
