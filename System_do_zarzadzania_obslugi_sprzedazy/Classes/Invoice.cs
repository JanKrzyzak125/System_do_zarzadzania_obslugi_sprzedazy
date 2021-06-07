namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    /// <summary>
    /// klasa Invoice z glownymi danymi odnosnie faktur
    /// </summary>
    public class Invoice : BaseInvoice
    {
        private int idSeller;
        private int idCompany;
        private string saleDate;
        private string paymentType;
        private string paymentDeadline;
        private string toPayInWords;
        private string toPay;
        private string dateOfIssue;
        private string nameOfService;
        private string accountNumber;
        private int isPrinted;

        /// <summary>
        /// ID Sprzedawcy
        /// </summary>
        public int IdSeller
        {
            get { return idSeller; }
            set { idSeller = value; }
        }

        /// <summary>
        /// ID firmy
        /// </summary>
        public int IdCompany
        {
            get { return idCompany; }
            set { idCompany = value; }
        }

        /// <summary>
        /// Data sprzedazy
        /// </summary>
        public string SaleDate
        {
            get { return saleDate; }
            set { saleDate = value; }
        }

        /// <summary>
        /// Typ platnosci
        /// </summary>
        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }

        /// <summary>
        /// Termin platnosci
        /// </summary>
        public string PaymentDeadline
        {
            get { return paymentDeadline; }
            set { paymentDeadline = value; }
        }

        /// <summary>
        /// Czy faktura zostala zaplacona
        /// </summary>
        public string ToPay
        {
            get { return toPay; }
            set { toPay = value; }
        }

        /// <summary>
        /// Ilosc pieniedzy do zaplaty slownie
        /// </summary>
        public string ToPayInWords
        {
            get { return toPayInWords; }
            set { toPayInWords = value; }
        }

        /// <summary>
        /// Nazwa uslugi
        /// </summary>
        public string NameOfService
        {
            get { return nameOfService; }
            set { nameOfService = value; }
        }
       
        /// <summary>
        /// Data Wystawienia faktury
        /// </summary>
        public string DateOfIssue
        {
            get { return dateOfIssue; }
            set { dateOfIssue = value; }
        }    

        /// <summary>
        /// Numer konta
        /// </summary>
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        /// <summary>
        /// Czy jest wydrukowana do pdf
        /// </summary>
        public int IsPrinted
        {
            get { return isPrinted; }
            set { isPrinted = value; }
        }

        /// <summary>
        /// Konstruktor domyslny
        /// </summary>
        public Invoice()
        {

        }

        /// <summary>
        /// Konstruktor przeladowany danymi
        /// </summary>
        /// <param name="idSeller">ID sprzedawcy</param>
        /// <param name="idCompany">ID Firmy</param>
        /// <param name="number">Numer faktury</param>
        /// <param name="creationDate">Data stworzenia faktury</param>
        /// <param name="saleDate">Data sprzedazy</param>
        /// <param name="paymentType">Typ platnosci</param>
        /// <param name="paymentDeadline">Termin do zaplaty</param>
        /// <param name="toPay">ile zaplacono</param>
        /// <param name="toPayInWords">Slownie suma do zaplacenia</param>
        /// <param name="paid">Czy zaplacono</param>
        /// <param name="dateOfIssue">Data wystawienia</param>
        /// <param name="nameOfService">Nazwa uslugi</param>
        /// <param name="accountNumber">Numer konta bankowego</param>
        public Invoice(int idSeller, int idCompany, string number, string creationDate, string saleDate, string paymentType, string paymentDeadline, string toPay,
            string toPayInWords, string paid, string dateOfIssue, string nameOfService, string accountNumber)
        {
            IdSeller = idSeller;
            IdCompany = idCompany;
            Number = number;
            CreationDate = creationDate;
            SaleDate = saleDate;
            PaymentType = paymentType;
            PaymentDeadline = paymentDeadline;
            ToPay = toPay;
            ToPayInWords = toPayInWords;
            Paid = paid;
            DateOfIssue = dateOfIssue;
            NameOfService = nameOfService;
            AccountNumber = accountNumber;
           
            
        }

        /// <summary>
        /// Konstruktor przeladowany danymi
        /// </summary>
        /// <param name="id">ID faktury</param>
        /// <param name="idSeller">ID sprzedawcy</param>
        /// <param name="idCompany">ID Firmy</param>
        /// <param name="number">Numer faktury</param>
        /// <param name="creationDate">Data stworzenia faktury</param>
        /// <param name="saleDate">Data sprzedazy</param>
        /// <param name="paymentType">Typ platnosci</param>
        /// <param name="paymentDeadline">Termin do zaplaty</param>
        /// <param name="toPay">ile zaplacono</param>
        /// <param name="toPayInWords">Slownie suma do zaplacenia</param>
        /// <param name="paid">Czy zaplacono</param>
        /// <param name="dateOfIssue">Data wystawienia</param>
        /// <param name="nameOfService">Nazwa uslugi</param>
        /// <param name="accountNumber">Numer konta bankowego</param>
        public Invoice(int id, int idSeller,int idCompany, string number, string creationDate, string saleDate, string paymentType, string paymentDeadline, string toPay,
            string toPayInWords, string paid, string dateOfIssue, string nameOfService, string accountNumber)
        {
            Id = id;
            IdSeller = idSeller;
            IdCompany = idCompany;
            Number = number;
            CreationDate = creationDate;
            SaleDate = saleDate;
            PaymentType = paymentType;
            PaymentDeadline = paymentDeadline;
            ToPay = toPay;
            ToPayInWords = toPayInWords;
            Paid = paid;
            DateOfIssue = dateOfIssue;
            NameOfService = nameOfService;
            AccountNumber = accountNumber;
        }

        /// <summary>
        /// Konstruktor przeladowany danymi
        /// </summary>
        /// <param name="id">ID faktury</param>
        /// <param name="idSeller">ID sprzedawcy</param>
        /// <param name="idCompany">ID Firmy</param>
        /// <param name="number">Numer faktury</param>
        /// <param name="creationDate">Data stworzenia faktury</param>
        /// <param name="saleDate">Data sprzedazy</param>
        /// <param name="paymentType">Typ platnosci</param>
        /// <param name="paymentDeadline">Termin do zaplaty</param>
        /// <param name="toPay">ile zaplacono</param>
        /// <param name="toPayInWords">Slownie suma do zaplacenia</param>
        /// <param name="paid">Czy zaplacono</param>
        /// <param name="dateOfIssue">Data wystawienia</param>
        /// <param name="nameOfService">Nazwa uslugi</param>
        /// <param name="accountNumber">Numer konta bankowego</param>
        /// <param name="isPrinted">Czy jest wydrukowany</param>
        public Invoice(int id, int idSeller, int idCompany, string number, string creationDate, string saleDate, string paymentType, string paymentDeadline, string toPay,
                        string toPayInWords, string paid, string dateOfIssue, string nameOfService, string accountNumber, int isPrinted)
        {
            Id = id;
            IdSeller = idSeller;
            IdCompany = idCompany;
            Number = number;
            CreationDate = creationDate;
            SaleDate = saleDate;
            PaymentType = paymentType;
            PaymentDeadline = paymentDeadline;
            ToPay = toPay;
            ToPayInWords = toPayInWords;
            Paid = paid;
            DateOfIssue = dateOfIssue;
            NameOfService = nameOfService;
            AccountNumber = accountNumber;
            IsPrinted = isPrinted;
        }
    }
}