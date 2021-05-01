using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    class Debter
    {

        private string fullName;
        private int iD;
        private int debt;
        private string invoiceNumber;


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
