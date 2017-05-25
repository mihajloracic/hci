using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCIprojekat
{
    /// <summary>
    /// Interaction logic for FormVrsta.xaml
    /// </summary>
    public partial class FormVrsta : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    
        BitmapImage img;
        MySql.Data.MySqlClient.MySqlConnection conn;
        String myConnectionString = "server=localhost;uid=root;" +"pwd=admin;database=hci;";

        private static void DisplayData(System.Data.DataTable table)
        {
            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }
                Console.WriteLine("============================");
            }
        }
        public FormVrsta()
        {
            InitializeComponent();

            try
            {
                upadteGrid();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private string addQutoes(string val)
        {
            return "\"" + val + "\",";
        }
        private void addVrsta(string naziv,string opis, int tip_vrste, string status_ugrozenosti,int turisticki_prihod,string slika, int opasna, int vrsta_col,int iucn_lista,string turisticki_status)
        {
           
            slika = image.Source.ToString();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            string values = "VALUES (" + addQutoes(naziv) + addQutoes(opis) + tip_vrste + "," +addQutoes(status_ugrozenosti)+  turisticki_prihod + "," + addQutoes(slika) + opasna + "," + vrsta_col + "," + iucn_lista + ",\"" + turisticki_status +  "\");";
            string komanda = "INSERT INTO vrsta (naziv,opis,tip_vrste,status_ugrozenosti,turisticki_prihod,slika,opasna,vrstacol,iucn_lista,turisticki_status)" + values;
            MySqlCommand cmd = new MySqlCommand(komanda, conn);
            Console.WriteLine("\nKOMANDA: " + komanda);
            cmd.ExecuteReader();
            conn.Close();
        }
        Dictionary<String, String> tipip = new Dictionary<string, string>();
        private void updateTipVrste()
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);

            conn.Open();
            MySqlCommand sc = new MySqlCommand("select id,ime from tip_vrsta", conn);
            MySqlDataReader reader;
     
            try
            {
                
                reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    tipip.Add(reader["ime"].ToString(), reader["id"].ToString());
                    comboBoxTip.Items.Add(reader["ime"].ToString());
                    //comboBoxTip. = reader["FleetID"].ToString();
                    //comboBoxTip.DisplayMember = reader["FleetName"].ToString();
                }
            }
            catch
            {
                
            }
            finally
            {
                conn.Close();
            }
        }
        private void upadteGrid()
        {
            updateTipVrste();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from vrsta", conn);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());
            //DisplayData(table);
            conn.Close();
            dataGrid.DataContext = table;
        }
        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                //private void addVrsta(string naziv,string opis, 
                //int tip_vrste, string status_ugrozenosti,
                //int turisticki_prihod,string slika, int opasna, int vrsta_col,int ,int turisticki_status)
                addVrsta(textBoxIme.Text,//naziv
                    textBoxOpis.Text, //opis
                    Int32.Parse(tipip[comboBoxTip.SelectedItem.ToString()]), //tip_vrste
                    radioButtonOpasnaTrue.IsChecked == true ? "ugrozena" : "neugrozena", //status_ugrozenosti
                    Int32.Parse(textBoxPrihod.Text), //tusristicki prihod
                    null,  //slika
                    1,  // opasna
                    123, // vrsta_col
                    radioButtonIucnTrue.IsChecked == true ? 1 : 0, // iucn_lista 
                    comboBoxTuristickiStatus.Text);//turisticki_status
                upadteGrid();
                removeFields();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Molimo popunite sve vrednosti");
            }
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.FileName != null)
                {
                    img = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
            image.Source = img;
        }

        private void radioButtonIucnTrue_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void removeFields()
        {
            textBoxIme.Text = "";
            textBoxOpis.Text = "";
            textBoxPrihod.Text = "";
            image.Source = null; 
        }
        private void radioButtonOpasnaTrue_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void comboBoxTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        class Row
        {
            public string Tipvrsta { get; set; }
        }
        void delte()
        {
            string result = dataGrid.ToString();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            Console.WriteLine("\nCOMMAND: "+ "delete from vrsta where tip_vrste=" + result);
            MySqlCommand cmd = new MySqlCommand("delete from vrsta where tip_vrste="+result, conn);
            cmd.ExecuteReader();
            //DisplayData(table);
            conn.Close();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            delte();
            upadteGrid();
        }
        double cena=0;
        public double Cena
        {
            get
            {
                return cena;
            }
            set
            {
                if (value != cena)
                {
                    cena = value;
                    OnPropertyChanged("Cena");
                }
            }
        }
    }
}
