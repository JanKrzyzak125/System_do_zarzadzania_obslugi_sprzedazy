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
using System_do_zarzadzania_obslugi_sprzedazy.Classes;

namespace System_do_zarzadzania_obslugi_sprzedazy.Winows
{
    /// <summary>
    /// Logika interakcji dla klasy ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        private int invoiceID;
        List<InvoiceProduct> invoiceProducts;
        public ProductList(int ID)
        {
            InitializeComponent();
            invoiceID = ID;
            invoiceProducts = SQLiteDataAccess.LoadInvoicesProduct(invoiceID);
            InvoiceProductListDataGrid.ItemsSource = invoiceProducts;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            NewProduct newProduct = new NewProduct();
            newProduct.Show();
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
        }
    }
    }

