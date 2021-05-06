using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    public class Debter
    {

        private string fullName;
        private int iD;
        private int debt;
        private string invoiceNumber;
        private Dictionary<string, int> invoiceDictionaty = new Dictionary<string, int>();


        [DisplayName ("Imię i nazwisko")]
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }


        [DisplayName("Dług")]
        public int Debt
        {
            get { return debt; }
            set { debt = value; }
        }

        [DisplayName("Numer faktury")]
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; }
        }


        public void AddToInvoiceDictionary(string invoiceNumber, int debtAmount)
        {
            invoiceDictionaty.Add(invoiceNumber, debtAmount);

        }

        public void AddDebts(int debt)
        {
            Debt += debt;
        }



        public Debter()
        {

        }

        public Debter(string fullName, int iD, int debt, string invoiceNumber)
        {
            FullName = fullName;
            ID = iD;
            Debt = debt;
            InvoiceNumber = invoiceNumber;
        }

    }
}
