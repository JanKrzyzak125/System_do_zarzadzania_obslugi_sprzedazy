using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace System_do_zarzadzania_obslugi_sprzedazy.Winows
{
    /// <summary>
    /// Okienko, które pozwala dodawać nowy/e produkt/y do magazynu
    /// </summary>
    public partial class AddNewProductMagazine : Window
    {
        List<Seller> sellers = new List<Seller>();
        Company company = SQLiteDataAccess.LoadNameCompany(1)[0];

        /// <summary>
        /// Konstruktor okienka który inicjalizuje kontrolki ładujemy listę kontrahentów, która
        /// wczytujemy do comboboxa.
        /// </summary>
        public AddNewProductMagazine()
        {
            InitializeComponent();
            sellers = SQLiteDataAccess.LoadNameSeller(1);
            SellerCB.ItemsSource = sellers;
        }

        /// <summary>
        /// Metoda do przycisku dodaj produkt, który przekazuje dane z textboxów z, którego 
        /// tworzymy nowy obiekt Product i dodaje go do bazy danych. Na koniec tworzymy pdf 
        /// z przyjęcia zewnętrznego
        /// </summary>
        private void AddProductToMagazine_Click(object sender, RoutedEventArgs e)
        {
            string Name = ProductName.Text;
            string Quantity = ProductQuantity.Text;
            string NetPrice = NetPrice1art.Text;
            string Vat = ProductVat.Text;
            string VatValue = ProductNettoPrice.Text;
            string GrossValue = ProductBruttoPrice.Text;
            Product Product = new Product(Name, Quantity, NetPrice, Vat, VatValue, GrossValue);
            int IdProduct = SQLiteDataAccess.LoadAiCompanyId("Product")[0]+1;
            SQLiteDataAccess.SaveProductToCustomer(IdProduct, 1);
            SQLiteDataAccess.SaveProduct(Product);
            CreatePDF();
            this.Close();
        }

        /// <summary>
        /// Metoda, która generuje pdf z przyjęcia zewnętrznego. Używamy biblioteki iTextSharp.
        /// </summary>
        private void CreatePDF()
        {
            string date = DateTime.Now.ToString();
            date = date.Replace("/", "-");
            date = date.Replace(":", ";");
            Seller sellerCB = SellerCB.SelectedItem as Seller;

            try
            {
                System.IO.FileStream fs = new FileStream("D:/projekcik" + "\\" + "PZ " + date  + " " + ProductName.Text + ".pdf", FileMode.Create);
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

                var mainParagraph = new iTextSharp.text.Paragraph("Przyjęcie zewnętrzne (PZ)" , numberFont);
                var storage = new iTextSharp.text.Paragraph("Magazyn: Główny magazyn", dateFont);
                var creationDate = new iTextSharp.text.Paragraph("Data przyjęcia: " + DateTime.Now.ToString("d"), smallFont);
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
                headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Imię i nazwisko", tableFont)));
                headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(sellerCB.Name + " " + sellerCB.Surname, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Adres", tableFont)));
                headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(sellerCB.Street + "\n" + sellerCB.City, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph("Numer Telefonu", tableFont)));
                headerTable.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(sellerCB.NumberPhone, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                headerTable.TotalWidth = 240f;
                headerTable.WriteSelectedRows(0, -1, pdfDoc.Left, pdfDoc.Top - 180, writer.DirectContent);

                var headerTable2 = new PdfPTable(new[] { .5f, .5f })
                {
                    HorizontalAlignment = 2,
                    WidthPercentage = 40,
                    DefaultCell = { MinimumHeight = 22f }
                };

                headerTable2.AddCell(cellBuyer);
                headerTable2.AddCell("Nazwa Firmy");
                headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(company.CompanyName, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                headerTable2.AddCell("NIP");
                headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(company.Nip, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });                                            
                headerTable2.AddCell("Adres");
                headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(company.Street + "\n" + company.City, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                headerTable2.AddCell("Numer Telefonu");
                headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(company.PhoneNumber, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                headerTable2.AddCell("E-mail");
                headerTable2.AddCell(new PdfPCell(new iTextSharp.text.Paragraph(company.Email, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });

                headerTable2.TotalWidth = 240f;
                headerTable2.WriteSelectedRows(0, -1, pdfDoc.Left + 305, pdfDoc.Top - 180, writer.DirectContent);

                pdfDoc.Add(spacer2);
                pdfDoc.Add(spacer3);
                pdfDoc.Add(storage);

                var columnCount = 6;
                var columnWidths = new[] { 2.2f, 1f, 1f, 1.5f, 1.5f, 1.5f};

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
                PdfPCell cell3 = new PdfPCell(new iTextSharp.text.Paragraph("Ilość", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                PdfPCell cell2 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Netto za sztukę", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                PdfPCell cell4 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Netto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                PdfPCell cell5 = new PdfPCell(new iTextSharp.text.Paragraph("Cena Brutto [zł]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };
                PdfPCell cell6 = new PdfPCell(new iTextSharp.text.Paragraph("Podatek VAT [%]", tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER };

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
                table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(ProductName.Text, tableFont)));
                table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(ProductQuantity.Text, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(NetPrice1art.Text, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(ProductNettoPrice.Text, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(ProductBruttoPrice.Text, tableFont)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new iTextSharp.text.Phrase(ProductVat.Text)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER });

                pdfDoc.Add(spacer);
                pdfDoc.Add(table);

                pdfDoc.Close();
                writer.Close();
                fs.Close();
                MessageBox.Show("Utworzono plik pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się utworzyć pliku pdf" +  "_" +ex.Message);
            }
        }

        /// <summary>
        /// Metoda, która generuje ceny Netto, Brutto na podstawie Vatu.
        /// </summary>
        private void NetPrice1art_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(ProductQuantity.Text) && !string.IsNullOrEmpty(ProductVat.Text))
            {
                double Netto = (double.Parse(NetPrice1art.Text))*(double.Parse(ProductQuantity.Text));
                ProductNettoPrice.Text = Netto.ToString();
                double Vat = double.Parse(ProductVat.Text) / 100;
                double Gross = (Netto + Netto * Vat);
                ProductBruttoPrice.Text = Gross.ToString();
            }
        }
    }
}
