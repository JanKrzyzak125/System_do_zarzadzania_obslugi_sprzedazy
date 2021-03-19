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

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Interaction logic for InvoiceDetails.xaml
    /// </summary>
    public partial class InvoiceDetails : Window
    {

        private Invoice showInvoice;
        public InvoiceDetails(Invoice invoice)
        {
            showInvoice = invoice;
            InitializeComponent();
            ID.Text = showInvoice.Id.ToString();
            IdSeller.Text = showInvoice.IdSeller.ToString();
            IdCompany.Text = showInvoice.IdCompany.ToString();
            Number.Text = showInvoice.Number.ToString();
            CreationDate.Text = showInvoice.CreationDate;
            SaleDate.Text = showInvoice.SaleDate;
            PaymentType.Text = showInvoice.PaymentType;
            PaymentDeadline.Text = showInvoice.PaymentDeadline;
            ToPay.Text = showInvoice.ToPay;
            ToPayInWord.Text = showInvoice.ToPayInWords;
            Paid.Text = showInvoice.Paid;
        }
    }
}
