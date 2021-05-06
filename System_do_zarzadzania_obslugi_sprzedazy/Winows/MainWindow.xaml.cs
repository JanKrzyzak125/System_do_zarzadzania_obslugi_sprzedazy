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
using System_do_zarzadzania_obslugi_sprzedazy.Classes;


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
        List<BaseInvoice> invoiceStatements = new List<BaseInvoice>();
        List<Invoice> invoices = new List<Invoice>();
        List<Product> products = new List<Product>();
        List<StorageOperations> storage = new List<StorageOperations>();
        List<string> operations = new List<string>();
        List<Debter> debters = new List<Debter>();
        

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
            ShowControls();
            FillOperations();
            LoadInvoicesList();
            OperationCB.ItemsSource = operations;
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
        private void FillOperations()
        {
            operations.Add("Przyjęcie wewnętrzne");
            operations.Add("Przyjęcie zewnętrzne");
            operations.Add("Wydanie wewnętrzne");
            operations.Add("Wydanie zewnętrzne");
            operations.Add("Wybór wszystkich");
        }

        
        private void WiredUpSellersList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = sellers;
        }

        private void WiredUpInvoicesList()
        {
            CompanyDataGrid.ItemsSource = null;
            StorageDG.ItemsSource = null;
            CompanyDataGrid.ItemsSource = baseInvoices;
            InvoiceDG.ItemsSource = invoiceStatements;
        }
        private void WiredUpProductList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = products;
        }

        private void WiredUpStorageList()
        {
            StorageDG.ItemsSource = null;
            StorageDG.ItemsSource = storage;
        }

        private void LoadStorageList()
        {
            storage = SQLiteDataAccess.LoadOperations();
            WiredUpStorageList();
        }
        private void LoadInvoicesList()
        {
            invoices = SQLiteDataAccess.LoadInvoices();
            baseInvoices = invoices.ConvertAll(x => (BaseInvoice)x);
            invoiceStatements = invoices.ConvertAll(x => (BaseInvoice)x);
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

        private void LoadHistoryList() 
        {

            
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
                AddNewSeller addNewSeller = new AddNewSeller();
                addNewSeller.Show();
                addNewSeller.Closed += (s, eventarg) =>
                {
                    LoadSellersList();
                };
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
                if (CompanyDataGrid.SelectedItem != null)
                {


                    SQLiteDataAccess.DeleteContractors((Seller)CompanyDataGrid.SelectedItem);
                    sellers.Remove((Seller)CompanyDataGrid.SelectedItem);
                    LoadSellersList();
                }
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
            ShowControls();

        }

        private void Storage_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            StorageOpen = true;
            AddUser.Content = "Dodaj przedmiot";
            RemoveUser.Content = "Usuń przedmiot";
            CompanyDataGrid.ItemsSource = null;
            LoadProductsList();
            ShowControls();
        }

        private void Settlements_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            SettlementsOpen = true;
            AddUser.Content = "Dodaj rozliczenie";
            RemoveUser.Content = "Usuń rozliczenie";

            LoadSellersList();
            ShowControlsSettelments();
            GridSettelmentIncome.ItemsSource = null;
            GridSettelmentIncome.ItemsSource = invoices;
           
            WiredUpDebtorList(invoices);
            GridSettelmentDebt.ItemsSource = null;
            GridSettelmentDebt.ItemsSource = debters;
            GridSettelmentDebt.Columns[1].Visibility = Visibility.Collapsed;

        }


        private void Contractors_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            ContractorsOpen = true;
            AddUser.Content = "Dodaj Kontrahenta";
            RemoveUser.Content = "Usuń Kontrahenta";
            LoadSellersList();
            ShowControls();
        }

        private void Statments_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            StatmentsOpen = true;      
            HideControls();
            StorageRB.IsChecked = true;
            LoadStorageList();
            StorageDG.Columns[5].Visibility = Visibility.Collapsed;
            StorageDG.Columns[6].Visibility = Visibility.Collapsed;
            StorageDG.Columns[7].Visibility = Visibility.Collapsed;
            StorageDG.Columns[8].Visibility = Visibility.Collapsed;
        }

        private void HideControls()
        {
            CompanyDataGrid.Visibility = Visibility.Hidden;
            AddUser.Visibility = Visibility.Hidden;
            RemoveUser.Visibility = Visibility.Hidden;
            Print.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Hidden;
            
            DateTo.Visibility = Visibility.Visible;
            DateFrom.Visibility = Visibility.Visible;
            StorageDG.Visibility = Visibility.Visible;
            StorageRB.Visibility = Visibility.Visible;
            InvoiceRB.Visibility = Visibility.Visible;
            OperationCB.Visibility = Visibility.Visible;
            GridSettelmentIncome.Visibility = Visibility.Hidden;
            GridSettelmentDebt.Visibility = Visibility.Hidden;
            DateSettelmentFrom.Visibility = Visibility.Hidden;
            DateSettelmentTo.Visibility = Visibility.Hidden;
        }

        private void ShowControls()
        {
            CompanyDataGrid.Visibility = Visibility.Visible;
            AddUser.Visibility = Visibility.Visible;
            RemoveUser.Visibility = Visibility.Visible;
            Print.Visibility = Visibility.Visible;
            Search.Visibility = Visibility.Visible;
            DateTo.Visibility = Visibility.Hidden;
            DateFrom.Visibility = Visibility.Hidden;
            StorageDG.Visibility = Visibility.Hidden;
            StorageRB.Visibility = Visibility.Hidden;
            InvoiceRB.Visibility = Visibility.Hidden;
            OperationCB.Visibility = Visibility.Hidden;
            Filter.Visibility = Visibility.Hidden;
            InvoiceDG.Visibility = Visibility.Hidden;
            GridSettelmentIncome.Visibility = Visibility.Hidden;
            GridSettelmentDebt.Visibility = Visibility.Hidden;
            DateSettelmentFrom.Visibility = Visibility.Hidden;
            DateSettelmentTo.Visibility = Visibility.Hidden;
        }

        private void ShowControlsSettelments() 
        {
            CompanyDataGrid.Visibility = Visibility.Hidden;
            AddUser.Visibility = Visibility.Hidden;
            RemoveUser.Visibility = Visibility.Hidden;
            Print.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Hidden;
            DateTo.Visibility = Visibility.Hidden;
            DateFrom.Visibility = Visibility.Hidden;
            StorageDG.Visibility = Visibility.Hidden;
            StorageRB.Visibility = Visibility.Hidden;
            InvoiceRB.Visibility = Visibility.Hidden;
            OperationCB.Visibility = Visibility.Hidden;
            Filter.Visibility = Visibility.Hidden;
            InvoiceDG.Visibility = Visibility.Hidden;
            GridSettelmentIncome.Visibility = Visibility.Visible;
            GridSettelmentDebt.Visibility = Visibility.Visible;
            DateSettelmentFrom.Visibility = Visibility.Visible;
            DateSettelmentTo.Visibility = Visibility.Visible;


        }

        private void DoubleClickDebt_Open(object sender,RoutedEventArgs e) 
        {
            if (GridSettelmentDebt.SelectedItem != null) 
            {
                DebtorWindow debtorWindow = new DebtorWindow((Debter)GridSettelmentDebt.SelectedItem);
                debtorWindow.Show();
            }
            

        }
        private void VATRegister_Open(object sender, RoutedEventArgs e)
        {
            SettingToFalse();
            VATRegisterOpen = true;
            AddUser.Content = "Dodaj rejestr vat";
            RemoveUser.Content = "Usuń rejestr vat";
            CompanyDataGrid.ItemsSource = null;
            ShowControls();
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

        private void OperationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilter();
        }

        private void InvoiceRB_Checked(object sender, RoutedEventArgs e)
        {
            if((bool)InvoiceRB.IsChecked)
            {
                Filter.Visibility = Visibility.Visible;
                OperationCB.Visibility = Visibility.Hidden;
                InvoiceDG.Visibility = Visibility.Visible;
            }
            InvoiceDG.ItemsSource = invoiceStatements;
        }

        private void StorageRB_Checked(object sender, RoutedEventArgs e)
        {
            if(Filter != null)
            {
                if ((bool)StorageRB.IsChecked)
                {
                    Filter.Visibility = Visibility.Hidden;
                    OperationCB.Visibility = Visibility.Visible;
                    InvoiceDG.Visibility = Visibility.Hidden;
                }
                StorageDG.ItemsSource = storage;
                StorageDG.Columns[5].Visibility = Visibility.Collapsed;
                StorageDG.Columns[6].Visibility = Visibility.Collapsed;
                StorageDG.Columns[7].Visibility = Visibility.Collapsed;
                StorageDG.Columns[8].Visibility = Visibility.Collapsed;
            }           
        }

        private void DateFilter()
        {
            List<StorageOperations> storageOperations = storage;
            if (DateFrom != null || DateTo != null)
            {
                if (DateFrom.SelectedDate != null || DateTo.SelectedDate != null)
                {
                    if (DateFrom.SelectedDate != null)
                    {
                        storageOperations = storageOperations.FindAll(delegate (StorageOperations x) { return DateFrom.SelectedDate <= Convert.ToDateTime(x.Date); });
                    }

                    if (DateTo.SelectedDate != null)
                    {
                        storageOperations = storageOperations.FindAll(delegate (StorageOperations x) { return DateTo.SelectedDate >= Convert.ToDateTime(x.Date); });
                    }
                }
                if (!OperationCB.SelectedItem.ToString().Equals("Wybór wszystkich"))
                {
                    storageOperations = storageOperations.FindAll(delegate (StorageOperations x) { return x.OperationName.ToLower().Equals(OperationCB.SelectedItem.ToString().ToLower()); });
                }
                StorageDG.ItemsSource = storageOperations;
                if (StorageDG.Columns.Count > 0)
                {
                    StorageDG.Columns[5].Visibility = Visibility.Collapsed;
                    StorageDG.Columns[6].Visibility = Visibility.Collapsed;
                    StorageDG.Columns[7].Visibility = Visibility.Collapsed;
                    StorageDG.Columns[8].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void DateFilterDebtor()
        {
            List<Invoice> debtorInvoice = invoices;
            if (DateFrom != null || DateTo != null)
            {
                if (DateSettelmentFrom.SelectedDate != null || DateSettelmentTo.SelectedDate != null)
                {
                    if (DateSettelmentFrom.SelectedDate != null)
                    {
                        debtorInvoice = debtorInvoice.FindAll(delegate (Invoice x) { return DateSettelmentFrom.SelectedDate <= Convert.ToDateTime(x.CreationDate); });
                    }

                    if (DateSettelmentTo.SelectedDate != null)
                    {
                        debtorInvoice = debtorInvoice.FindAll(delegate (Invoice x) { return DateSettelmentTo.SelectedDate >= Convert.ToDateTime(x.CreationDate); });
                    }
                }
            }
            GridSettelmentIncome.ItemsSource = null;
            GridSettelmentIncome.ItemsSource = debtorInvoice;
            WiredUpDebtorList(debtorInvoice);
            GridSettelmentDebt.ItemsSource = null;
            GridSettelmentDebt.ItemsSource = debters;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            List<BaseInvoice> baseInvoice = baseInvoices;
            if (DateFrom != null || DateTo != null)
            {
                if (DateFrom.SelectedDate != null || DateTo.SelectedDate != null)
                {
                    if (DateFrom.SelectedDate != null)
                    {
                        baseInvoice = baseInvoice.FindAll(delegate (BaseInvoice x) { return DateFrom.SelectedDate <= Convert.ToDateTime(x.CreationDate); });
                    }

                    if (DateTo.SelectedDate != null)
                    {
                        baseInvoice = baseInvoice.FindAll(delegate (BaseInvoice x) { return DateTo.SelectedDate >= Convert.ToDateTime(x.CreationDate); });
                    }
                }
                InvoiceDG.ItemsSource = baseInvoice;
            }
        }

        private void WiredUpDebtorList(List<Invoice> debtorInvoice)
        {
            debters.Clear();
            string fullName = "";
            int iD;
            int debt;
            int paid;
            int toPay;
            string invoiceNumber;
            foreach (Invoice invoice in debtorInvoice)
            {
                paid = Int32.Parse(invoice.Paid);
                toPay = Int32.Parse(invoice.ToPay);
                if(paid < toPay)
                {
                    debt = toPay - paid;
                    iD = invoice.IdSeller;
                    invoiceNumber = invoice.Number;
                    foreach(Seller seller in sellers)
                    {
                        if(invoice.IdSeller == seller.IdSeller)
                        {
                            fullName = seller.Name + " " + seller.Surname;
                        }
                    }
                     Debter debter = new Debter(fullName, iD, debt, invoiceNumber);
                    bool isInList = false;
                    foreach (Debter debter1 in debters)
                    {
                        if (debter1.FullName.Equals(debter.FullName))
                        {
                            isInList = true;
                            debter = debter1;
                        }

                    }
                    if (isInList)
                    {
                        debter.AddToInvoiceDictionary(invoiceNumber, debt);
                        debter.AddDebts(debt);
                        debters.Remove(debter);
                    }
                    debters.Add(debter);

                }
            }
        }


        private void DateTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilter();
        }

        private void DateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilter();
        }

        private void DateSettelmentFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilterDebtor();
        }

        private void DateSettelmentTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilterDebtor();
        }
    }
}