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

namespace System_do_zarzadzania_obslugi_sprzedazy.Winows
{
    /// <summary>
    /// Logika interakcji dla klasy AddNewProductMagazine.xaml
    /// </summary>
    public partial class AddNewProductMagazine : Window
    {
        public AddNewProductMagazine()
        {
            InitializeComponent();
        }

        private void AddProductToMagazine_Click(object sender, RoutedEventArgs e)
        {
            string Name = ProductName.Text;
            string Quantity = (ProductQuantity.Text);
            string NetPrice = (NetPrice1art.Text);
            string Vat = (ProductVat.Text);
            string VatValue = (ProductNettoPrice.Text);
            string GrossValue = (ProductBruttoPrice.Text);
            Product Product = new Product(Name, Quantity, NetPrice, Vat, VatValue, GrossValue);
            int IdProduct = SQLiteDataAccess.LoadAiCompanyId("Product")[0]+1;
            SQLiteDataAccess.SaveProductToCustomer(IdProduct, 1);
            SQLiteDataAccess.SaveProduct(Product);
            this.Close();
        }

        private void ProductNettoPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void ProductBruttoPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

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
