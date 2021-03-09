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
            Firma f = new Firma();

            f.Nazwa_Firmy = CompName.Text;
            f.Nip = NIP.Text;
            f.Miasto = City.Text;
            f.Ulica = Street.Text;
            f.Numer_Telefonu = PhNum.Text;
            f.Email = Mail.Text;

           SQLiteDataAccess.SaveUser(f);


        }
    }
}
