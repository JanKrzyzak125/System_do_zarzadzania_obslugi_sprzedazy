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
        public NewProduct(int invoiceId, int companyId)
        {
            this.invoiceId = invoiceId;
            this.companyId = companyId;
            products = SQLiteDataAccess.LoadInvoiceProduct(this.companyId);
            InitializeComponent();
            InvoiceID.Text = invoiceId.ToString();
            ProductNameComboBox.ItemsSource = products;
        }

        private void ProductNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ProductNameComboBox.SelectedItem!=null)
            {
                Product product = ProductNameComboBox.SelectedItem as Product;
                ProductName.Text = product.Name;
                ProductID.Text = product.Id.ToString();
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

        
    }
}
