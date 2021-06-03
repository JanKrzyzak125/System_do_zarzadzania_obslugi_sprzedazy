namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Klasa Company zawiera informacje na temat firmy
    /// </summary>
    public class Company
    {
        private int companyID;

        private string companyName;
        private string nip;
        private string city;
        private string street;
        private string phoneNumber;
        private string email;

        /// <summary>
        /// Id Firmy
        /// </summary>
        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                companyID = value;
            }
        }

        /// <summary>
        /// Nazwa Firmy
        /// </summary>
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }

        /// <summary>
        /// Nip firmy
        /// </summary>
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
        /// Nazwa miasta w, którym jest zajestrowana firma
        /// </summary>
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
        /// Nazwa ulicy, na której się znajduje firma
        /// </summary>
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
        /// Numer telefonu do firmy
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
            }
        }

        /// <summary>
        /// Poczta interetowa do firmy
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public Company()
        {

        }

        /// <summary>
        /// Konstruktor klasy przeładowany informacjami o firmie(używany jest do zapisania)
        /// </summary>
        /// <param name="companyID">Numer Id firmy</param>
        /// <param name="companyName">Nazwa firmy</param>
        /// <param name="nip">NIP firmy</param>
        /// <param name="city">Nazwa Miasta</param>
        /// <param name="street">Nazwa ulicy</param>
        /// <param name="phoneNumber">Numer telefonu do firmy</param>
        /// <param name="email">Numer Email do firmy</param>
        public Company(int companyID, string companyName, string nip, string city, string street, string phoneNumber, string email)
        {
            CompanyID = companyID;
            CompanyName = companyName;
            Nip = nip;
            City = city;
            Street = street;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        /// <summary>
        /// Konstruktor klasy przeładowany informacjami o firmie(bez numeru Firmy)
        /// </summary>
        /// <param name="companyName">Nazwa firmy</param>
        /// <param name="nip">NIP firmy</param>
        /// <param name="city">Nazwa Miasta</param>
        /// <param name="street">Nazwa ulicy</param>
        /// <param name="phoneNumber">Numer telefonu do firmy</param>
        /// <param name="email">Numer Email do firmy</param>
        public Company(string companyName, string nip, string city, string street, string phoneNumber, string email)
        {
            CompanyName = companyName;
            Nip = nip;
            City = city;
            Street = street;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
