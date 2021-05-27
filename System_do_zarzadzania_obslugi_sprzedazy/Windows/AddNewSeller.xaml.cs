using System.Windows;

namespace System_do_zarzadzania_obslugi_sprzedazy.Winows
{
    /// <summary>
    /// Okno które pozwala na dodawanie nowego kontrahenta.
    /// </summary>
    public partial class AddNewSeller : Window
    {
        /// <summary>
        /// Konstruktor, który inicjalizuje komponenty okienka dodawanie nowego kontrahenta
        /// </summary>
        public AddNewSeller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda z przycisku Dodaj, która przekazuje dane z textboxów i tworzy nowy obiekt Seller
        /// oraz dodaje do bazy danych nowego kontrahenta.
        /// </summary>
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

        /// <summary>
        /// Metoda co odsłaia potrzebne rzeczy dla kontrahenta, który jest osoba fizyczna
        /// </summary>
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

        /// <summary>
        /// Metoda co odsłaia potrzebne rzeczy dla kontrahenta, który jest firma
        /// </summary>
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