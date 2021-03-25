using System;
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

        public NewProduct(int invoiceId, int companyId)
        {
            this.invoiceId = invoiceId;
            this.companyId = companyId;
            products = SQLiteDataAccess.LoadProducts(companyId);
            InitializeComponent();
            InvoiceID.Text = invoiceId.ToString();
            ProductNameComboBox.ItemsSource = products;
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

        private void AddProductToInvoice_Click(object sender, RoutedEventArgs e)
        {
            int idInvoice = Int32.Parse(InvoiceID.Text);
            int idProduct = Int32.Parse(ProductID.Text);
            string productName = ProductName.Text;
            int quantity = Int32.Parse(ProductQuantity.Text);
            string quantityUnits = ProductQuantityUnit.Text;
            int nettoPrice = Int32.Parse(ProductNettoPrice.Text);
            int bruttoPrice = Int32.Parse(ProductBruttoPrice.Text);
            int vat = Int32.Parse(ProductVat.Text);
            InvoiceProduct invoiceProduct = new InvoiceProduct(idInvoice, idProduct, productName, quantity, quantityUnits, nettoPrice, bruttoPrice, vat);
            SQLiteDataAccess.SaveInvoiceProduct(invoiceProduct);
            this.Close();
        }

        private void ProductNettoPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ProductBruttoPrice.IsFocused&&!isChanged)
            {
                if (!string.IsNullOrEmpty(ProductNettoPrice.Text))
                {
                    double Netto = double.Parse(ProductNettoPrice.Text);
                    double Vat = double.Parse(ProductVat.Text) / 100;
                    double Gross = Netto + Netto * Vat;
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
                    double Netto = Gross - VatPrice;
                    ProductNettoPrice.Text = Netto.ToString();
                }
            }
        }
    }
}
