using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

    public partial class StorageAdditionalOperations : Window
    {

        Product products;

        public StorageAdditionalOperations(Product product)
        {
            InitializeComponent();
            adoptionRB.IsChecked = true;
            products = product;
            ProductName.Text = product.Name;
            StorageQuantityTB.Text = product.Quantity.ToString();

        }

        private void adoptionRB_Checked(object sender, RoutedEventArgs e)
        {
            StorageQuantityL.Visibility = Visibility.Hidden;
            StorageQuantityTB.Visibility = Visibility.Hidden;

        }

        private void issueRB_Checked(object sender, RoutedEventArgs e)
        {
            adoptionRB.IsChecked = false;
            StorageQuantityL.Visibility = Visibility.Visible;
            StorageQuantityTB.Visibility = Visibility.Visible;
        }

        private void createStoragePDF()
        {
            StringBuilder stringBuilder1 = new StringBuilder("");
            StringBuilder stringBuilder2 = new StringBuilder("");

            if (adoptionRB.IsChecked == true)
            {
                stringBuilder1.Append("PW ");
                stringBuilder2.Append("Przyjęcie wewnętrzne (PW) ");
            }
            else
            {
                stringBuilder1.Append("WW ");
                stringBuilder2.Append("Wydanie wewnętrzne (WW) ");
            }
            try
            {
                DateTime date2 = DateTime.Now;
                String savedate = date2.ToString("G");
                savedate = savedate.Replace(":", ";");
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
                saveFileDialog1.FileName = stringBuilder1.ToString() + products.Name + savedate;
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

                    DateTime date = DateTime.Today;

                    var mainParagraph = new iTextSharp.text.Paragraph(stringBuilder2.ToString(), numberFont);
                    var storage = new iTextSharp.text.Paragraph("Magazyn: Główny magazyn", dateFont);
                    var creationDate = new iTextSharp.text.Paragraph("Data wydania: " + date.ToString("d"), smallFont);
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
                        WidthPercentage = 100,
                        DefaultCell = { MinimumHeight = 22f },

                    };

                    PdfPCell cell1 = new PdfPCell(new iTextSharp.text.Paragraph("Nazwa produktu", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    PdfPCell cell2 = new PdfPCell(new iTextSharp.text.Paragraph("Ilość", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                    cell2.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                    headerTable.AddCell(cell1);
                    headerTable.AddCell(cell2);
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(products.Name, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                    headerTable.AddCell(new PdfPCell(new iTextSharp.text.Phrase(UserQuantity.Text.ToString(), tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });

                    pdfDoc.Add(spacer);
                    pdfDoc.Add(headerTable);


                    pdfDoc.Close();
                    writer.Close();
                    fs.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się utworzyć pdf", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CreatePDFbutton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(UserQuantity.Text.ToString()))
            {
                int StorageProductID = SQLiteDataAccess.LoadAiCompanyId("StorageProduct")[0] + 1;

                if (issueRB.IsChecked == true)
                {
                    if (Int32.Parse(products.Quantity) - (Int32.Parse(UserQuantity.Text)) > 0)
                    {
                        products.Quantity = (Int32.Parse(products.Quantity) - Int32.Parse(UserQuantity.Text)).ToString();
                        createStoragePDF();
                        MessageBox.Show("Utworzono plik pdf");
                        SQLiteDataAccess.SaveProductInformation(products, Int32.Parse(UserQuantity.Text));
                        SQLiteDataAccess.SaveOperation(products, 3, StorageProductID);
                        SQLiteDataAccess.UpdateProductQuantity(products);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Brak tylu produktów na magazynie", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    products.Quantity = (Int32.Parse(products.Quantity) + Int32.Parse(UserQuantity.Text)).ToString();
                    createStoragePDF();
                    MessageBox.Show("Utworzono plik pdf");
                    SQLiteDataAccess.SaveProductInformation(products, Int32.Parse(UserQuantity.Text));
                    SQLiteDataAccess.SaveOperation(products, 1, StorageProductID);
                    SQLiteDataAccess.UpdateProductQuantity(products);
                    this.Close();
                }

            }
        }
    }
}
