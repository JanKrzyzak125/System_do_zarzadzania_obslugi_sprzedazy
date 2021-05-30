using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System_do_zarzadzania_obslugi_sprzedazy.Classes;

namespace System_do_zarzadzania_obslugi_sprzedazy.Winows
{
    /// <summary>
    /// Okienko, które pozwala dodać nowy produkt do bazy danych oraz na magazyn
    /// </summary>
    public partial class NewProduct : Window
    {
        List<Product> products;
        private int invoiceId;
        private int companyId;
        private bool isChanged = true;
        List<string> unitNames;
        List<Product> productBackup;

        /// <summary>
        /// Konstruktor, który przekazuje dane firmy oraz faktury i inicjalizuje komponenty 
        /// okienka Nowy produkt.
        /// </summary>
        public NewProduct(int invoiceId, int companyId)
        {
            this.invoiceId = invoiceId;
            this.companyId = companyId;
            products = SQLiteDataAccess.LoadProducts(companyId);
            InitializeComponent();
            InvoiceID.Text = invoiceId.ToString();
            ProductNameComboBox.ItemsSource = products;
            unitNames = SQLiteDataAccess.LoadQuantityUnitName();
            ProductQuantityUnitComboBox.ItemsSource = unitNames;
        }

        /// <summary>
        /// Metoda co aktualizuje dane nowego produktu 
        /// </summary>
        private void ProductNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ProductNameComboBox.SelectedItem!=null)
            {
                isChanged = true;
                Product product = ProductNameComboBox.SelectedItem as Product;
                ProductName.Text = product.Name;
                ProductID.Text = product.Id.ToString();
                ProductVat.Text = product.Vat;
                ProductNettoPrice.Text = product.NetPrice;
                ProductBruttoPrice.Text = product.GrossValue;
                isChanged = false;
            }
        }

        /// <summary>
        /// Metoda co aktualizuje lub dodaje nową jednostkę miary produktu 
        /// </summary>
        private void ProductQuantityUnitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            if (ProductQuantityUnitComboBox.SelectedItem != null) 
            {
                isChanged = true;
                ProductQuantityUnit.Text = ProductQuantityUnitComboBox.SelectedItem as string;
                isChanged = false;
            }
        }

        /// <summary>
        /// Metoda co dodaje do faktury produkt (wpisanymi danymi)
        /// </summary>
        private void AddProductToInvoice_Click(object sender, RoutedEventArgs e)
        {
            int idInvoice = Int32.Parse(InvoiceID.Text);
            int idProduct = Int32.Parse(ProductID.Text);
            string productName = ProductNameComboBox.Text;
            int quantity = Int32.Parse(ProductQuantity.Text);
            string quantityUnits = ProductQuantityUnitComboBox.Text;
            double nettoPrice = Double.Parse(ProductNettoPrice.Text);
            double bruttoPrice = Double.Parse(ProductBruttoPrice.Text);
            int vat = Int32.Parse(ProductVat.Text);
            int id=0;
            int productQuantity=0;

            foreach(Product product1 in products)
            {
                if(product1.Id == idProduct)
                {
                    id = product1.Id;
                    productQuantity = Int32.Parse(product1.Quantity);
                }
            }

            if(!String.IsNullOrEmpty(ProductQuantityUnitComboBox.Text))
            {
                if (!isAlreadyExisting(products, productName))
                {
                    Product product = new Product(productName, "0", nettoPrice.ToString(), vat.ToString(), "0", bruttoPrice.ToString());
                    int IdProduct = SQLiteDataAccess.LoadAiCompanyId("Product")[0] + 1;
                    SQLiteDataAccess.SaveProductToCustomer(IdProduct, 1);
                    SQLiteDataAccess.SaveProduct(product);
                    InvoiceProduct invoiceProduct = new InvoiceProduct(idInvoice, idProduct, productName, quantity, quantityUnits, nettoPrice, bruttoPrice, vat);
                    if (unitNames.IndexOf(quantityUnits) == -1)
                    {
                        int idUnitname = SQLiteDataAccess.LoadAiCompanyId("QuantityUnit")[0] + 1;
                        SQLiteDataAccess.SaveUnitName(idUnitname, quantityUnits);
                        SQLiteDataAccess.SaveInvoiceProduct(invoiceProduct, idUnitname);
                    }
                    else
                    {
                        SQLiteDataAccess.SaveInvoiceProduct(invoiceProduct, unitNames.IndexOf(quantityUnits) + 1);
                    }
                    this.Close();
                }

                if(productQuantity - quantity >= 0)
                {
                    SQLiteDataAccess.UpdateProductQuantity(productQuantity, id);
                    InvoiceProduct invoiceProduct = new InvoiceProduct(idInvoice, idProduct, productName, quantity, quantityUnits, nettoPrice, bruttoPrice, vat);
                    if (unitNames.IndexOf(quantityUnits) == -1)
                    {
                        int idUnitname = SQLiteDataAccess.LoadAiCompanyId("QuantityUnit")[0] + 1;
                        SQLiteDataAccess.SaveUnitName(idUnitname, quantityUnits);
                        SQLiteDataAccess.SaveInvoiceProduct(invoiceProduct, idUnitname);
                    }
                    else
                    {
                        SQLiteDataAccess.SaveInvoiceProduct(invoiceProduct, unitNames.IndexOf(quantityUnits) + 1);
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Brak wybranej ilości produktów na magazynie");
                }
                
            }
        }

        /// <summary>
        /// Metoda co sprawdza, czy istnieje już ten produkt co został wpisany.
        /// </summary>
        private bool isAlreadyExisting(List<Product> products, string productName)
        {
            bool exist = false;
            foreach(var product in products)
            {
                if (productName.Equals(product.Name))
                {
                    exist = true;
                }
            }
            return exist;
        }

        /// <summary>
        /// Zmiana Brutto i vat na podstawie ceny netto
        /// </summary>
        private void ProductNettoPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ProductBruttoPrice.IsFocused&&!isChanged)
            {
                if (!string.IsNullOrEmpty(ProductNettoPrice.Text))
                {
                    double Netto = double.Parse(ProductNettoPrice.Text);
                    double Vat = double.Parse(ProductVat.Text) / 100;
                    double Gross = (Netto + Netto * Vat);
                    ProductBruttoPrice.Text = Gross.ToString();
                }
            }
        }

        /// <summary>
        /// Zmiana netto i vat na podstawie ceny brutto
        /// </summary>
        private void ProductBruttoPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ProductNettoPrice.IsFocused&&!isChanged)
            {
                if (!string.IsNullOrEmpty(ProductBruttoPrice.Text))
                {
                    double Vat = double.Parse(ProductVat.Text) / 100;
                    double Gross = double.Parse(ProductBruttoPrice.Text);
                    double VatPrice = (Gross * Vat) / (1 + Vat);
                    double Netto = (Gross - VatPrice);
                    ProductNettoPrice.Text = Netto.ToString();
                }
            }
        }

        /// <summary>
        /// Zmiana jednostki produktu
        /// </summary>
        private void ProductQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ProductQuantity.IsFocused&&!isChanged)
            {
                if(!string.IsNullOrEmpty(ProductQuantity.Text)&&!string.IsNullOrEmpty(ProductNettoPrice.Text)&&!string.IsNullOrEmpty(ProductVat.Text))
                {
                    double Netto = double.Parse(ProductNettoPrice.Text);
                    Netto = Netto*double.Parse(ProductQuantity.Text);
                    double Vat = double.Parse(ProductVat.Text) / 100;
                    double Gross = Netto + Netto * Vat;
                    ProductNettoPrice.Text = Netto.ToString();
                    ProductBruttoPrice.Text = Gross.ToString();
                }
            }
        }

        /// <summary>
        /// Zmiana nazwy produktu oraz aktualizacja 
        /// </summary>
        private void ProductNameComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter_param = ProductNameComboBox.Text;
            if (String.IsNullOrWhiteSpace(filter_param))
            {
                ProductNameComboBox.ItemsSource = products;
                ProductNameComboBox.IsDropDownOpen = false;
            }
            else
            {
                List<Product> productBackup = products.FindAll(x => x.StartsWith(filter_param));
                ProductNameComboBox.ItemsSource = productBackup;
                ProductNameComboBox.SelectedIndex = -1;
                ProductNameComboBox.Text = filter_param;
                ProductNameComboBox.ItemsSource = productBackup;
                ProductNameComboBox.IsDropDownOpen = true;
            }
        }

        /// <summary>
        /// Metoda, która gdy otwieramy ona jest do autouzupełniania nazw produktów
        /// </summary>
        private void ProductNameComboBox_DropDownOpened_1(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)((ComboBox)sender).Template.FindName("PART_EditableTextBox", (ComboBox)sender);
            textBox.SelectionStart = ((ComboBox)sender).Text.Length;
            textBox.SelectionLength = 0;
        }
    }
}
