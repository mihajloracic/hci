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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIprojekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VrstaDao dao;
        Vrsta vrsta;
        Point startPoint = new Point();
        public MainWindow()
        {
            InitializeComponent();
            dao = new VrstaDao();
            //image.Source = new BitmapImage(new Uri("C:\\Users\\mihajlo\\Desktop\\HCIprojekat\\pictures\\Simple_world_map.svg"));
        }

        private void buttonAddVrsta_Click(object sender, RoutedEventArgs e)
        {
            FormVrsta formVrsta = new FormVrsta(this);
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

        private void buttonEtiketaVrsta_Click(object sender, RoutedEventArgs e)
        {
            DodajEtiketaVrsta formEtiektaVrsta = new DodajEtiketaVrsta();
            formEtiektaVrsta.ShowDialog();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DodajEtiketu formDodajEtiketu = new DodajEtiketu();
            formDodajEtiketu.ShowDialog();
        }

        private void menuItemVrsta_Click(object sender, RoutedEventArgs e)
        {
            FormVrsta formVrsta = new FormVrsta(this);
            formVrsta.ShowDialog();
        }

        private void menuItemVrsta1_Click(object sender, RoutedEventArgs e)
        {
            DodajTip formDodajTip = new DodajTip();
            formDodajTip.ShowDialog();
        }
        private void menuItemEtiketaVrsta_Click(object sender, RoutedEventArgs e)
        {
            DodajEtiketaVrsta formEtiektaVrsta = new DodajEtiketaVrsta();
            formEtiektaVrsta.ShowDialog();
        }
        

        private void dataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Get the dragged ListViewItem
                
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(vrsta.Slika));
                //DataGridTemplateColumn dataGridTemplateColumn =
                // FindAncestor<DataGridTemplateColumn>((DependencyObject)e.OriginalSource);


                // Find the data behind the ListViewItem
                //Image image = (Image)image.ItemContainerGenerator.
                // ItemFromContainer(dataGridTemplateColumn);

                // Initialize the drag & drop operation
                /*if (image.Source != null)
                {*/
                DataObject dragData = new DataObject("myFormat", image.Source);
                DragDrop.DoDragDrop(image, dragData, DragDropEffects.Link);
                //}
            }
        }

        internal void dodajSliku(string slika, int id)
        {
            ImageBrush image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri(slika, UriKind.Relative));

            Canvas c1 = new Canvas();
            c1.Background = image;
            c1.Width = 25;
            c1.Height = 25;

            //skladiste.Add(id, c1);

            Random r = new Random();
            double y = r.NextDouble() * (mapa.Height - 230);
            double x = r.NextDouble() * (mapa.Width - 230);


            Canvas.SetTop(c1, y);
            Canvas.SetLeft(c1, x);



            mapa.Children.Add(c1);


            c1.AllowDrop = true;
        }
        Point startPosition; //tacka koja predstavlja startnu poziciju kliknutog widgeta
        private Canvas draggedImage;
        private Point mousePosition;


        private void mapa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = e.Source as Canvas;

            if (image != null && mapa.CaptureMouse())
            {
                mousePosition = e.GetPosition(mapa);
                draggedImage = image;
                Panel.SetZIndex(draggedImage, 1);
            }
        }
        private void mapa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (draggedImage != null)
            {
                mapa.ReleaseMouseCapture();
                Panel.SetZIndex(draggedImage, 0);
                draggedImage = null;
            }
        }

        private void mapa_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedImage != null)
            {
                double canvasHeight = mapa.ActualHeight;
                double canvasWidth = mapa.ActualWidth;
                var position = e.GetPosition(mapa);
                var offset = position - startPosition;
                startPosition = position;

                double newLeft = position.X;
                double newTop = position.Y;

                if (newLeft < 0)
                    newLeft = 0;
                else if (newLeft + draggedImage.ActualWidth > canvasWidth)
                    newLeft = canvasWidth - draggedImage.ActualWidth;

                if (newTop < 0)
                    newTop = 0;
                else if (newTop + draggedImage.ActualHeight > canvasHeight)
                    newTop = canvasHeight - draggedImage.ActualHeight;

                Canvas.SetLeft(draggedImage, newLeft);
                Canvas.SetTop(draggedImage, newTop);


            }
        }
    }
}
