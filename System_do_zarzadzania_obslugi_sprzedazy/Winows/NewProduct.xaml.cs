using System;
using System.Collections;
using System.Collections.Generic;
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

//W linii 89 mamy na sztywno ustawione id konsumenta
namespace System_do_zarzadzania_obslugi_sprzedazy.Winows
{
    /// <summary>
    /// Interaction logic for NewProduct.xaml
    /// </summary>
    public partial class NewProduct : Window
    {
        List<Product> products;
        private int invoiceId;
        private int companyId;
        private bool isChanged = true;
        List<string> unitNames;
        List<Product> productBackup;

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

        private void ProductQuantityUnitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            if (ProductQuantityUnitComboBox.SelectedItem != null) 
            {
                isChanged = true;
                ProductQuantityUnit.Text = ProductQuantityUnitComboBox.SelectedItem as string;
                

                isChanged = false;
            }
        }


        private void AddProductToInvoice_Click(object sender, RoutedEventArgs e)
        {
            int idInvoice = Int32.Parse(InvoiceID.Text);
            int idProduct = Int32.Parse(ProductID.Text);
            string productName = ProductNameComboBox.Text;
            int quantity = Int32.Parse(ProductQuantity.Text);
            string quantityUnits = ProductQuantityUnitComboBox.Text;
            int nettoPrice = Int32.Parse(ProductNettoPrice.Text);
            int bruttoPrice = Int32.Parse(ProductBruttoPrice.Text);
            int vat = Int32.Parse(ProductVat.Text);

            //Bedzie trzeba sprawdzac wszystkie kontrolki czy sa puste

            if(!String.IsNullOrEmpty(ProductQuantityUnitComboBox.Text))
            {
                //Wykorzystamy metode indexof w celu wyciagniecia numeru indexu danego elementu z listy unitname jesli index = -1 nalezy dodac nowy element do listy
                if (!isAlreadyExisting(products, productName))
                {
                    Product product = new Product(productName, "0", nettoPrice.ToString(), vat.ToString(), "0", bruttoPrice.ToString());
                    int IdProduct = SQLiteDataAccess.LoadAiCompanyId("Product")[0] + 1;
                    SQLiteDataAccess.SaveProductToCustomer(IdProduct, 1);
                    SQLiteDataAccess.SaveProduct(product);
                    //W linii 89 mamy na sztywno ustawione id konsumenta
                }


                InvoiceProduct invoiceProduct = new InvoiceProduct(idInvoice, idProduct, productName, quantity, quantityUnits, nettoPrice, bruttoPrice, vat);
                if (unitNames.IndexOf(quantityUnits) == -1)
                {

                    //Wyciagnac numer indexu ktore nadamy nowododanemu produktowi jednostki
                    //Zaladowac do tabeli ktora ma nazwy jednostki nowy produkt z tym indeksem
                    //dodac nowy produkt do faktury gdzie zamiast index of uzyjemy nowy index
                    int idUnitname = SQLiteDataAccess.LoadAiCompanyId("QuantityUnit")[0] + 1;
                    SQLiteDataAccess.SaveUnitName(idUnitname,quantityUnits );
                    SQLiteDataAccess.SaveInvoiceProduct(invoiceProduct,idUnitname);
                }
                else
                {
                    SQLiteDataAccess.SaveInvoiceProduct(invoiceProduct, unitNames.IndexOf(quantityUnits) + 1);
                }
                this.Close();
            }
        }


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

        private void ProductNameComboBox_DropDownOpened_1(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)((ComboBox)sender).Template.FindName("PART_EditableTextBox", (ComboBox)sender);
            textBox.SelectionStart = ((ComboBox)sender).Text.Length;
            textBox.SelectionLength = 0;
        }
    }
}
