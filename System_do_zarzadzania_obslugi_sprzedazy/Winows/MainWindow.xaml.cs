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
using System.ComponentModel;
using System_do_zarzadzania_obslugi_sprzedazy.Winows;


namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Company> companies = new List<Company>();
        List<Seller> sellers = new List<Seller>();
        List<BaseInvoice> baseInvoices = new List<BaseInvoice>();
        List<Invoice> invoices = new List<Invoice>();
        List<Product> products = new List<Product>();
        

        private bool invoiceOpen=true;
        private bool StorageOpen = false;
        private bool SettlementsOpen = false;
        private bool ContractorsOpen = false;
        private bool StatmentsOpen = false;
        private bool VATRegisterOpen = false;

        private int companyID = 1;


        public MainWindow()
        {
            InitializeComponent();
            LoadInvoicesList();
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
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = companies;

        }

        
        private void WiredUpSellersList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = sellers;
        }

        private void WiredUpInvoicesList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = baseInvoices;
        }
        private void WiredUpProductList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = products;
        }
        private void LoadInvoicesList()
        {
            invoices = SQLiteDataAccess.LoadInvoices();
            baseInvoices = invoices.ConvertAll(x => (BaseInvoice)x);
            WiredUpInvoicesList();
        }

        private void LoadSellersList()
        {
            sellers = SQLiteDataAccess.LoadSellers();
            WiredUpSellersList();
      
        }
        private void LoadProductsList()
        {
            products = SQLiteDataAccess.LoadProducts(companyID);
            WiredUpProductList();

        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            //addUser addUser = new addUser();
            //addUser.Show();
            //addUser.Closed += (s, eventarg) =>
            //{
            //    LoadCompaniesList();
            //}
            if (invoiceOpen)
            {
                NewInvoice newInvoice = new NewInvoice();
                newInvoice.Show();
                newInvoice.Closed += (s, eventarg) =>
                {
                    LoadInvoicesList();
                };
            }
            if(StorageOpen)
            {
                AddNewProductMagazine addNewProductMagazine = new AddNewProductMagazine();
                addNewProductMagazine.Show();
                addNewProductMagazine.Closed += (s, eventarg) =>
                {
                    LoadProductsList();
                };
            }
            if(SettlementsOpen)
            {
                
            }
            if(ContractorsOpen)
            {

            }
            if(StatmentsOpen)
            {

            }
            if(VATRegisterOpen)
            {

            }
          
        }

        private void Remove_User(object sender, RoutedEventArgs e)
        {
            if (invoiceOpen)
            {
                if (CompanyDataGrid.SelectedItem != null)
                {


                    SQLiteDataAccess.DeleteInvoice((Invoice)CompanyDataGrid.SelectedItem);
                    invoices.Remove((Invoice)CompanyDataGrid.SelectedItem);
                    LoadInvoicesList();
                }
            }
            if (StorageOpen)
            {
                if (CompanyDataGrid.SelectedItem != null)
                {


                    SQLiteDataAccess.DeleteProduct((Product)CompanyDataGrid.SelectedItem);
                    products.Remove((Product)CompanyDataGrid.SelectedItem);
                    LoadProductsList();
                }

            }
            if (SettlementsOpen)
            {

            }
            if (ContractorsOpen)
            {

            }
            if (StatmentsOpen)
            {

            }
            if (VATRegisterOpen)
            {

            }


            
        }

        private void Invoice_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            invoiceOpen = true;
            AddUser.Content = "Dodaj fakture";
            RemoveUser.Content = "Usuń fakture";
            LoadInvoicesList();

        }

        private void Storage_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            StorageOpen = true;
            AddUser.Content = "Dodaj przedmiot";
            RemoveUser.Content = "Usuń przedmiot";
            CompanyDataGrid.ItemsSource = null;
            LoadProductsList();
        }

        private void Settlements_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            SettlementsOpen = true;
            AddUser.Content = "Dodaj rozliczenie";
            RemoveUser.Content = "Usuń rozliczenie";
            CompanyDataGrid.ItemsSource = null;
        }

        private void Contractors_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            ContractorsOpen = true;
            AddUser.Content = "Dodaj Kontrahenta";
            RemoveUser.Content = "Usuń Kontrahenta";
            LoadSellersList();
        }

        private void Statments_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            StatmentsOpen = true;
            AddUser.Content = "Dodaj sprawozdanie";
            RemoveUser.Content = "Usuń sprawozdanie";
            CompanyDataGrid.ItemsSource = null;
        }

        private void VATRegister_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            VATRegisterOpen = true;
            AddUser.Content = "Dodaj rejestr vat";
            RemoveUser.Content = "Usuń rejestr vat";

            CompanyDataGrid.ItemsSource = null;
        }

        private void Print_Open(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Open(object sender, RoutedEventArgs e)
        {

        }

        private void CompanyDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
        }

        private void DoubleClick_Open(object sender, MouseButtonEventArgs e)
        {
            if (CompanyDataGrid.SelectedItem != null && invoiceOpen)
            {
                Invoice inv = CompanyDataGrid.SelectedItem as Invoice;
                InvoiceDetails invoiceDetails = new InvoiceDetails(inv,inv.Id, inv.IdCompany);
                invoiceDetails.Show();
            }
            else
            {
                MessageBox.Show("Wybierz pozycję z listy!");
            }
        }

        private void SettingToFalse()
        {
              invoiceOpen = false;
              StorageOpen = false;
              SettlementsOpen = false;
              ContractorsOpen = false;
              StatmentsOpen = false;
              VATRegisterOpen = false;
        }

       
    }
}