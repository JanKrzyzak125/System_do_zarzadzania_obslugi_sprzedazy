using System.Collections.Generic;
using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    /// <summary>
    /// Klasa Debtor z informacjami odnośnie dłużników
    /// </summary>
    public class Debter
    {
        private string fullName;
        private int iD;
        private int debt;
        private string invoiceNumber;
        private Dictionary<string, int> invoiceDictionaty = new Dictionary<string, int>();

        /// <summary>
        /// Imię i nazwisko Dłużnika
        /// </summary>
        [DisplayName ("Imię i nazwisko")]
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        /// <summary>
        /// Numer ID dłużnika 
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <summary>
        /// Dług dłużnika
        /// </summary>
        [DisplayName("Dług")]
        public int Debt
        {
            get { return debt; }
            set { debt = value; }
        }

        /// <summary>
        /// Numer faktury
        /// </summary>
        [DisplayName("Numer faktury")]
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; }
        }

        /// <summary>
        /// Metoda, która dodaje do słownika faktury z długami
        /// </summary>
        /// <param name="invoiceNumber">Numer faktury</param>
        /// <param name="debtAmount">Dany dług</param>
        public void AddToInvoiceDictionary(string invoiceNumber, int debtAmount)
        {
            invoiceDictionaty.Add(invoiceNumber, debtAmount);

        }

        /// <summary>
        /// Metoda, która sumuje dług
        /// </summary>
        /// <param name="debt"></param>
        public void AddDebts(int debt)
        {
            Debt += debt;
        }

        /// <summary>
        /// Metoda, która odsłania słownik długu
        /// </summary>
        /// <returns>zwraca słownik długu</returns>
        public Dictionary<string, int> returnList()
        {
            return invoiceDictionaty;
        }

        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public Debter()
        {

        }

        /// <summary>
        /// Konstruktor przeładowany danymi
        /// </summary>
        /// <param name="fullName">Pełne imię z nazwiskiem</param>
        /// <param name="iD">Numer dłużnika</param>
        /// <param name="debt">Dług</param>
        /// <param name="invoiceNumber">Numery faktur</param>
        public Debter(string fullName, int iD, int debt, string invoiceNumber)
        {
            FullName = fullName;
            ID = iD;
            Debt = debt;
            InvoiceNumber = invoiceNumber;
        }
    }
}