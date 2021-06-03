using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System_do_zarzadzania_obslugi_sprzedazy.Winows;
using System_do_zarzadzania_obslugi_sprzedazy.Classes;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Microsoft.Win32;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Startowe okienko aplikacji Systemu fakturowania. Z tego okienka można się przedostać
    /// do faktur, magazynu, rozliczeń, kontrahentów, zestawień. Można dodawać, zmieniać 
    /// i drukować do pdf.
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Seller> sellers = new List<Seller>();
        List<BaseInvoice> baseInvoices = new List<BaseInvoice>();
        List<BaseInvoice> invoiceStatements = new List<BaseInvoice>();
        List<Invoice> invoices = new List<Invoice>();
        List<Product> products = new List<Product>();
        List<StorageOperations> storage = new List<StorageOperations>();
        List<string> operations = new List<string>();
        List<Debter> debters = new List<Debter>();
        List<InvoiceCorrection> invoiceCorrections = new List<InvoiceCorrection>();

        private bool invoiceOpen = true;
        private bool StorageOpen = false;
        private bool SettlementsOpen = false;
        private bool ContractorsOpen = false;
        private bool StatmentsOpen = false;

        private int companyID = 1;

        /// <summary>
        /// Konstruktor, który inicjalizuje komponenty okienka oraz odsłania domyślne kontrolki
        /// i przeładowywuje pola odpowiednymi danymi
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ShowControls();
            FillOperations();
            LoadInvoicesList();
            OperationCB.ItemsSource = operations;
            InvoiceCorrections.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Metoda co dodaje na listę nazwy operacji przyjęcia i wydań
        /// </summary>
        private void FillOperations()
        {
            operations.Add("Przyjęcie wewnętrzne");
            operations.Add("Przyjęcie zewnętrzne");
            operations.Add("Wydanie wewnętrzne");
            operations.Add("Wydanie zewnętrzne");
            operations.Add("Wybór wszystkich");
        }

        /// <summary>
        /// Metoda, która aktualizuje datagrida kontrahentami 
        /// </summary>
        private void WiredUpSellersList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = sellers;
        }

        /// <summary>
        /// Metoda, która aktualizuje listę faktur
        /// </summary>
        private void WiredUpInvoicesList()
        {
            CompanyDataGrid.ItemsSource = null;
            StorageDG.ItemsSource = null;
            CompanyDataGrid.ItemsSource = baseInvoices;
            InvoiceDG.ItemsSource = invoiceStatements;
        }

        /// <summary>
        /// Metoda, która aktualizuje listę produktów
        /// </summary>
        private void WiredUpProductList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = products;
        }

        /// <summary>
        /// Metoda, która aktualizuje listę magazynu
        /// </summary>
        private void WiredUpStorageList()
        {
            StorageDG.ItemsSource = null;
            StorageDG.ItemsSource = storage;
        }

        /// <summary>
        /// Metoda, która aktualizuje listę korekt faktur
        /// </summary>
        private void WiredUpCorrectionList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = invoiceCorrections;
        }

        /// <summary>
        /// Metoda, która wczytuje z bazy danych korekty faktur
        /// </summary>
        private void LoadCorrectionList()
        {
            invoiceCorrections = SQLiteDataAccess.LoadCorrection();
            WiredUpCorrectionList();
        }

        /// <summary>
        /// Metoda, która wczytuje z bazy danych pozycje na magazynie
        /// </summary>
        private void LoadStorageList()
        {
            storage = SQLiteDataAccess.LoadOperations();
            WiredUpStorageList();
        }

        /// <summary>
        /// Metoda, która wczytuje z bazy danych fakturę, i konwertujemy na faktury z 
        /// podstawowymi informacjami
        /// </summary>
        private void LoadInvoicesList()
        {
            invoices = SQLiteDataAccess.LoadInvoices();
            baseInvoices = invoices.ConvertAll(x => (BaseInvoice)x);
            invoiceStatements = invoices.ConvertAll(x => (BaseInvoice)x);
            WiredUpInvoicesList();
        }

        /// <summary>
        /// Metoda, która wczytuje z bazy danych listę kontrahentów
        /// </summary>
        private void LoadSellersList()
        {
            sellers = SQLiteDataAccess.LoadSellers();
            WiredUpSellersList();

        }
        /// <summary>
        /// Metoda, która wczytuje z bazy danych listę produktów
        /// </summary>
        private void LoadProductsList()
        {
            products = SQLiteDataAccess.LoadProducts(companyID);
            WiredUpProductList();
        }

        /// <summary>
        /// Metoda, która obsługuje guzik dodawanie operacji w zależności 
        /// od wczytanego datagrida(dodawanie faktur,kontrahentów, produktów,)
        /// </summary>
        private void Add_User(object sender, RoutedEventArgs e)
        {
            if (invoiceOpen)
            {
                NewInvoice newInvoice = new NewInvoice();
                newInvoice.Show();
                newInvoice.Closed += (s, eventarg) =>
                {
                    LoadInvoicesList();
                };
            }
            if (StorageOpen)
            {
                AddNewProductMagazine addNewProductMagazine = new AddNewProductMagazine();
                addNewProductMagazine.Show();
                addNewProductMagazine.Closed += (s, eventarg) =>
                {
                    LoadProductsList();
                };
            }
            if (ContractorsOpen)
            {
                AddNewSeller addNewSeller = new AddNewSeller();
                addNewSeller.Show();
                addNewSeller.Closed += (s, eventarg) =>
                {
                    LoadSellersList();
                };
            }
        }

        /// <summary>
        /// Metoda, która obsługuje guzik usuwanie operacji w zależności 
        /// od wczytanego datagrida(usuwanie faktur,kontrahentów, produktów,)
        /// </summary>
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
            if (ContractorsOpen)
            {
                if (CompanyDataGrid.SelectedItem != null)
                {
                    SQLiteDataAccess.DeleteContractors((Seller)CompanyDataGrid.SelectedItem);
                    sellers.Remove((Seller)CompanyDataGrid.SelectedItem);
                    LoadSellersList();
                }
            }
        }

        /// <summary>
        /// Metoda, która obsługuje zdarzenie wcisnięcia przycisku faktury, który 
        /// powoduje ukrywanie wszystkich kontrolek poza fakturami(dodaj fakture,
        /// usuń fakturę,checkbox korekt, datagrid faktur oraz panelu głównego 
        /// przycisków ).Wywołuje metodę wczytywania faktur.
        /// </summary>
        private void Invoice_Open(object sender, RoutedEventArgs e)
        {
            InvoiceCorrections.IsChecked = false;
            SettingToFalse();
            invoiceOpen = true;
            AddUser.Content = "Dodaj fakture";
            RemoveUser.Content = "Usuń fakture";
            LoadInvoicesList();
            ShowControls();
            InvoiceCorrections.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Metoda, która obsługuje zdarzenie wcisnięcia przycisku Magazyn, który 
        /// powoduje ukrywanie wszystkich kontrolek poza Magazynem(dodaj produkt,
        /// usuń produkt, datagrid magazynu oraz panelu głównego przycisków ).
        /// Wywołuje metodę wczytywania listy na magazynie.
        /// </summary>
        private void Storage_Open(object sender, RoutedEventArgs e)
        {
            InvoiceCorrections.IsChecked = false;
            SettingToFalse();
            StorageOpen = true;
            AddUser.Content = "Dodaj przedmiot";
            RemoveUser.Content = "Usuń przedmiot";
            CompanyDataGrid.ItemsSource = null;
            LoadProductsList();
            ShowControls();
            InvoiceCorrections.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Metoda, która obsługuje zdarzenie wcisnięcia przycisku Rozliczenia, który 
        /// powoduje ukrywanie wszystkich kontrolek poza Rozliczeniami(datepickery wyboru dnia,
        /// przycisk generuj zestawienie, datagrid rozliczeń,dłużnków oraz panelu głównego 
        /// przycisków).Wywołuje metodę wczytywania listy rozliczeń, oraz dłużników.
        /// </summary>
        private void Settlements_Open(object sender, RoutedEventArgs e)
        {
            InvoiceCorrections.IsChecked = false;
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
            GridSettelmentDebt.Columns[3].Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Metoda, która obsługuje zdarzenie wcisnięcia przycisku Kontrahenta, który 
        /// powoduje ukrywanie wszystkich kontrolek poza Kontrahentami(dodaj kontrahenta,
        /// usuń kontrahenta, datagrid kontrahenta oraz panelu głównego przycisków).
        /// Wywołuje metodę wczytywania listy kontrahentów.
        /// </summary>
        private void Contractors_Open(object sender, RoutedEventArgs e)
        {
            InvoiceCorrections.IsChecked = false;
            SettingToFalse();
            ContractorsOpen = true;
            AddUser.Content = "Dodaj Kontrahenta";
            RemoveUser.Content = "Usuń Kontrahenta";
            LoadSellersList();
            ShowControls();
        }

        /// <summary>
        /// Metoda, która obsługuje zdarzenie wcisnięcia przycisku Zestawienia, który 
        /// powoduje ukrywanie wszystkich kontrolek poza Zestawieniami(dwa datapickery,
        /// dwa radiobuttony,dwa datagridy faktur oraz zestawień i panelu głównego 
        /// przycisków). Wywołuje metodę wczytywania listy zestaweń i faktur w 
        /// zależności od wybranego radiobuttona.
        /// </summary>
        private void Statments_Open(object sender, RoutedEventArgs e)
        {
            InvoiceCorrections.IsChecked = false;
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

        /// <summary>
        /// Metoda, która ukrywa/odsłania odpowiednie kontrolki
        /// </summary>
        private void HideControls()
        {
            CompanyDataGrid.Visibility = Visibility.Hidden;
            AddUser.Visibility = Visibility.Hidden;
            RemoveUser.Visibility = Visibility.Hidden;
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
            CreateStatement.Visibility = Visibility.Hidden;
            InvoiceCorrections.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Metoda, która ukrywa/odsłania odpowiednie kontrolki
        /// </summary>
        private void ShowControls()
        {
            CompanyDataGrid.Visibility = Visibility.Visible;
            AddUser.Visibility = Visibility.Visible;
            RemoveUser.Visibility = Visibility.Visible;
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
            CreateStatement.Visibility = Visibility.Hidden;
            InvoiceCorrections.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Metoda, która ukrywa/odsłania odpowiednie kontrolki dla Rozliczeń
        /// </summary>
        private void ShowControlsSettelments()
        {
            CompanyDataGrid.Visibility = Visibility.Hidden;
            AddUser.Visibility = Visibility.Hidden;
            RemoveUser.Visibility = Visibility.Hidden;
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
            CreateStatement.Visibility = Visibility.Visible;
            InvoiceCorrections.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Metoda, która otwiera okienko w przypadku podwójnego kliknięcia
        /// na dłużnika (w rozliczeniach) i pokazuje dla dłużnika jego długi
        /// </summary>
        private void DoubleClickDebt_Open(object sender, RoutedEventArgs e)
        {
            if (GridSettelmentDebt.SelectedItem != null)
            {
                DebtorWindow debtorWindow = new DebtorWindow((Debter)GridSettelmentDebt.SelectedItem);
                debtorWindow.Show();
            }
        }

        /// <summary>
        /// Metoda, która generuje nazwy kolumn dla Datagridów
        /// </summary>
        private void CompanyDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
        }

        /// <summary>
        /// Metoda, która po podwójnego kliknięcia na datagrida w zakładce faktury 
        /// otwiera szczegóły danej faktury(normalna faktura, korekta faktur oraz 
        /// listę produktów)W przypadku datagrida w zakładce Magazyn otwiera okno 
        /// z dodatkowymi operacjami. W ostatnim przypadku(nie wybrania pozycji z listy)
        /// użytkownik dostanie powiadomienie "Wybierz pozycję z listy!".
        /// </summary>
        private void DoubleClick_Open(object sender, MouseButtonEventArgs e)
        {
            if (CompanyDataGrid.SelectedItem != null && invoiceOpen)
            {
                if (InvoiceCorrections.IsChecked != true)
                {
                    Invoice inv = CompanyDataGrid.SelectedItem as Invoice;
                    InvoiceDetails invoiceDetails = new InvoiceDetails(inv, inv.Id, inv.IdCompany);
                    invoiceDetails.Show();
                }
                else
                {
                    InvoiceCorrection invoiceCorrection = CompanyDataGrid.SelectedItem as InvoiceCorrection;
                    EditedInvoiceDetails edited = new EditedInvoiceDetails(invoiceCorrection);
                    edited.Show();
                }
            }
            else if (CompanyDataGrid.SelectedItem != null && StorageOpen)
            {
                Product prd = CompanyDataGrid.SelectedItem as Product;
                StorageAdditionalOperations storageAdditionalOperations = new StorageAdditionalOperations(prd);
                storageAdditionalOperations.Show();
                storageAdditionalOperations.Closed += (s, eventarg) =>
                {
                    LoadProductsList();
                };
            }
            else
            {
                MessageBox.Show("Wybierz pozycję z listy!");
            }
        }

        /// <summary>
        /// Metoda, ustawia flagi na false
        /// </summary>
        private void SettingToFalse()
        {
            invoiceOpen = false;
            StorageOpen = false;
            SettlementsOpen = false;
            ContractorsOpen = false;
            StatmentsOpen = false;
        }

        /// <summary>
        /// Metoda, która wywołuje metodę do filtrowania datagridów po datach
        /// </summary>
        private void OperationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilter();
        }

        /// <summary>
        /// Metoda wywołuje się jeśli będzie wcisnięty radiobutton faktur w zakładce
        /// rozliczenia to pokazuje odpowiednie kontrolki dla faktur
        /// </summary>
        private void InvoiceRB_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)InvoiceRB.IsChecked)
            {
                Filter.Visibility = Visibility.Visible;
                OperationCB.Visibility = Visibility.Hidden;
                InvoiceDG.Visibility = Visibility.Visible;
            }
            InvoiceDG.ItemsSource = invoiceStatements;
        }

        /// <summary>
        /// Metoda wywołuje się jeśli będzie wcisnięty radiobuton magazyn w zakładce
        /// rozliczenia i odkrywa odpowiednie kontrolki i chowa niektóre kolumny w datagridzie
        /// </summary>
        private void StorageRB_Checked(object sender, RoutedEventArgs e)
        {
            if (Filter != null)
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

        /// <summary>
        /// Metoda filtrowania datagrida po wybraniu dat
        /// </summary>
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
        /// <summary>
        /// Metoda która filtruje daty po datagridzie po wybraniu dat w zakładce Dłużnicy
        /// </summary>
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

        /// <summary>
        /// Metoda, która się wywołuje po wcisnięciu się guzika filtruj w zakładce zestawienia
        /// podczas wczytanego datagrida z fakturami
        /// </summary>
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

        /// <summary>
        /// Metoda, która wczytuje listę dłużników
        /// </summary>
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
                if (paid < toPay)
                {
                    debt = toPay - paid;
                    iD = invoice.IdSeller;
                    invoiceNumber = invoice.Number;
                    foreach (Seller seller in sellers)
                    {
                        if (invoice.IdSeller == seller.IdSeller)
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
                        debter.AddDebts(debt);
                        debters.Remove(debter);
                    }
                    debter.AddToInvoiceDictionary(invoiceNumber, debt);
                    debters.Add(debter);

                }
            }
        }

        /// <summary>
        /// Metoda, która tworzy pdf do zestawień i zapisuje je do pliku w wybranym folderze
        /// </summary>
        private void CreatestatementsPDF()
        {
            try
            {

                StringBuilder date = new StringBuilder("");

                if (!String.IsNullOrEmpty(DateSettelmentFrom.Text))
                {
                    date.Append(" od " + DateSettelmentFrom.Text);
                }
                if (!String.IsNullOrEmpty(DateSettelmentTo.Text))
                {
                    date.Append(" do " + DateSettelmentTo.Text);
                }

                date = date.Replace("/", ".");

                DateTime date2 = DateTime.Now;
                String save = date2.ToString("G");
                save = save.Replace("/", ".");

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
                saveFileDialog1.FileName = "Zestawienie okresowe" + " " + save;
                saveFileDialog1.InitialDirectory = @"c:\";
                if (saveFileDialog1.ShowDialog() == true)
                {

                    System.IO.FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                    var pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);
                    pdfDoc.Open();

                    var spacer = new iTextSharp.text.Paragraph("")
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f,
                    };

                    var spacer2 = new iTextSharp.text.Paragraph("")
                    {
                        SpacingBefore = 100f,
                        SpacingAfter = 70f,
                    };

                    var spacer3 = new iTextSharp.text.Paragraph("")
                    {
                        SpacingBefore = 50f,
                        SpacingAfter = 30f,
                    };

                    List<Debter> debters = GridSettelmentDebt.ItemsSource as List<Debter>;
                    List<Invoice> invoiceList = GridSettelmentIncome.ItemsSource as List<Invoice>;

                    var titleFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 28);
                    var numberFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 22);
                    var serviceFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 16);
                    var dateFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 14);
                    var smallFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 11);
                    var tableFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 12);
                    DateTime date1 = DateTime.Today;

                    var docDate = new iTextSharp.text.Paragraph("Data wygenerowania: " + date1.ToString("d"), dateFont);
                    docDate.Alignment = Element.ALIGN_RIGHT;
                    var docTitle = new iTextSharp.text.Paragraph("Zestawienie okresowe" + date.ToString(), numberFont);
                    docTitle.Alignment = Element.ALIGN_CENTER;

                    pdfDoc.Add(docDate);
                    pdfDoc.Add(spacer3);
                    pdfDoc.Add(docTitle);

                    var invoiceTable = new PdfPTable(new[] { 2f, 2f, 2f, 2f })
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 100,
                        DefaultCell = { MinimumHeight = 22f }
                    };

                    var debtTable = new PdfPTable(new[] { 2f, 2f })
                    {
                        HorizontalAlignment = 1,
                        WidthPercentage = 100,
                        DefaultCell = { MinimumHeight = 22f }
                    };

                    PdfPCell cell1 = new PdfPCell(new iTextSharp.text.Paragraph("Numer Faktury", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    PdfPCell cell2 = new PdfPCell(new iTextSharp.text.Paragraph("Data wystawienia", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    PdfPCell cell3 = new PdfPCell(new iTextSharp.text.Paragraph("Do zapłaty", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    PdfPCell cell4 = new PdfPCell(new iTextSharp.text.Paragraph("Zapłacono", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell2.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell3.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell4.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);

                    invoiceTable.AddCell(cell1);
                    invoiceTable.AddCell(cell2);
                    invoiceTable.AddCell(cell3);
                    invoiceTable.AddCell(cell4);

                    PdfPCell cell5 = new PdfPCell(new iTextSharp.text.Paragraph("Imie i nazwisko", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    PdfPCell cell6 = new PdfPCell(new iTextSharp.text.Paragraph("Dług", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };

                    cell5.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell6.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);

                    debtTable.AddCell(cell5);
                    debtTable.AddCell(cell6);


                    invoiceList.ForEach(a =>
                    {
                        invoiceTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.Number, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        invoiceTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.CreationDate, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        invoiceTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.ToPay.ToString(), tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        invoiceTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.Paid.ToString(), tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    });

                    debters.ForEach(a =>
                    {
                        debtTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.FullName, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        debtTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.Debt.ToString(), tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    });

                    pdfDoc.Add(spacer);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(invoiceTable);
                    pdfDoc.Add(spacer3);
                    pdfDoc.Add(debtTable);
                    pdfDoc.Close();
                    writer.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało utworzyć się pliku pdf", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Metoda, która wywołuje metodę do filtrowania po datach od w zestawieniach
        /// </summary>
        private void DateTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilter();
        }

        /// <summary>
        /// Metoda, która wywołuje metodę do filtrowania po datach do w zestawieniach
        /// </summary>
        private void DateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilter();
        }

        /// <summary>
        /// Metoda, która wywołuje metodę do filtrowania po datach od w rozliczeniach
        /// </summary>
        private void DateSettelmentFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilterDebtor();
        }

        /// <summary>
        /// Metoda, która wywołuje metodę do filtrowania po datach do w rozliczeniach
        /// </summary>
        private void DateSettelmentTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFilterDebtor();
        }

        /// <summary>
        /// Metoda, która po wcisnięciu przycisku zapisz do pdf wywołuje metodę tworzenia pdf
        /// </summary>
        private void CreateStatement_Click(object sender, RoutedEventArgs e)
        {
            CreatestatementsPDF();
            MessageBox.Show("Utworzono plik pdf");
        }

        /// <summary>
        /// Metoda, która wywołuje się po wcisnięciu się checkboxu korekta faktur w zakładce 
        /// faktury. Ukrywa odpowiedne kolumny w datagridzie 
        /// </summary>
        private void InvoiceCorrections_Checked(object sender, RoutedEventArgs e)
        {
            LoadCorrectionList();
            CompanyDataGrid.Columns[4].Visibility = Visibility.Collapsed;
            CompanyDataGrid.Columns[5].Visibility = Visibility.Collapsed;

        }

        /// <summary>
        /// Metoda, która wywołuje się po odznaczeniu się checkboxu korekta faktur w zakładce 
        /// faktury. Odsłania odpowiedne kolumny w datagridzie 
        /// </summary>
        private void InvoiceCorrections_Unchecked(object sender, RoutedEventArgs e)
        {
            CompanyDataGrid.Columns[4].Visibility = Visibility.Visible;
            CompanyDataGrid.Columns[5].Visibility = Visibility.Visible;
            LoadInvoicesList();
        }
    }
}