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
            SQLiteDataAccess.SaveProduct(Product);
            this.Close();

        }

        
    }
}
