using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Drawing;


namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Firma> firma = new List<Firma>();
        public MainWindow()
        {
            InitializeComponent();
            LoadFirmaList();
        }


        private void LoadFirmaList()
        {
            firma = SQLiteDataAccess.LoadPeople();
            WiredUpPeople();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("dziala");
        }

        private void WiredUpPeople()
        {
            FirmaListBox.ItemsSource = null;
            FirmaListBox.ItemsSource = firma;

        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadFirmaList();
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            Window addUser = new addUser();
            addUser.Show();
        }

        private void Remove_User(object sender, RoutedEventArgs e)
        {
        //    if (FirmaListBox.SelectedItem != null)
        //    {
        //        //firma.Remove((Firma)FirmaListBox.SelectedItem);
        //       // WiredUpPeople();
        //        //SQLiteDataAccess.DeletePeople((Firma)FirmaListBox.SelectedItem);
        //    }
        }
    }
}