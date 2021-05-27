using System;
using System.Windows;
using System.Windows.Controls;
using System_do_zarzadzania_obslugi_sprzedazy.Classes;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Okienko, które pozwala stworzyć nowa fakturę i dodaje do bazy danych
    /// </summary>
    public partial class NewInvoice : Window
    {
        /// <summary>
        /// Konstruktor okienka Nowa faktura, która inicjalizuje komponenty okienka
        /// </summary>
        public NewInvoice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda, która obsługuje wcisnięcie przyciska Dodaj, która pozwala stworzyć nowa 
        /// fakturę i dodaje do bazy danych oraz zamyka okienko 
        /// </summary>
        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            int num = SQLiteDataAccess.LoadAiCompanyId("Database_for_invoices")[0]+1;
            int idSeller = Int32.Parse(IdSeller.Text);
            int idCompany = Int32.Parse(IdCompany.Text);
            string creationDate = CreationDate.Text;
            string saleDate = SaleDate.Text;
            string paymentType = PaymentTypeComboBox.Text;
            string paymentDeadline = PaymentDeadline.Text;
            string toPay = ToPay.Text;
            string toPayInWord = ToPayInWord.Text;
            string paid = Paid.Text;
            string dateOfIssue = DateOfIssue.Text;
            string nameOfService = NameOfService.Text;
            string[] date = creationDate.Split('.');
            string number = num.ToString()+"."+date[1]+"."+date[2];
            string accountNumber = AccountNumber.Text;
            Invoice invoice = new Invoice(idSeller, idCompany, number, creationDate, saleDate, paymentType, paymentDeadline, toPay,
            toPayInWord, paid, dateOfIssue, nameOfService, accountNumber);
            SQLiteDataAccess.SaveInvoice(invoice);
            this.Close();
        }

        /// <summary>
        /// Metoda, która zmienia sposób płatności od wyboru użytkownika
        /// </summary>
        private void PaymentTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PaymentTypeComboBox.SelectedItem!=null)
            {
                if(PaymentTypeComboBox.SelectedItem == Gotowka)
                {
                    AccountNumber.Visibility = Visibility.Hidden;
                    AccountNumberLabel.Visibility = Visibility.Hidden;
                    AccountNumber.Clear();
                }
                else
                {
                    AccountNumber.Visibility = Visibility.Visible;
                    AccountNumberLabel.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
