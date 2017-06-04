using HCIprojekat.dao;
using HCIprojekat.model;
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
        VrstaDao dao;
        Vrsta vrsta;
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
        String myConnectionString = "server=localhost;uid=root;" +"pwd=root;database=hci;";

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
        MainWindow parent;
        public FormVrsta(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            dataGrid.DataContext = this;
            dao = new VrstaDao();
            try
            {
                updateGrid();
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
        private void addVrsta(string naziv,string opis, int tip_vrste, string status_ugrozenosti,int turisticki_prihod,string slika, int opasna, int vrsta_col,int iucn_lista,string turisticki_status, int flag)
        {
           
            slika = image.Source.ToString();
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            string values = "VALUES (" + addQutoes(naziv) + addQutoes(opis) + tip_vrste + "," +addQutoes(status_ugrozenosti)+  turisticki_prihod + "," + addQutoes(slika) + opasna  + "," + iucn_lista + ",\"" + turisticki_status + "\"," + flag +  ");";
            string komanda = "INSERT INTO vrsta (naziv,opis,tip_vrste,status_ugrozenosti,turisticki_prihod,slika,opasna,iucn_lista,turisticki_status,flag)" + values;
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
        private void updateGrid()
        {
            updateTipVrste();

            
            dataGrid.ItemsSource = dao.readVrsta();
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
                
                if (vrsta == null)
                {
                    addVrsta(textBoxIme.Text,//naziv
                    textBoxOpis.Text, //opis
                    Int32.Parse(tipip[comboBoxTip.SelectedItem.ToString()]), //tip_vrste
                    radioButtonOpasnaTrue.IsChecked == true ? "ugrozena" : "neugrozena", //status_ugrozenosti
                    Int32.Parse(textBoxPrihod.Text), //tusristicki prihod
                    null,  //slika
                    radioButtonOpasnaTrue.IsChecked == true ? 1 : 0,  // opasna
                    123, // vrsta_col
                    radioButtonIucnTrue.IsChecked == true ? 1 : 0, // iucn_lista 
                    comboBoxTuristickiStatus.Text,//turiticki status
                    checkboxUseParent.IsChecked == true ? 1 : 0);//flag za sliku
                    string path = ((BitmapImage)image.Source).UriSource.AbsolutePath;
                    List<Vrsta> pomocnaLista = dao.readVrsta();
                    Vrsta vp = pomocnaLista.ElementAt(pomocnaLista.Count - 1);
                    ((MainWindow)parent).dodajSliku(vp);

                }
                else
                {
                    vrsta.Slika = image.Source.ToString();
                    dao.update(vrsta);
                }
                //turisticki_status
                updateGrid();
                cancel();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            checkboxUseParent.IsChecked = false;
        }

        private void radioButtonIucnTrue_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void cancel()
        {
            textBoxIme.Text = "";
            textBoxOpis.Text = "";
            textBoxPrihod.Text = "";
            image.Source = null;
            vrsta = null;
            radioButton1IucnFalse.IsChecked = false;
            radioButton1OpasnaFalse.IsChecked = false;
            radioButtonIucnTrue.IsChecked = false;
            radioButtonOpasnaTrue.IsChecked = false;
            checkboxUseParent.IsChecked = false;
        }
        private void radioButtonOpasnaTrue_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void comboBoxTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipVrstaDao tipDao = new TipVrstaDao();
            TipVrsta pomocna = tipDao.getById(Int32.Parse(tipip[comboBoxTip.SelectedItem.ToString()]));
            image.Source = new BitmapImage(new Uri(pomocna.Ikonica));
            checkboxUseParent.IsChecked = true;
        }
        class Row
        {
            public string Tipvrsta { get; set; }
        }
        
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            dao.deleteVrsta(vrsta.Id);
            cancel();
            updateGrid();
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

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                vrsta = (Vrsta)dataGrid.SelectedItem;
                textBoxIme.Text = vrsta.Naziv;
                textBoxOpis.Text = vrsta.Opis;
                textBoxPrihod.Text = vrsta.Turisticki_prihod.ToString();
                if(vrsta.Opasna == true)
                {
                    radioButtonOpasnaTrue.IsChecked = true;
                }
                else
                {
                    radioButton1OpasnaFalse.IsChecked = true;
                }
                if(vrsta.Iucn_lista == true)
                {
                    radioButtonIucnTrue.IsChecked = true;
                }
                else
                {
                    radioButton1IucnFalse.IsChecked = true;
                }
                comboBoxTip.SelectedIndex = findIndex(comboBoxTip, vrsta.TipVrsta.Ime);
                comboBoxStatus.SelectedItem = selectedItem(comboBoxStatus, vrsta.Status_ugrozenosti);
                comboBoxTuristickiStatus.SelectedItem = selectedItem(comboBoxTuristickiStatus, vrsta.Turisticki_status);
                image.Source = new BitmapImage(new Uri(vrsta.Slika));
            }
        }
        private int findIndex(ComboBox myComboBox, String val)
        {
            int postion = 0;
            foreach (var item in myComboBox.Items)
            {
                if (item.ToString().Equals(val))
                {
                    return postion;
                }
                postion++;
            }
            return 0;
        }
        private string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
        private ComboBoxItem selectedItem(ComboBox myComboBox, String val)
        {
            val = RemoveWhitespace(val);
            int postion = 0;
            foreach (ComboBoxItem item in myComboBox.Items)
            {
                string cmpThis = RemoveWhitespace(item.Content.ToString());
                if (cmpThis.Equals(val))
                {
                    return item;
                }
            }
            return null;
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            cancel();
        }

        private void textBoxOpis_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
