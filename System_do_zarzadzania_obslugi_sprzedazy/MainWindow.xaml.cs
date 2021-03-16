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
        List<Seller> sellers = new List<Seller>();

        //private bool invoiceOpen=false;
        private Invoice invoice;
        private Storage storage;
        private Settlements settlements;
        private Contractors contractors;
        private Statments statments;
        private VATRegister vATRegister;
        private Print print;
        private Search search;
        private ToSquare toSquare;


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

        private void WiredUpSellersList()
        {
            FirmaListBox.ItemsSource = null;
            FirmaListBox.ItemsSource = sellers;
        }

        private void LoadSellersList()
        {
            sellers = SQLiteDataAccess.LoadSellers();
            WiredUpSellersList();
      
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
           if (FirmaListBox.SelectedItem != null)
            {
              SQLiteDataAccess.DeleteUsers((Company)FirmaListBox.SelectedItem);
              companies.Remove((Company)FirmaListBox.SelectedItem);
              WiredUpCompaniesList();
            }
        }

        private void Invoice_Open(object sender, RoutedEventArgs e)
        {         
            ListOfInvoices listOfInvoices = new ListOfInvoices();
            listOfInvoices.Show();
        }

        private void Storage_Open(object sender, RoutedEventArgs e)
        {
            storage = new Storage();
            storage.Show();
        }

        private void Settlements_Open(object sender, RoutedEventArgs e)
        {
            settlements = new Settlements();
            settlements.Show();
        }

        private void Contractors_Open(object sender, RoutedEventArgs e)
        {
            ListOfSellers listOfSellers = new ListOfSellers();
            listOfSellers.Show();
        }

        private void Statments_Open(object sender, RoutedEventArgs e)
        {
            statments = new Statments();
            statments.Show();
        }

        private void VATRegister_Open(object sender, RoutedEventArgs e)
        {
            vATRegister = new VATRegister();
            vATRegister.Show();
        }

        private void Print_Open(object sender, RoutedEventArgs e)
        {
            print = new Print();
            print.Show();
        }

        private void Search_Open(object sender, RoutedEventArgs e)
        {
            search = new Search();
            search.Show();
        }

        private void ToSquare_Open(object sender, RoutedEventArgs e)
        {
            toSquare = new ToSquare();
            toSquare.Show();
        }

        private void FirmaListBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {
        }
    }
}