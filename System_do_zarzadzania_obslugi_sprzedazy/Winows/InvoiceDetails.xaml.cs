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
using Microsoft.Win32;

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
        private StorageOperations storageOperation;

        private int invoiceID;
        private int companyID;
        List<InvoiceProduct> invoiceProducts;
        List<Company> companyName;
        List<Seller> companySellerName;
        List<EditedInvoiceProduct> editedInvoiceProducts = new List<EditedInvoiceProduct>();

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
            IdSeller.Text = showCompanySeller.Name + " " + showCompanySeller.Surname;

            
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
            EditPdfCB.IsChecked = false;

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

        private Dictionary<int, int> vatValuesMethod(int a)
        {
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            foreach(EditedInvoiceProduct product in editedInvoiceProducts)
            {
                if (keyValuePairs.ContainsKey(product.EditedVat))
                {
                    keyValuePairs[product.EditedVat] += product.EditedNettoPrice;
                }
                else
                {
                    keyValuePairs.Add(product.EditedVat, product.EditedNettoPrice);
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

        private Dictionary<int, int> vatValuesBruttoMethod(int y)
        {
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            foreach (EditedInvoiceProduct product in editedInvoiceProducts)
            {
                if (keyValuePairs.ContainsKey(product.EditedVat))
                {
                    keyValuePairs[product.EditedVat] += product.EditedBruttoPrice;
                }
                else
                {
                    keyValuePairs.Add(product.EditedVat, product.EditedBruttoPrice);
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

                DateTime date2 = DateTime.Now;
                String savedate = date2.ToString("G");
                savedate = savedate.Replace(":", ";");
                savedate = savedate.Replace("/", ".");
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
                saveFileDialog1.FileName = showInvoice.Number + " " + savedate;
                
                saveFileDialog1.InitialDirectory= @"c:\";
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


                    if (showInvoice.PaymentType == "Przelew")
                    {
                        pdfDoc.Add(accountNumberVariable);
                    }
                    pdfDoc.Add(spacer);



                    var headerTable = new PdfPTable(new[] { .5f, .5f })
                    {
                        HorizontalAlignment = 0,
                        WidthPercentage = 40,
                        DefaultCell = { MinimumHeight = 22f },

                    };

                    var cellSeller = new PdfPCell(new Phrase("Dane sprzedawcy"))
                    {
                        Colspan = 2,
                        HorizontalAlignment = 1,
                        MinimumHeight = 30f
                    };

                    cellSeller.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    var cellBuyer = new PdfPCell(new Phrase("Dane nabywcy"))
                    {
                        Colspan = 2,
                        HorizontalAlignment = 1,
                        MinimumHeight = 30f
                    };
                    cellBuyer.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);


                    headerTable.AddCell(cellSeller);
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Nazwa Firmy", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.CompanyName, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Nip", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Adres", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Street + "\n" + showCompanyName.City, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Numer Telefonu", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.PhoneNumber, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("E-mail", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Email, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.TotalWidth = 240f;
                    headerTable.WriteSelectedRows(0, -1, pdfDoc.Left, pdfDoc.Top - 220, writer.DirectContent);


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
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Name, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        headerTable2.AddCell("NIP");
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        headerTable2.AddCell("REGON");
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    }
                    else
                    {
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Imię", tableFont)));
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Name, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        headerTable2.AddCell("Nazwisko");
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Surname, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    }
                    headerTable2.AddCell("Adres");
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Street + "\n" + showCompanySeller.City, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable2.AddCell("Numer Telefonu");
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.NumberPhone, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable2.TotalWidth = 240f;
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

                    PdfPCell cell1 = new PdfPCell(new iTextSharp.text.Paragraph("Nazwa Produktu", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell2 = new PdfPCell(new iTextSharp.text.Paragraph("JM", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell3 = new PdfPCell(new iTextSharp.text.Paragraph("Ilość", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell4 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Netto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell5 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Brutto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell6 = new PdfPCell(new iTextSharp.text.Paragraph("Podatek VAT [%]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell2.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell3.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell4.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell5.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell6.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                    table.AddCell(cell4);
                    table.AddCell(cell5);
                    table.AddCell(cell6);

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



                    PdfPCell cell7 = new PdfPCell(new iTextSharp.text.Paragraph("Wartość netto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell8 = new PdfPCell(new iTextSharp.text.Paragraph("VAT [%]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell9 = new PdfPCell(new iTextSharp.text.Paragraph("Wartość brutto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    cell7.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell8.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell9.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    vatTable.AddCell(cell7);
                    vatTable.AddCell(cell8);
                    vatTable.AddCell(cell9);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało utworzyć się pliku pdf", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void GenerateStorageOperations()
        {
            try
            {
                
                DateTime date2 = DateTime.Now;
                String savedate = date2.ToString("G");
                savedate = savedate.Replace(":", ";");
                savedate = savedate.Replace("/", ".");
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
                saveFileDialog1.FileName = "WZ " + showInvoice.Number + " "+ savedate;
                saveFileDialog1.InitialDirectory = @"c:\";
                if (saveFileDialog1.ShowDialog() == true)
                {
                    System.IO.FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                    var pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);
                    pdfDoc.Open();

                    var titleFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 28);
                    var numberFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 22);
                    var serviceFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 16);
                    var dateFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 14);
                    var smallFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 11);
                    var tableFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 12);


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


                    var mainParagraph = new iTextSharp.text.Paragraph("Wydanie zewnętrzne (WZ) nr " + showInvoice.Number, numberFont);
                    var storage = new iTextSharp.text.Paragraph("Magazyn: Główny magazyn", dateFont);
                    var creationDate = new iTextSharp.text.Paragraph("Data wydania: " + showInvoice.CreationDate, smallFont);
                    mainParagraph.Alignment = Element.ALIGN_CENTER;
                    storage.Alignment = Element.ALIGN_LEFT;
                    creationDate.Alignment = Element.ALIGN_RIGHT;

                    pdfDoc.Add(creationDate);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(mainParagraph);
                    pdfDoc.Add(spacer);


                    var headerTable = new PdfPTable(new[] { .5f, .5f })
                    {
                        HorizontalAlignment = 0,
                        WidthPercentage = 40,
                        DefaultCell = { MinimumHeight = 22f },

                    };

                    var cellSeller = new PdfPCell(new Phrase("Dostawca"))
                    {
                        Colspan = 2,
                        HorizontalAlignment = 1,
                        MinimumHeight = 30f
                    };

                    cellSeller.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    var cellBuyer = new PdfPCell(new Phrase("Odbiorca"))
                    {
                        Colspan = 2,
                        HorizontalAlignment = 1,
                        MinimumHeight = 30f
                    };
                    cellBuyer.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);


                    headerTable.AddCell(cellSeller);
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Nazwa Firmy", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.CompanyName, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Nip", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Adres", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Street + "\n" + showCompanyName.City, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Numer Telefonu", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.PhoneNumber, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("E-mail", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Email, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.TotalWidth = 240f;
                    headerTable.WriteSelectedRows(0, -1, pdfDoc.Left, pdfDoc.Top - 180, writer.DirectContent);


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
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Name, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        headerTable2.AddCell("NIP");
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        headerTable2.AddCell("REGON");
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    }
                    else
                    {
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Imię", tableFont)));
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Name, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        headerTable2.AddCell("Nazwisko");
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Surname, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    }
                    headerTable2.AddCell("Adres");
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Street + "\n" + showCompanySeller.City, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable2.AddCell("Numer Telefonu");
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.NumberPhone, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable2.TotalWidth = 240f;
                    headerTable2.WriteSelectedRows(0, -1, pdfDoc.Left + 305, pdfDoc.Top - 180, writer.DirectContent);


                    pdfDoc.Add(spacer2);
                    pdfDoc.Add(spacer3);
                    pdfDoc.Add(storage);


                    var columnCount = 7;
                    var columnWidths = new[] { 2.2f, 1f, 1f, 1.5f, 1.5f, 1.5f, 1.5f };

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

                    PdfPCell cell1 = new PdfPCell(new iTextSharp.text.Paragraph("Nazwa Produktu", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell2 = new PdfPCell(new iTextSharp.text.Paragraph("JM", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    PdfPCell cell3 = new PdfPCell(new iTextSharp.text.Paragraph("Ilość", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    PdfPCell cell4 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Netto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    PdfPCell cell5 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Brutto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    PdfPCell cell6 = new PdfPCell(new iTextSharp.text.Paragraph("Podatek VAT [%]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    PdfPCell cell7 = new PdfPCell(new iTextSharp.text.Paragraph("Wartość [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell2.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell3.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell4.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell5.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell6.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell7.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                    table.AddCell(cell4);
                    table.AddCell(cell5);
                    table.AddCell(cell6);
                    table.AddCell(cell7);

                    int sum = 0;

                    invoiceProducts.ForEach(a =>
                    {
                        table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.ProductName, tableFont)));
                        table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.QuantityUnitName, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.Quantity.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.NettoPrice.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.BruttoPrice.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.Vat.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new iTextSharp.text.Phrase((a.Quantity * a.BruttoPrice).ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        sum += a.Quantity * a.BruttoPrice;
                    });

                    pdfDoc.Add(spacer);
                    pdfDoc.Add(table);

                    var sumTable = new PdfPTable(new[] { .75f })
                    {
                        HorizontalAlignment = 2,
                        WidthPercentage = 14.7f,
                        DefaultCell = { MinimumHeight = 22f }
                    };


                    sumTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(sum.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });

                    pdfDoc.Add(sumTable);

                    pdfDoc.Close();
                    writer.Close();
                    fs.Close();
                }
                int id = SQLiteDataAccess.LoadAiCompanyId("StorageInformation")[0]-1;
                storageOperation = new StorageOperations(id,"Wydanie zewnętrzne", DateTime.Now.ToString("dd/MM/yyyy"), showCompanyName.CompanyName, showCompanySeller.Name + " " + showCompanySeller.Surname);
                SQLiteDataAccess.SaveOperation(storageOperation, 4, showInvoice.Id);
            }
            catch(Exception ex)
            {

            }
        }

        private void ExportEditedPDF()
        {
            try
            {

                DateTime date2 = DateTime.Now;
                String savedate = date2.ToString("G");
                savedate = savedate.Replace(":", ";");
                savedate = savedate.Replace("/", ".");
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
                saveFileDialog1.FileName = "Korekta faktury nr. " + showInvoice.Number + " " + savedate;

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

                    int correctionID = SQLiteDataAccess.LoadAiCompanyId("CorrectedPdf")[0]+1;

                    StringBuilder stringBuilder = new StringBuilder("");
                    stringBuilder.Append(correctionID.ToString());
                    stringBuilder.Append("." + showInvoice.Number);

                    var titleFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 28);
                    var numberFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 22);
                    var serviceFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 16);
                    var dateFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 14);
                    var smallFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 11);
                    var tableFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1257, 12);
                    var docTitle = new iTextSharp.text.Paragraph("KOREKTA FAKTURY VAT", titleFont);
                    var docNumber = new iTextSharp.text.Paragraph("NR " + stringBuilder.ToString(), numberFont);
                    docTitle.Alignment = Element.ALIGN_CENTER;
                    docNumber.Alignment = Element.ALIGN_CENTER;
                    var nameOfService = new iTextSharp.text.Paragraph("Na wykonanie: " + NameOfService.Text, serviceFont);
                    var creationDateVariable = new iTextSharp.text.Paragraph("Data wystawienia: " + CreationDate.Text, dateFont);
                    var secondDateVariable = new iTextSharp.text.Paragraph("Data wykonania usługi: " + DateOfIssue.Text, dateFont);
                    var paymentTypeVariable = new iTextSharp.text.Paragraph("Forma płatności: " + PaymentTypeCB.Text, dateFont);
                    var accountNumberVariable = new iTextSharp.text.Paragraph("Numer konta: " + AccountNumber.Text, dateFont);
                    var sellerParagraph = new iTextSharp.text.Paragraph("Dane sprzedawcy", dateFont);
                    var buyerParagraph = new iTextSharp.text.Paragraph("Dane nabywcy", dateFont);
                    var dateOfEdit = new iTextSharp.text.Paragraph("Data korekty: " + DateOfCoretion.Text, dateFont);
                    var correction = new iTextSharp.text.Paragraph("Powód korekty: " + Corection.Text, dateFont);
                    dateOfEdit.Alignment = Element.ALIGN_RIGHT;
                    correction.Alignment = Element.ALIGN_LEFT;
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
                    pdfDoc.Add(dateOfEdit);
                    pdfDoc.Add(paymentTypeVariable);


                    if (PaymentTypeCB.Text.Equals("Przelew"))
                    {
                        pdfDoc.Add(accountNumberVariable);
                    }
                    pdfDoc.Add(spacer);



                    var headerTable = new PdfPTable(new[] { .5f, .5f })
                    {
                        HorizontalAlignment = 0,
                        WidthPercentage = 40,
                        DefaultCell = { MinimumHeight = 22f },

                    };

                    var cellSeller = new PdfPCell(new Phrase("Dane sprzedawcy"))
                    {
                        Colspan = 2,
                        HorizontalAlignment = 1,
                        MinimumHeight = 30f
                    };

                    cellSeller.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    var cellBuyer = new PdfPCell(new Phrase("Dane nabywcy"))
                    {
                        Colspan = 2,
                        HorizontalAlignment = 1,
                        MinimumHeight = 30f
                    };
                    cellBuyer.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);


                    headerTable.AddCell(cellSeller);
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Nazwa Firmy", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.CompanyName, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Nip", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Adres", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Street + "\n" + showCompanyName.City, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Numer Telefonu", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.PhoneNumber, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("E-mail", tableFont)));
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanyName.Email, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.TotalWidth = 240f;
                    headerTable.WriteSelectedRows(0, -1, pdfDoc.Left, pdfDoc.Top - 235, writer.DirectContent);


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
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Name, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        headerTable2.AddCell("NIP");
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        headerTable2.AddCell("REGON");
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    }
                    else
                    {
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Imię", tableFont)));
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Name, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        headerTable2.AddCell("Nazwisko");
                        headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Surname, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    }
                    headerTable2.AddCell("Adres");
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.Street + "\n" + showCompanySeller.City, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable2.AddCell("Numer Telefonu");
                    headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(showCompanySeller.NumberPhone, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable2.TotalWidth = 240f;
                    headerTable2.WriteSelectedRows(0, -1, pdfDoc.Left + 305, pdfDoc.Top - 235, writer.DirectContent);

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

                    PdfPCell cell1 = new PdfPCell(new iTextSharp.text.Paragraph("Nazwa Produktu", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell2 = new PdfPCell(new iTextSharp.text.Paragraph("JM", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell3 = new PdfPCell(new iTextSharp.text.Paragraph("Ilość", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell4 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Netto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell5 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Brutto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell6 = new PdfPCell(new iTextSharp.text.Paragraph("Podatek VAT [%]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell2.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell3.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell4.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell5.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell6.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                    table.AddCell(cell4);
                    table.AddCell(cell5);
                    table.AddCell(cell6);

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
                        WidthPercentage = 38.1f,
                        DefaultCell = { MinimumHeight = 22f }
                    };





                    List<InvoiceProduct> localProducts = new List<InvoiceProduct>();

                    foreach(InvoiceProduct invoiceProduct in InvoiceProductListDataGrid.ItemsSource)
                    {
                        localProducts.Add(invoiceProduct);
                    }    


                    foreach(InvoiceProduct invoiceProduct1 in localProducts)
                    {
                        EditedInvoiceProduct invoiceProduct = new EditedInvoiceProduct();
                        invoiceProduct.ConvertInvoiceProduct(invoiceProduct1);
                        editedInvoiceProducts.Add(invoiceProduct);
                    }


                    var table2 = new PdfPTable(columnWidths)
                    {
                        HorizontalAlignment = 0,
                        WidthPercentage = 100,
                        DefaultCell = { MinimumHeight = 22f }
                    };


                    PdfPCell cell7 = new PdfPCell(new iTextSharp.text.Paragraph("Nazwa Produktu", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell8 = new PdfPCell(new iTextSharp.text.Paragraph("JM", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell9 = new PdfPCell(new iTextSharp.text.Paragraph("Ilość", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell10 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Netto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell11= new PdfPCell(new iTextSharp.text.Paragraph("Cena Brutto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell12 = new PdfPCell(new iTextSharp.text.Paragraph("Podatek VAT [%]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    cell7.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell8.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell9.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell10.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell11.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell12.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    table2.AddCell(cell7);
                    table2.AddCell(cell8);
                    table2.AddCell(cell9);
                    table2.AddCell(cell10);
                    table2.AddCell(cell11);
                    table2.AddCell(cell12);

                    editedInvoiceProducts.ForEach(a =>
                    {
                        table2.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.EditedProductName, tableFont)));
                        table2.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.EditedQuantityUnit, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        table2.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.EditedQuantity.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        table2.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.EditedNettoPrice.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        table2.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.EditedBruttoPrice.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                        table2.AddCell(new PdfPCell(new iTextSharp.text.Phrase(a.EditedVat.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    });

                    PdfPCell cell13 = new PdfPCell(new iTextSharp.text.Paragraph("Wartość netto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell14 = new PdfPCell(new iTextSharp.text.Paragraph("VAT [%]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    PdfPCell cell15 = new PdfPCell(new iTextSharp.text.Paragraph("Wartość brutto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                    cell7.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell8.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    cell9.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    vatTable.AddCell(cell13);
                    vatTable.AddCell(cell14);
                    vatTable.AddCell(cell15);

                    Dictionary<int, int> vatValue = vatValuesMethod(1);
                    Dictionary<int, int> vatValueBrutto = vatValuesBruttoMethod(1);


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
                        WidthPercentage = 38.1f,
                        DefaultCell = { MinimumHeight = 22f }
                    };


                    sumTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(sumNetto.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    sumTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase("-")) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    sumTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(sum.ToString())) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    

                    pdfDoc.Add(table);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(table2);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(vatTable);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(sumTable);
                    pdfDoc.Add(spacer3);
                    pdfDoc.Add(amountPaid);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(spacer);
                    pdfDoc.Add(correction);
                    pdfDoc.Close();
                    writer.Close();
                    fs.Close();
                    InvoiceCorrection invoiceCorrection = new InvoiceCorrection(correctionID, stringBuilder.ToString(), DateOfCoretion.Text, Corection.Text, invoiceID);
                    SQLiteDataAccess.SaveCorrectedInvoice(invoiceCorrection);

                    foreach(EditedInvoiceProduct editedInvoiceProduct in editedInvoiceProducts)
                    {
                        SQLiteDataAccess.SaveCorrectedInvoiceProduct(editedInvoiceProduct);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało utworzyć się pliku pdf", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void CreatePDF_Click(object sender, RoutedEventArgs e)
        {
            if(invoiceProducts.Count != 0)
            {
                ExportToPDF();
                GenerateStorageOperations();
                MessageBox.Show("Plik pdf został utworzony");
            }
            else
            {
                MessageBox.Show("Brak produktów w fakturze", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
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
            Corection.Visibility=Visibility.Visible;
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
            InvoiceProductListDataGrid.IsReadOnly = true;
            InvoiceProductListDataGrid.Columns[0].Visibility = Visibility.Visible;
            InvoiceProductListDataGrid.Columns[1].Visibility = Visibility.Visible;
            LoadInvoiceList();
        }

        private void CreateEditedPdf_Click(object sender, RoutedEventArgs e)
        {
            invoiceProducts = SQLiteDataAccess.LoadInvoicesProduct(invoiceID);
            ExportEditedPDF();
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
    }
}
