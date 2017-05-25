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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIprojekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonAddVrsta_Click(object sender, RoutedEventArgs e)
        {
            FormVrsta formVrsta = new FormVrsta();
            formVrsta.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DodajTip formDodajTip = new DodajTip();
            formDodajTip.ShowDialog();
        }

        private void buttonEtiketa_Click(object sender, RoutedEventArgs e)
        {
            DodajEtiketu formDodajEtiketu = new DodajEtiketu();
            formDodajEtiketu.ShowDialog();
        }
    }
}
