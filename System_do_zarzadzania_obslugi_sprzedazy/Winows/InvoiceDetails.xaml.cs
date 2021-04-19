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

        private Dictionary<int, int> vatValuesMethod()
        {
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            foreach (InvoiceProduct product in invoiceProducts)
            {
                if(keyValuePairs.ContainsKey(product.Vat))
                {
                    keyValuePairs[product.Vat] += product.NettoPrice;
                }
                else
                {
                    keyValuePairs.Add(product.Vat, product.NettoPrice);
                }
            }
            return keyValuePairs;
        }

        private Dictionary<int, int> vatValuesBruttoMethod()
        {
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            foreach (InvoiceProduct product in invoiceProducts)
            {
                if (keyValuePairs.ContainsKey(product.Vat))
                {
                    keyValuePairs[product.Vat] += product.BruttoPrice;
                }
                else
                {
                    keyValuePairs.Add(product.Vat, product.BruttoPrice);
                }
            }
            return keyValuePairs;
        }


        private int wholeBruttoPrice(Dictionary<int, int> sumBrutto)
        {
            int sum = 0;
            foreach(KeyValuePair<int, int> entry in sumBrutto)
            {
                sum += entry.Value;
            }

            return sum;
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

                var spacer2 = new iTextSharp.text.Paragraph("")
                {
                    SpacingBefore = 100f,
                    SpacingAfter = 70f,
                };

                var titleFont = FontFactory.GetFont("Arial", 32, BaseColor.BLACK);
                var numberFont = FontFactory.GetFont("Arial", 26, BaseColor.BLACK);
                var dateFont = FontFactory.GetFont("Arial", 14, BaseColor.BLACK);
                var docTitle = new iTextSharp.text.Paragraph("FAKTURA VAT", titleFont);
                var docNumber = new iTextSharp.text.Paragraph("NR " + showInvoice.Number, numberFont);
                docTitle.Alignment = Element.ALIGN_CENTER;
                docNumber.Alignment = Element.ALIGN_CENTER;
                var creationDateVariable = new iTextSharp.text.Paragraph("Data wystawienia: " + showInvoice.CreationDate, dateFont);
                var secondDateVariable = new iTextSharp.text.Paragraph("Data wykonania usługi: " + showInvoice.DateOfIssue, dateFont);
                var paymentTypeVariable = new iTextSharp.text.Paragraph("Forma platności: " + showInvoice.PaymentType, dateFont);
                var accountNumberVariable = new iTextSharp.text.Paragraph("Numer konta: " + showInvoice.AccountNumber, dateFont);
                var sellerParagraph = new iTextSharp.text.Paragraph("Dane sprzedawcy", dateFont);
                var buyerParagraph = new iTextSharp.text.Paragraph("Dane nabywcy", dateFont);
                creationDateVariable.Alignment = Element.ALIGN_RIGHT;
                secondDateVariable.Alignment = Element.ALIGN_RIGHT;
                paymentTypeVariable.Alignment = Element.ALIGN_RIGHT;
                accountNumberVariable.Alignment = Element.ALIGN_RIGHT;
                sellerParagraph.Alignment = Element.ALIGN_LEFT;
                buyerParagraph.Alignment = Element.ALIGN_RIGHT;




                pdfDoc.Add(docTitle);
                pdfDoc.Add(docNumber);
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(creationDateVariable);
                pdfDoc.Add(secondDateVariable);
                pdfDoc.Add(paymentTypeVariable);                


                if(showInvoice.PaymentType == "Przelew")
                {
                    pdfDoc.Add(accountNumberVariable);
                }
                pdfDoc.Add(spacer);



                var headerTable = new PdfPTable(new[] { .5f, .5f })
                {                  
                    HorizontalAlignment = 0,
                    WidthPercentage = 40,
                    DefaultCell = { MinimumHeight = 22f }
                };

                var cellSeller = new PdfPCell(new Phrase("Dane sprzedawcy"))
                {
                    Colspan = 2,
                    HorizontalAlignment = 1,
                    MinimumHeight = 30f
                };
                var cellBuyer = new PdfPCell(new Phrase("Dane kupującego"))
                {
                    Colspan = 2,
                    HorizontalAlignment = 1,
                    MinimumHeight = 30f
                };



                headerTable.AddCell(cellSeller);
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
                headerTable.TotalWidth = 240f;
                headerTable.WriteSelectedRows(0, -1, pdfDoc.Left, pdfDoc.Top-205, writer.DirectContent);


                var headerTable2 = new PdfPTable(new[] { .5f, .5f })
                {
                    HorizontalAlignment = 2,
                    WidthPercentage = 40,
                    DefaultCell = { MinimumHeight = 22f }
                };

                headerTable2.AddCell(cellBuyer);
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
                headerTable2.TotalWidth=240f;
                headerTable2.WriteSelectedRows(0, -1, pdfDoc.Left + 305, pdfDoc.Top - 205, writer.DirectContent);


                pdfDoc.Add(spacer);
                //pdfDoc.Add(placeTable);
                pdfDoc.Add(spacer);
                //pdfDoc.Add(headerTable);
                //pdfDoc.Add(headerTable2);
                pdfDoc.Add(spacer2);



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


                var vatTable = new PdfPTable(new[] { .75f, .75f, .75f })
                {
                    HorizontalAlignment = 2,
                    WidthPercentage = 40,
                    DefaultCell = { MinimumHeight = 22f }
                };

                vatTable.AddCell("Wartosc netto [pln]");
                vatTable.AddCell("VAT [%]");
                vatTable.AddCell("Wartosc brutto [pln]");

                Dictionary<int, int> vatValue = vatValuesMethod();
                Dictionary<int, int> vatValueBrutto = vatValuesBruttoMethod();

                foreach (KeyValuePair<int, int> entry in vatValue)
                {
                    vatTable.AddCell(entry.Value.ToString());
                    vatTable.AddCell(entry.Key.ToString());
                    vatTable.AddCell(vatValueBrutto[entry.Key].ToString());
                }

                int sum = wholeBruttoPrice(vatValueBrutto);

                var amountPaid = new iTextSharp.text.Paragraph("Zaplacono: " + sum.ToString() + " pln", dateFont);
                amountPaid.Alignment = Element.ALIGN_LEFT;
                

                pdfDoc.Add(table);
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(vatTable);
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(amountPaid);

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
