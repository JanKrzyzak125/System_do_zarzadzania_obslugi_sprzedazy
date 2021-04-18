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
using System_do_zarzadzania_obslugi_sprzedazy.Winows;
using System_do_zarzadzania_obslugi_sprzedazy.Classes;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Interaction logic for InvoiceDetails.xaml
    /// </summary>
    public partial class InvoiceDetails : Window
    {

        private Invoice showInvoice;
        private Company showCompanyName;
        private Seller showCompanySeller;

        private int invoiceID;
        private int companyID;
        List<InvoiceProduct> invoiceProducts;
        List<Company> companyName;
        List<Seller> companySellerName;

        public InvoiceDetails(Invoice invoice, int iID, int companyID)
        {

            showInvoice = invoice;
            InitializeComponent();
            invoiceID = iID;
            //this.companyID = companyID;
            this.companyID = 1;
            LoadInvoiceList();
            ID.Text = showInvoice.Id.ToString();
            

            LoadCompanyList();
            IdCompany.Text = showCompanyName.CompanyName;

            LoadSellerList();
            IdSeller.Text = showCompanySeller.Name + " " + showCompanySeller.Surname;// potrzeba danych w tabelce 

            
            //IdSeller.Text = showInvoice.IdSeller.ToString();
            //IdCompany.Text = showInvoice.IdCompany.ToString();
            Number.Text = showInvoice.Number.ToString();
            CreationDate.Text = showInvoice.CreationDate;
            SaleDate.Text = showInvoice.SaleDate;
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
        }

        private void LoadInvoiceList()
        {
            invoiceProducts = SQLiteDataAccess.LoadInvoicesProduct(invoiceID);
            InvoiceProductListDataGrid.ItemsSource = invoiceProducts;
        }

        private void LoadCompanyList() 
        {
            companyName = SQLiteDataAccess.LoadNameCompany(companyID);
            showCompanyName = companyName[0];

        }

        private void LoadSellerList()
        {
            companySellerName = SQLiteDataAccess.LoadNameSeller(companyID);
            showCompanySeller = companySellerName[0]; //potrzeba danych w tabelce 
        }


        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {

            NewProduct newProduct = new NewProduct(invoiceID, companyID);
            newProduct.Show();
            newProduct.Closed += (s, eventarg) =>
            {
                LoadInvoiceList();
            };
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceProductListDataGrid.SelectedItem != null)
            {
                SQLiteDataAccess.DeleteProductFromInvoice((InvoiceProduct)InvoiceProductListDataGrid.SelectedItem);
                invoiceProducts.Remove((InvoiceProduct)InvoiceProductListDataGrid.SelectedItem);
                LoadInvoiceList();
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
        }

        private void ExportToPDF()
        {
            try
            {
                System.IO.FileStream fs = new FileStream("D:/projekcik" + "\\" + showInvoice.Number + ".pdf", FileMode.Create);
                var pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);
                pdfDoc.Open();
                var spacer = new iTextSharp.text.Paragraph("")
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                };

                var titleFont = FontFactory.GetFont("Arial", 32, BaseColor.BLACK);
                var numberFont = FontFactory.GetFont("Arial", 26, BaseColor.BLACK);
                var docTitle = new iTextSharp.text.Paragraph("FAKTURA VAT", titleFont);
                var docNumber = new iTextSharp.text.Paragraph("NR " + showInvoice.Number, numberFont);
                docTitle.Alignment = Element.ALIGN_CENTER;
                docNumber.Alignment = Element.ALIGN_CENTER;

                pdfDoc.Add(docTitle);
                pdfDoc.Add(docNumber);
                pdfDoc.Add(spacer);

                var placeTable = new PdfPTable(new[] { .5f, .5f })
                {
                    HorizontalAlignment = 1,
                    WidthPercentage = 50,
                    DefaultCell = { MinimumHeight = 22f }
                };

                placeTable.AddCell("Data wystawienia: ");
                placeTable.AddCell(showInvoice.CreationDate);
                placeTable.AddCell("Data wykonania usługi: ");
                placeTable.AddCell(showInvoice.DateOfIssue);


                var headerTable = new PdfPTable(new[] { .5f, .5f })
                {
                    HorizontalAlignment = 0,
                    WidthPercentage = 40,
                    DefaultCell = { MinimumHeight = 22f }
                };
                headerTable.AddCell("Nazwa Firmy");
                headerTable.AddCell(showCompanyName.CompanyName);
                headerTable.AddCell("Nip");
                headerTable.AddCell(showCompanyName.Nip);
                headerTable.AddCell("Adres");
                headerTable.AddCell(showCompanyName.Street + "\n" + showCompanyName.City);
                headerTable.AddCell("Numer Telefonu");
                headerTable.AddCell(showCompanyName.PhoneNumber);
                headerTable.AddCell("E-mail");
                headerTable.AddCell(showCompanyName.Email);

                var headerTable2 = new PdfPTable(new[] { .5f, .5f })
                {
                    HorizontalAlignment = 2,
                    WidthPercentage = 40,
                    DefaultCell = { MinimumHeight = 22f }
                };

                if (String.IsNullOrEmpty(showCompanySeller.Surname))
                {
                    headerTable2.AddCell("Nazwa Firmy");
                    headerTable2.AddCell(showCompanySeller.Name);
                    headerTable2.AddCell("NIP");
                    headerTable2.AddCell(showCompanySeller.Nip);
                    headerTable2.AddCell("REGON");
                    headerTable2.AddCell(showCompanySeller.Regon);

                }
                else
                {
                    headerTable2.AddCell("Imie");
                    headerTable2.AddCell(showCompanySeller.Name);
                    headerTable2.AddCell("Nazwisko");
                    headerTable2.AddCell(showCompanySeller.Surname);
                }
                headerTable2.AddCell("Adres");
                headerTable2.AddCell(showCompanySeller.Street + "\n" + showCompanySeller.City);
                headerTable2.AddCell("Numer Telefonu");
                headerTable2.AddCell(showCompanySeller.NumberPhone);


                pdfDoc.Add(spacer);
                pdfDoc.Add(placeTable);
                pdfDoc.Add(spacer);
                pdfDoc.Add(headerTable);
                pdfDoc.Add(headerTable2);
                pdfDoc.Add(spacer);

                var columnCount = 6;
                var columnWidths = new[] { 2.5f, 1f, 1f, 2f, 2f, 2f };

                var table = new PdfPTable(columnWidths)
                {
                    HorizontalAlignment = 0,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 22f }
                };

                var cell = new PdfPCell()
                {
                    Colspan = columnCount,
                    HorizontalAlignment = 1,  //0=Left, 1=Centre, 2=Right
                    MinimumHeight = 30f
                };

                table.AddCell("Nazwa Produktu");
                table.AddCell("JM");
                table.AddCell("Ilosc");
                table.AddCell("Cena Netto [pln]");
                table.AddCell("Cena Brutto [pln]");
                table.AddCell("Podatek VAT [%]");

                invoiceProducts.ForEach(a =>
                {
                    table.AddCell(a.ProductName);
                    table.AddCell(a.QuantityUnitName);
                    table.AddCell(a.Quantity.ToString());
                    table.AddCell(a.NettoPrice.ToString());
                    table.AddCell(a.BruttoPrice.ToString());
                    table.AddCell(a.Vat.ToString());

                });

                pdfDoc.Add(table);

                pdfDoc.Close();
                writer.Close();
                fs.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void CreatePDF_Click(object sender, RoutedEventArgs e)
        {
            ExportToPDF();
        }
    }
}
