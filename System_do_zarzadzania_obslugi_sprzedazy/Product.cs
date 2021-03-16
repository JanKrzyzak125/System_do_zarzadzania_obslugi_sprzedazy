using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    class Product
    {
        private int id;
        
        private string name;
        
        private string quantity;
        
        private string netPrice;
        
        private string vat;
        
        private string vatValue;
        
        private string grossValue;
        
        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public string NetPrice
        {
            get { return netPrice; }
            set { netPrice = value; }
        }
        public string Vat
        {
            get { return vat; }
            set { vat = value; }
        }
        public string VatValue
        {
            get { return vatValue; }
            set { vatValue = value; }
        }
        public string GrossValie
        {
            get { return grossValue; }
            set { grossValue = value; }
        }
    }
}