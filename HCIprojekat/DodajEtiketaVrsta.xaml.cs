using HCIprojekat.dao;
using HCIprojekat.model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DodajEtiketaVrsta.xaml
    /// </summary>
    public partial class DodajEtiketaVrsta : Window
    {
        EtiketaDao etiketaDao;
        VrstaDao vrstaDao;
        EtiketaVrstaDao dao;
        Etiketa etiketa;
        Vrsta vrsta;
        EtiketaVrsta etiketaVrsta;
        public DodajEtiketaVrsta()
        {
            InitializeComponent();
            etiketaDao = new EtiketaDao();
            vrstaDao = new VrstaDao();
            dao = new EtiketaVrstaDao();
            updateDataGrid();
        }
        public void updateDataGrid()
        {
            dataGrid.ItemsSource = dao.readEtikete();
            dataGridVrsta.ItemsSource = vrstaDao.readVrsta();
            dataGridEtiketa.ItemsSource = etiketaDao.readEtikete();
        }

        private void dataGridEtiketa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGridEtiketa.SelectedItem != null)
            {
                etiketa = (Etiketa)dataGridEtiketa.SelectedItem;
            }
        }

        private void dataGridVrsta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridVrsta.SelectedItem != null)
            {
                vrsta = (Vrsta)dataGridVrsta.SelectedItem;
            }
        }

        private void buttoAdd_Click(object sender, RoutedEventArgs e)
        {
            if (etiketa != null && vrsta != null)
            {
                EtiketaVrsta etiketaVrsta = new EtiketaVrsta();
                etiketaVrsta.FieldVrsta = vrsta;
                etiketaVrsta.FieldEtiketa = etiketa;
                dao.insert(etiketaVrsta);
                cancel();
            }
            else
            {
                MessageBox.Show("Molimo odaberite etiketu i vrstu");
            }
        
            updateDataGrid();
        }
        private void cancel()
        {
            etiketa = null;
            vrsta = null;
            etiketaVrsta = null;
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            cancel();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (etiketaVrsta != null)
            {
                dao.delete(etiketaVrsta.Id);
                cancel();
            }
            else
            {
                MessageBox.Show("Molimo odaberite Eitiketa-Vrstu koju želite da obrišete");
            }
            updateDataGrid();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                etiketaVrsta = (EtiketaVrsta)dataGrid.SelectedItem;
            }
        }
    }
}
