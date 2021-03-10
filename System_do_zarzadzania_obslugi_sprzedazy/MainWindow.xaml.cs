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
        //private bool invoiceOpen=false;

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

        private void Invoice_Open(object sender, RoutedEventArgs e)
        {
            
            Invoice invoice=new Invoice();
            //if (invoiceOpen== false) 
            invoice.Show();
            //invoiceOpen=true;
            
            
        }

        private void Storage_Open(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage();
            storage.Show();
        }

        private void Settlements_Open(object sender, RoutedEventArgs e)
        {
            Settlements settlements = new Settlements();
            settlements.Show();
        }

        private void Contractors_Open(object sender, RoutedEventArgs e)
        {
            Contractors contractors = new Contractors();
            contractors.Show();
        }

        private void Statments_Open(object sender, RoutedEventArgs e)
        {
            Statments statments = new Statments();
            statments.Show();

        }

        private void VATRegister_Open(object sender, RoutedEventArgs e)
        {
            VATRegister vATRegister = new VATRegister();
            vATRegister.Show();
        }

        private void Print_Open(object sender, RoutedEventArgs e)
        {
            Print print = new Print();
            print.Show();
        }

        private void Search_Open(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.Show();
        }

        private void ToSquare_Open(object sender, RoutedEventArgs e)
        {
            ToSquare toSquare = new ToSquare();
            toSquare.Show();
        }
    }
}