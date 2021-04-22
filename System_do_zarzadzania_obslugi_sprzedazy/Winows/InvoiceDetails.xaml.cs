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

                var spacer3 = new iTextSharp.text.Paragraph("")
                {
                    SpacingBefore = 50f,
                    SpacingAfter = 30f,
                };

                var titleFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 28);
                var numberFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 22);
                var serviceFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 16);
                var dateFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 14);
                var smallFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 11);
                var tableFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 12);
                var docTitle = new iTextSharp.text.Paragraph("FAKTURA VAT", titleFont);
                var docNumber = new iTextSharp.text.Paragraph("NR " + showInvoice.Number, numberFont);
                docTitle.Alignment = Element.ALIGN_CENTER;
                docNumber.Alignment = Element.ALIGN_CENTER;
                var nameOfService = new iTextSharp.text.Paragraph("Na wykonanie: " + showInvoice.NameOfService, serviceFont);
                var creationDateVariable = new iTextSharp.text.Paragraph("Data wystawienia: " + showInvoice.CreationDate, dateFont);
                var secondDateVariable = new iTextSharp.text.Paragraph("Data wykonania usługi: " + showInvoice.DateOfIssue, dateFont);
                var paymentTypeVariable = new iTextSharp.text.Paragraph("Forma płatności: " + showInvoice.PaymentType, dateFont);
                var accountNumberVariable = new iTextSharp.text.Paragraph("Numer konta: " + showInvoice.AccountNumber, dateFont);
                var sellerParagraph = new iTextSharp.text.Paragraph("Dane sprzedawcy", dateFont);
                var buyerParagraph = new iTextSharp.text.Paragraph("Dane nabywcy", dateFont);
                creationDateVariable.Alignment = Element.ALIGN_RIGHT;
                secondDateVariable.Alignment = Element.ALIGN_RIGHT;
                paymentTypeVariable.Alignment = Element.ALIGN_RIGHT;
                accountNumberVariable.Alignment = Element.ALIGN_RIGHT;
                sellerParagraph.Alignment = Element.ALIGN_LEFT;
                buyerParagraph.Alignment = Element.ALIGN_RIGHT;
                nameOfService.Alignment = Element.ALIGN_CENTER;




                pdfDoc.Add(docTitle);
                pdfDoc.Add(docNumber);
                pdfDoc.Add(nameOfService);
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
                var cellBuyer = new PdfPCell(new Phrase("Dane nabywcy"))
                {
                    Colspan = 2,
                    HorizontalAlignment = 1,
                    MinimumHeight = 30f
                };



                headerTable.AddCell(cellSeller);
                headerTable.AddCell("Nazwa Firmy");
                headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.CompanyName, tableFont)));
                headerTable.AddCell("Nip");
                headerTable.AddCell(showCompanyName.Nip);
                headerTable.AddCell("Adres");
                headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Street + "\n" + showCompanyName.City, tableFont)));
                headerTable.AddCell("Numer Telefonu");
                headerTable.AddCell(showCompanyName.PhoneNumber);
                headerTable.AddCell("E-mail");
                headerTable.AddCell(showCompanyName.Email);
                headerTable.TotalWidth = 240f;
                headerTable.WriteSelectedRows(0, -1, pdfDoc.Left, pdfDoc.Top-220, writer.DirectContent);


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
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Name, tableFont)));
                    headerTable2.AddCell("NIP");
                    headerTable2.AddCell(showCompanySeller.Nip);
                    headerTable2.AddCell("REGON");
                    headerTable2.AddCell(showCompanySeller.Regon);
                }
                else
                {
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Imię", tableFont)));
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Name, tableFont)));
                    headerTable2.AddCell("Nazwisko");
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Surname, tableFont)));
                }
                headerTable2.AddCell("Adres");
                headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Street + "\n" + showCompanySeller.City, tableFont)));
                headerTable2.AddCell("Numer Telefonu");
                headerTable2.AddCell(showCompanySeller.NumberPhone);
                headerTable2.TotalWidth=240f;
                headerTable2.WriteSelectedRows(0, -1, pdfDoc.Left + 305, pdfDoc.Top - 220, writer.DirectContent);

                pdfDoc.Add(spacer);
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

                table.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Nazwa Produktu", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT });
                table.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("JM", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Ilość", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Cena Netto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Cena Brutto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Podatek VAT [%]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });

                invoiceProducts.ForEach(a =>
                {
                    table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.ProductName, tableFont)));
                    table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.QuantityUnitName, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.Quantity.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.NettoPrice.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.BruttoPrice.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.Vat.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                });


                var vatTable = new PdfPTable(new[] { .75f, .75f, .75f })
                {
                    HorizontalAlignment = 2,
                    WidthPercentage = 40,
                    DefaultCell = { MinimumHeight = 22f }
                };

                vatTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Wartość netto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                vatTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("VAT [%]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                vatTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Wartość brutto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });

                Dictionary<int, int> vatValue = vatValuesMethod();
                Dictionary<int, int> vatValueBrutto = vatValuesBruttoMethod();
                

                foreach (KeyValuePair<int, int> entry in vatValue)
                {
                    vatTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(entry.Value.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    vatTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(entry.Key.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    vatTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(vatValueBrutto[entry.Key].ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                }

                int sum = wholeBruttoPrice(vatValueBrutto);
                int sumNetto = wholeBruttoPrice(vatValue);

                var amountPaid = new iTextSharp.text.Paragraph("Zapłacono: " + sum.ToString() + " zł", dateFont);
                amountPaid.Alignment = Element.ALIGN_LEFT;


                var sumTable = new PdfPTable(new[] { .75f, .75f, .75f })
                {
                    HorizontalAlignment = 2,
                    WidthPercentage = 40,
                    DefaultCell = { MinimumHeight = 22f }
                };


                sumTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(sumNetto.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                sumTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase("-")) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                sumTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(sum.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });

                pdfDoc.Add(table);
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(vatTable);
                pdfDoc.Add(spacer);
                pdfDoc.Add(sumTable);
                pdfDoc.Add(spacer3);
                pdfDoc.Add(amountPaid);
                pdfDoc.Close();
                writer.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się zapisać pliku pdf");
            }
        }

        private void CreatePDF_Click(object sender, RoutedEventArgs e)
        {
            ExportToPDF();
            MessageBox.Show("Plik pdf został utworzony");
        }
    }
}
