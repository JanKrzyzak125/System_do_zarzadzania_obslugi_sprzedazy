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
    /// Interaction logic for NewInvoice.xaml
    /// </summary>
    public partial class NewInvoice : Window
    {
        public NewInvoice()
        {
            InitializeComponent();
        }

        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            int id = ID.Text.toInt();
            int idSeller = IdSeller.Text.toInt();
            int idCompany = IdCompany.Text.toInt();
            string idProduct = idProduct.Text;
            int number = Number.Text.toInt();
            string creationDate = CreationDate.Text;
            string saleDate = SaleDate.Text;
            int paymentType = PaymentType.Text.toInt();
            int paymentDeadline = PaymentDeadline.Text.toInt();
            string toPay = ToPay.Text;
            string toPayInWord = ToPayInWord.Text;
            string paid = Paid.Text;
            string remarks = Remarks.Text;
            Invoice invoice = new Invoice(id, idSeller, idCompany, idProduct, number, creationDate, saleDate, paymentType, paymentDeadline, toPay,
            toPayInWord, paid, remarks);
            SQLiteDataAccess.SaveInvoice(invoice);
            this.close();
        }
    }
}
