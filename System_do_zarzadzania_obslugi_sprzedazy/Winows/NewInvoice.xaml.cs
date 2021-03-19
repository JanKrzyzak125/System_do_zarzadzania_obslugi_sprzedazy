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
            int idSeller = Int32.Parse(IdSeller.Text);
            int idCompany = Int32.Parse(IdCompany.Text);
            string number = Number.Text;
            string creationDate = CreationDate.Text;
            string saleDate = SaleDate.Text;
            string paymentType = PaymentType.Text;
            string paymentDeadline = PaymentDeadline.Text;
            string toPay = ToPay.Text;
            string toPayInWord = ToPayInWord.Text;
            string paid = Paid.Text;
            string dateOfIssue = DateOfIssue.Text;
            string nameOfService = NameOfService.Text;
            Invoice invoice = new Invoice(idSeller, idCompany, number, creationDate, saleDate, paymentType, paymentDeadline, toPay,
            toPayInWord, paid, dateOfIssue, nameOfService);
            SQLiteDataAccess.SaveInvoice(invoice);
            this.Close();
        }
    }
}
