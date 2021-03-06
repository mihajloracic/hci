﻿using HCIprojekat.dao;
using HCIprojekat.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DodajEtiketu.xaml
    /// </summary>
    public partial class DodajEtiketu : Window
    {
        Etiketa etiketa;
        EtiketaDao dao = new EtiketaDao();
        public DodajEtiketu()
        {
            InitializeComponent();
            updateDataGrid();
            cancel();
        }
    
        void updateDataGrid()
        {
            List<Etiketa> etikete = dao.readEtikete();
            dataGrid.ItemsSource = etikete;
            cancel();
        }       
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Etiketa)dataGrid.SelectedItem == null)
                return;
            try
            {
                etiketa = (Etiketa)dataGrid.SelectedItem;
                Console.WriteLine(etiketa.ToString());
                textBoxNaziv.Text = etiketa.Naziv;
            }
            catch (Exception er)
            {
                Console.Write(er.ToString());
                textBoxNaziv.Text = "";
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberite ajtem koji treba da se izbrise");
            }else
            {
                etiketa = (Etiketa)dataGrid.SelectedItem;
               
                dao.delete(etiketa.Id);
                
            }
            updateDataGrid();
            cancel();
        }
        void delteWarrning()
        {
            string sMessageBoxText = "Da li zelite da izbrisete samo etikete ili i vrste vezane za tu etiketu";
            string sCaption = "Upozorenje";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    
                    /* ... */
                    break;

                case MessageBoxResult.No:
                 
                    /* ... */
                    break;

                case MessageBoxResult.Cancel:
                    /* ... */
                    break;
            }
            cancel();
        }
        void updateWarning(Etiketa e)
        {
            string sMessageBoxText = "Da li zelite izmenite etiketu, ove izmene ce uticati na eitkete u tabeli vrste";
            string sCaption = "Upozorenje";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    dao.update(etiketa);
                    /* ... */
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;
            }
            cancel();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (bojaBirac.SelectedColor.Value.ToString() == "" || textBoxNaziv.Text == "")
            {
                return;
            }
            if(etiketa == null)
            {
                dao.insert(new Etiketa(textBoxNaziv.Text, bojaBirac.SelectedColor.Value.ToString()));
            }
            else
            {
                etiketa.Naziv = textBoxNaziv.Text;
                etiketa.Boja = bojaBirac.SelectedColor.Value.ToString();
                dao.update(etiketa);
            }
            updateDataGrid();
        }
        public void cancel()
        {
            textBoxNaziv.Text = "";
            this.etiketa = null;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            cancel();
        }
    }
}
