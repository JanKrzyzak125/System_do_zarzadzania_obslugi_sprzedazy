using System;
using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Klasa Product, ktora zawiera informacje odnosnie produktu
    /// </summary>
    public class Product : IEquatable<Product>
    {
        private int id;
        
        private string name;
        private string quantity;
        private string netPrice;
        private string vat;
        private string vatValue;
        private string grossValue;

        /// <summary>
        /// ID produktu
        /// </summary>
        [DisplayName("ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Nazwa Produktu
        /// </summary>
        [DisplayName("Nazwa Produktu")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Ilosc produktu
        /// </summary>
        [DisplayName("Ilosc")]
        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /// <summary>
        /// Cena Netto
        /// </summary>
        [DisplayName("Wartosc Netto ")]
        public string NetPrice
        {
            get { return netPrice; }
            set { netPrice = value; }
        }

        /// <summary>
        /// Procent Vat
        /// </summary>
        [DisplayName("Procent Vat")]
        public string Vat
        {
            get { return vat; }
            set { vat = value; }
        }

        /// <summary>
        /// Wartosc Vat
        /// </summary>
        [DisplayName("Wartosc Vat")]
        public string VatValue
        {
            get { return vatValue; }
            set { vatValue = value; }
        }

        /// <summary>
        /// Wartosc Brutto 
        /// </summary>
        [DisplayName("Wartosc Brutto")]
        public string GrossValue
        {
            get { return grossValue; }
            set { grossValue = value; }
        }

        /// <summary>
        /// Konstruktor domyslny
        /// </summary>
        public Product()
        {

        }

        /// <summary>
        /// Konstruktor przeladowany danymi
        /// </summary>
        /// <param name="id">Id Produktu</param>
        /// <param name="name">Nazwa Produktu</param>
        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Konstruktor przeladowany danymi
        /// </summary>
        /// <param name="id">Id Produktu</param>
        /// <param name="name">Nazwa Produktu</param>
        /// <param name="quantity">Ilosc produktu</param>
        /// <param name="netPrice">Cena Netto</param>
        /// <param name="vat">Vat Produktu</param>
        /// <param name="vatValue">Wartosc Vat</param>
        /// <param name="grossValue">Wartosc Brutto</param>
        public Product(int id, string name, string quantity, string netPrice, string vat, string vatValue, string grossValue )
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            NetPrice = netPrice;
            Vat = vat;
            VatValue = vatValue;
            GrossValue = grossValue;
        }

        /// <summary>
        /// Konstruktor przeladowany danymi
        /// </summary>
        /// <param name="name">Nazwa Produktu</param>
        /// <param name="quantity">Ilosc produktu</param>
        /// <param name="netPrice">Cena Netto</param>
        /// <param name="vat">Vat Produktu</param>
        /// <param name="vatValue">Wartosc Vat</param>
        /// <param name="grossValue">Wartosc Brutto</param>
        public Product( string name, string quantity, string netPrice, string vat, string vatValue, string grossValue)
        {
            Name = name;
            Quantity = quantity;
            NetPrice = netPrice;
            Vat = vat;
            VatValue = vatValue;
            GrossValue = grossValue;
        }

        /// <summary>
        /// Porownywanie Nazw
        /// </summary>
        /// <param name="other">Drugi produkt klasy Product</param>
        /// <returns>true w przypadku rownosci, false w przypadku braku</returns>
        public bool Equals(Product other)
        {
            if(other == null)
            {
                return false;
            }
            return (this.Name.Equals(other.Name));
        }

        /// <summary>
        /// Porownywanie tekstu
        /// </summary>
        /// <param name="other">Drugi tekst</param>
        /// <returns>true w przypadku rownosci, false w przypadku braku</returns>
        public bool Equals(string other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Name==other;
        }

        /// <summary>
        /// Nadpisana metoda Equals
        /// </summary>
        /// <param name="obj">Object produkt</param>
        /// <returns>Zwraca false jezli bedzie pusty obiekt, w przeciwnym wypadku wzraca nazwe produktu</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Product objAsProduct = obj as Product;
            if (objAsProduct == null) return false;
            else return objAsProduct.Name == this.Name;
        }

        /// <summary>
        /// Nadpisana metoda GetHashCode
        /// </summary>
        /// <returns>zwraca baze objektu</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Metoda bool, ktora wzraca czy nazwa sie zaczyna tak samo jak nazwa argumencie metody
        /// </summary>
        /// <param name="filter_param"></param>
        /// <returns>zwraca false gdy string jest pusty, w przeciwnym wypadku wzraca true jezeli tak samo sie nazywa jak argument metody</returns>
        internal bool StartsWith(string filter_param)
        {
            if (filter_param == null)
            {
                return false;
            }
            return Name.StartsWith(filter_param);
        }
    }
}