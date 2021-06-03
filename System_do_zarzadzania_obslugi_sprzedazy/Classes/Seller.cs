using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Klasa Seller, która zawiera informacje dotyczące Kontrahentów
    /// </summary>
    class Seller
    {
        private int idSeller;

        private string name;
        private string surname;
        private string city;
        private string street;
        private string numberPhone;
        private string nip;
        private string regon;

        /// <summary>
        /// ID Sprzedawcy
        /// </summary>
        [DisplayName("ID Sprzedawcy")]
        public int IdSeller
        {
            get
            {
                return idSeller;
            }
            set
            {
                idSeller = value;
            }
        }

        /// <summary>
        /// Imię Kontrahenta
        /// </summary>
        [DisplayName("Imię")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Nazwisko kontrahenta
        /// </summary>
        [DisplayName("Nazwisko")]
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }

        /// <summary>
        /// Miasto Kontrahenta
        /// </summary>
        [DisplayName("Miasto")]
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        /// <summary>
        /// Nazwa ulicy kontrahenta
        /// </summary>
        [DisplayName("Ulica")]
        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                street = value;
            }
        }

        /// <summary>
        /// Numer telefonu do kontrahenta
        /// </summary>
        [DisplayName("Numer telefonu")]
        public string NumberPhone
        {
            get
            {
                return numberPhone;
            }
            set
            {
                numberPhone = value;
            }
        }

        /// <summary>
        /// NIP kontrahenta
        /// </summary>
        [DisplayName("NIP")]
        public string Nip
        {
            get
            {
                return nip;
            }
            set
            {
                nip = value;
            }
        }

        /// <summary>
        /// REGON kontrahenta
        /// </summary>
        [DisplayName("REGON")]
        public string Regon
        {
            get { return regon; }
            set { regon = value; }
        }

        /// <summary>
        /// Domyślny konstruktor klasy Seller
        /// </summary>
        public Seller()
        {

        }

        /// <summary>
        /// Przeładowany konstruktor danymi
        /// </summary>
        /// <param name="idSeller">ID kontrahenta</param>
        /// <param name="name">Imię Kontrahenta</param>
        /// <param name="surname">Nazwisko Kontrahenta</param>
        /// <param name="city">Nazwa Miasta kontrahenta</param>
        /// <param name="street">Nazwa Ulicy kontrahenta</param>
        /// <param name="numberPhone">Numer telefonu</param>
        /// <param name="nip">Numer NIP</param>
        /// <param name="regon">numer REGON</param>
        public Seller(int idSeller, string name, string surname, string city, string street, string numberPhone, string nip, string regon) : this(name, surname, city, street, numberPhone, nip, regon)
        {
            IdSeller = idSeller;
        }

        /// <summary>
        /// Przeładowany konstruktor danymi
        /// </summary>
        /// <param name="name">Imię Kontrahenta</param>
        /// <param name="surname">Nazwisko Kontrahenta</param>
        /// <param name="city">Nazwa Miasta kontrahenta</param>
        /// <param name="street">Nazwa Ulicy kontrahenta</param>
        /// <param name="numberPhone">Numer telefonu</param>
        /// <param name="nip">Numer NIP</param>
        /// <param name="regon">numer REGON</param>
        public Seller(string name, string surname, string city, string street, string numberPhone, string nip, string regon)
        {
            Name = name;
            Surname = surname;
            City = city;
            Street = street;
            NumberPhone = numberPhone;
            Nip = nip;
            Regon = regon;
        }
    }
}
