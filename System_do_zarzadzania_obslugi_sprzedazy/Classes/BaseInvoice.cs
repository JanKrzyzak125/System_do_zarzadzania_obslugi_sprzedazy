using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    public class BaseInvoice
    {
        private int id;
        private int number;
        private string creationDate;
        private string paid;

        [DisplayName("ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("Numer")]
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        [DisplayName("Data wystawienia")]
        public string CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        [DisplayName("Zapłacono")]
        public string Paid
        {
            get { return paid; }
            set { paid = value; }
        }

        public BaseInvoice()
        {

        }

        public BaseInvoice(int id, int number, string creationDate, string paid)
        {
            Id = id;
            Number = number;
            CreationDate = creationDate;
            Paid = paid;
        }
    }
}
