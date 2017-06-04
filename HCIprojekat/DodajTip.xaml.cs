using HCIprojekat.dao;
using HCIprojekat.model;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class DodajTip : Window
    {
        BitmapImage img;
        MySql.Data.MySqlClient.MySqlConnection conn;
        String myConnectionString = "server=localhost;uid=root;" + "pwd=root;database=hci;";
        TipVrstaDao dao = new TipVrstaDao();
        public DodajTip()
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
        private void upadteGrid()
        {
            List<TipVrsta> etikete = dao.readTipVrsta();
            dataGrid.ItemsSource = etikete;
  
        }
        private string addQutoes(string val)
        {
            return "\"" + val + "\",";
        }
        private void addTipVrsta(string naziv, string opis, string ikonica)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            conn.Open();
            string values = "VALUES (" + addQutoes(naziv) + addQutoes(opis) + "\""+ikonica + "\"" + ");";
            string komanda = "INSERT INTO tip_vrsta (ime,opis,ikonica)" + values;
            MySqlCommand cmd = new MySqlCommand(komanda, conn);
            Console.WriteLine("\nKOMANDA: " + komanda);
            cmd.ExecuteReader();
            conn.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tipVrsta == null)
                {
                    addTipVrsta(textBoxIme.Text, textBoxOpis.Text, image.Source.ToString());
                }
                else
                {
                    MessageBox.Show("Sad treba da se apdjetuje!");
                }
                cancel();
                upadteGrid();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Popunite sva polja");
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
        void deleteWarnning(int id)
        {
            string sMessageBoxText = "Ovo brisanje ce izbrisati i neke vrste da li zelite da nastavite?";
            string sCaption = "Upozorenje";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    dao.deleteFromVrsta(id);
                    /* ... */
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;
                    
            }
            cancel();
        }
        private void cancel()
        {
            textBoxIme.Text = "";
            textBoxOpis.Text = "";
            dataGrid.SelectedItem = null;
            img = null;
            image.Source = null;
            tipVrsta = null;
        }
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            TipVrsta tipVrsta = (TipVrsta)dataGrid.SelectedItem;
            if(tipVrsta != null)
            {
                try{
                    dao.deleteTipOnly(tipVrsta.Id);
                    cancel();
                }catch(Exception ex)
                {
                    deleteWarnning(tipVrsta.Id);
                }
                finally
                {
                    upadteGrid();
                }
                
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            cancel();
        }
        TipVrsta tipVrsta;
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                tipVrsta = (TipVrsta)dataGrid.SelectedItem;
                textBoxIme.Text = tipVrsta.Ime;
                textBoxOpis.Text = tipVrsta.Opis;
                image.Source = new BitmapImage(new Uri(tipVrsta.Ikonica));
            }

        }
    }
}
