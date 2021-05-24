using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System_do_zarzadzania_obslugi_sprzedazy.Classes;


namespace System_do_zarzadzania_obslugi_sprzedazy.Winows
{

    public partial class EditedInvoiceDetails : Window
    {

        private Invoice showInvoice;
        private Company showCompanyName;
        private Seller showCompanySeller;
        private StorageOperations storageOperation;

        private int invoiceID;
        private int companyID;
        List<InvoiceProduct> invoiceProducts;
        List<Company> companyName;
        List<Seller> companySellerName;
        List<EditedInvoiceProduct> editedInvoiceProducts = new List<EditedInvoiceProduct>();
        public EditedInvoiceDetails(InvoiceCorrection invoiceCorrection)
        {

            InitializeComponent();
            showInvoice = SQLiteDataAccess.LoadInvoice(invoiceCorrection.InvoiceConnection)[0];
            this.companyID = 1;
            invoiceID = invoiceCorrection.CorrectionID;
            LoadInvoiceList();
            ID.Text = invoiceCorrection.CorrectionID.ToString();
            LoadCompanyList();
            IdCompany.Text = showCompanyName.CompanyName;

            LoadSellerList();
            IdSeller.Text = showCompanySeller.Name + " " + showCompanySeller.Surname;
            Number.Text = invoiceCorrection.CorrectionNumber.ToString();
            CreationDate.Text = invoiceCorrection.CorrectionDate;
            SaleDate.Text = invoiceCorrection.CorrectionDate;
            PaymentType.Text = showInvoice.PaymentType;
            PaymentDeadline.Text = showInvoice.PaymentDeadline;
            ToPay.Text = showInvoice.ToPay;
            ToPayInWords.Text = showInvoice.ToPayInWords;
            Paid.Text = showInvoice.Paid;
            DateOfIssue.Text = showInvoice.DateOfIssue;
            NameOfService.Text = showInvoice.NameOfService;
            if (String.IsNullOrEmpty(showInvoice.AccountNumber))
            {
                AccountNumber.Visibility = Visibility.Hidden;
                AccountNumberLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                AccountNumber.Text = showInvoice.AccountNumber;
            }
            EditPdfCB.IsChecked = false;
        }
        private void LoadInvoiceList()
        {
            editedInvoiceProducts = SQLiteDataAccess.LoadEditedInvoicesProduct(invoiceID);
            InvoiceProductListDataGrid.ItemsSource = editedInvoiceProducts;
        }

        private void LoadCompanyList()
        {
            companyName = SQLiteDataAccess.LoadNameCompany(companyID);
            showCompanyName = companyName[0];

        }

        private void LoadSellerList()
        {
            companySellerName = SQLiteDataAccess.LoadNameSeller(companyID);
            showCompanySeller = companySellerName[0];
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
        }

        private void PaymentTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PaymentTypeCB.Text.Equals("Przelew"))
            {
                AccountNumber.Visibility = Visibility.Hidden;
                AccountNumberLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                AccountNumber.Visibility = Visibility.Visible;
                AccountNumberLabel.Visibility = Visibility.Visible;
                //Dlaczego to tak działa, Jakby to powiedział święty jp2 NIE WIEM
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PaymentType.IsEnabled = true;
            PaymentDeadline.IsEnabled = true;
            ToPay.IsEnabled = true;
            ToPayInWords.IsEnabled = true;
            Paid.IsEnabled = true;
            DateOfIssue.IsEnabled = true;
            NameOfService.IsEnabled = true;
            AccountNumber.IsEnabled = true;
            CreateEditedPdf.Visibility = Visibility.Visible;
            PaymentTypeCB.Visibility = Visibility.Visible;
            PaymentTypeCB.SelectedIndex = 1;
            Corection.Visibility = Visibility.Visible;
            CorectionLabel.Visibility = Visibility.Visible;
            DateOfCoretion.Visibility = Visibility.Visible;
            DateOfCorectionLabel.Visibility = Visibility.Visible;
            AddProduct.IsEnabled = false;
            DelProduct.IsEnabled = false;
            InvoiceProductListDataGrid.CanUserAddRows = true;
            InvoiceProductListDataGrid.IsReadOnly = false;
            InvoiceProductListDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            InvoiceProductListDataGrid.Columns[1].Visibility = Visibility.Collapsed;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IdSeller.IsEnabled = false;
            CreationDate.IsEnabled = false;
            SaleDate.IsEnabled = false;
            PaymentType.IsEnabled = false;
            PaymentDeadline.IsEnabled = false;
            ToPay.IsEnabled = false;
            ToPayInWords.IsEnabled = false;
            Paid.IsEnabled = false;
            DateOfIssue.IsEnabled = false;
            NameOfService.IsEnabled = false;
            AccountNumber.IsEnabled = false;
            CreateEditedPdf.Visibility = Visibility.Hidden;
            PaymentTypeCB.Visibility = Visibility.Hidden;
            Corection.Visibility = Visibility.Hidden;
            CorectionLabel.Visibility = Visibility.Hidden;
            DateOfCoretion.Visibility = Visibility.Hidden;
            DateOfCorectionLabel.Visibility = Visibility.Hidden;
            AddProduct.IsEnabled = true;
            DelProduct.IsEnabled = true;
            InvoiceProductListDataGrid.CanUserAddRows = false;
            LoadInvoiceList();
            InvoiceProductListDataGrid.Columns[0].Visibility = Visibility.Visible;
            InvoiceProductListDataGrid.Columns[1].Visibility = Visibility.Visible;
        }

    }
}
