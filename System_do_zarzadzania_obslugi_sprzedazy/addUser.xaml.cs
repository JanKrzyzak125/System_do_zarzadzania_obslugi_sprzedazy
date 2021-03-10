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
    /// Interaction logic for addUser.xaml
    /// </summary>
    public partial class addUser : Window
    {
        public addUser()
        {
            InitializeComponent();
        }

        private void AddUsr(object sender, RoutedEventArgs e)
        {
            
            string companyName = CompName.Text;
            string nip = NIP.Text;
            string city = City.Text;
            string street = Street.Text;
            string phoneNumber = PhNum.Text;
            string email = Mail.Text;
            Company company = new Company(companyName,nip,city,street,phoneNumber,email);

            SQLiteDataAccess.SaveUser(company);
            this.Close();


        }
    }
}
