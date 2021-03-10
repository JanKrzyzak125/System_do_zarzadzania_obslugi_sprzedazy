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
        List<Company> companies = new List<Company>();
        public MainWindow()
        {
            InitializeComponent();
            LoadCompaniesList();
        }


        private void LoadCompaniesList()
        {
            companies = SQLiteDataAccess.LoadUsers();
            WiredUpCompaniesList();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("dziala");
        }

        private void WiredUpCompaniesList()
        {
            FirmaListBox.ItemsSource = null;
            FirmaListBox.ItemsSource = companies;

        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCompaniesList();
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            addUser addUser = new addUser();
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