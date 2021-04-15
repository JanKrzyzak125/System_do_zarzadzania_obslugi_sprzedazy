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
    /// Logika interakcji dla klasy AddNewSeller.xaml
    /// </summary>
    public partial class AddNewSeller : Window
    {
        public AddNewSeller()
        {
            InitializeComponent();
        }

        private void addSeller_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string surname = Surname.Text;
            string city = City.Text;
            string street = Street.Text;
            string phonenumber = PhNum.Text;
            string nip = Nip.Text;
            string regon = Regon.Text;
            Seller seller = new Seller(name, surname, city, street, phonenumber, nip, regon) ;
            SQLiteDataAccess.SaveSeller(seller);
            this.Close();

        }

        private void RadioButton_person(object sender, RoutedEventArgs e)
        {
            Name_Label.Content = "Imie";
            Surname_Label.Visibility = Visibility.Visible;
            Nip_Label.Visibility = Visibility.Hidden;
            Regon_Label.Visibility = Visibility.Hidden;
            Surname.Visibility = Visibility.Visible;
            Nip.Visibility = Visibility.Hidden;
            Regon.Visibility = Visibility.Hidden;
        }

        private void RadioButton_Company(object sender, RoutedEventArgs e)
        {
            Name_Label.Content = "Nazwa Firmy";
            Surname_Label.Visibility = Visibility.Hidden;
            Nip_Label.Visibility = Visibility.Visible;
            Regon_Label.Visibility = Visibility.Visible;
            Nip.Visibility = Visibility.Visible;
            Regon.Visibility = Visibility.Visible;
            Surname.Visibility = Visibility.Hidden;

        }
    }
}
