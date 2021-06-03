using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Faktura, która zawiera podstawowe informacje
    /// </summary>
    public class BaseInvoice
    {

        private int id;
        private string number;
        private string creationDate;
        private string paid;

        /// <summary>
        /// Id faktury 
        /// </summary>
        [DisplayName("ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Numer faktury
        /// </summary>
        [DisplayName("Numer")]
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        /// <summary>
        /// Data wystawienia faktury
        /// </summary>
        [DisplayName("Data wystawienia")]
        public string CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        /// <summary>
        /// Informacja na temat zapłaconych pieniędzy za faktury
        /// </summary>
        [DisplayName("Zapłacono")]
        public string Paid
        {
            get { return paid; }
            set { paid = value; }
        }

        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public BaseInvoice()
        {
        }

        /// <summary>
        /// Konstruktor faktury
        /// </summary>
        /// <param name="id">Id faktury</param>
        /// <param name="number">Numer faktury</param>
        /// <param name="creationDate">Data utworzenia faktury</param>
        /// <param name="paid">Informacja czy zapłacono za fakturę</param>
        public BaseInvoice(int id, string number, string creationDate, string paid)
        {
            Id = id;
            Number = number;
            CreationDate = creationDate;
            Paid = paid;
        }
    }
}
