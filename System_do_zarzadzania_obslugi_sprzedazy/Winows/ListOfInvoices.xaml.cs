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

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Interaction logic for ListOfInvoices.xaml
    /// </summary>
    public partial class ListOfInvoices : Window
    {
        private List<Invoice> loadedInvoices;
        public ListOfInvoices()
        {
            loadedInvoices = SQLiteDataAccess.LoadInvoices();
            InitializeComponent();
            InvoicesListView.ItemsSource = loadedInvoices;
        }

        private void generate_Invoice(object sender, RoutedEventArgs e)
        {
            NewInvoice newInvoice = new NewInvoice();
            newInvoice.Show();
        }

        private void show_Invoice(object sender, RoutedEventArgs e)
        {
            if(InvoicesListView.SelectedItem != null)
            {
                Invoice inv = InvoicesListView.SelectedItem as Invoice;
                InvoiceDetails invoiceDetails = new InvoiceDetails(inv, inv.Id, inv.IdCompany);
                invoiceDetails.Show();
            }
        }

        private void close_ListOfInvoices(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
