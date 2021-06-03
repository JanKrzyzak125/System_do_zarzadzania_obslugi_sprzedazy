using System;
using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Klasa Product, która zawiera informacje odnoœnie produktu
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
        /// Iloœæ produktu
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
        /// Wartoœæ Vat
        /// </summary>
        [DisplayName("Wartosc Vat")]
        public string VatValue
        {
            get { return vatValue; }
            set { vatValue = value; }
        }

        /// <summary>
        /// Wartoœæ Brutto 
        /// </summary>
        [DisplayName("Wartosc Brutto")]
        public string GrossValue
        {
            get { return grossValue; }
            set { grossValue = value; }
        }

        /// <summary>
        /// Konstruktor domyœlny
        /// </summary>
        public Product()
        {

        }

        /// <summary>
        /// Konstruktor prze³adowany danymi
        /// </summary>
        /// <param name="id">Id Produktu</param>
        /// <param name="name">Nazwa Produktu</param>
        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Konstruktor prze³adowany danymi
        /// </summary>
        /// <param name="id">Id Produktu</param>
        /// <param name="name">Nazwa Produktu</param>
        /// <param name="quantity">Iloœæ produktu</param>
        /// <param name="netPrice">Cena Netto</param>
        /// <param name="vat">Vat Produktu</param>
        /// <param name="vatValue">Wartoœæ Vat</param>
        /// <param name="grossValue">Wartoœæ Brutto</param>
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
        /// Konstruktor prze³adowany danymi
        /// </summary>
        /// <param name="name">Nazwa Produktu</param>
        /// <param name="quantity">Iloœæ produktu</param>
        /// <param name="netPrice">Cena Netto</param>
        /// <param name="vat">Vat Produktu</param>
        /// <param name="vatValue">Wartoœæ Vat</param>
        /// <param name="grossValue">Wartoœæ Brutto</param>
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
        /// Porównywanie Nazw
        /// </summary>
        /// <param name="other">Drugi produkt klasy Product</param>
        /// <returns>true w przypadku równoœci, false w przypadku braku</returns>
        public bool Equals(Product other)
        {
            if(other == null)
            {
                return false;
            }
            return (this.Name.Equals(other.Name));
        }

        /// <summary>
        /// Porównywanie tekstu
        /// </summary>
        /// <param name="other">Drugi tekst</param>
        /// <returns>true w przypadku równoœci, false w przypadku braku</returns>
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
        /// <returns>Zwraca false jeœli bêdzie pusty obiekt, w przeciwnym wypadku wzraca nazwê produktu</returns>
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
        /// <returns>zwraca bazê objektu</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Metoda bool, która wzraca czy nazwa siê zaczyna tak samo jak nazwa argumencie metody
        /// </summary>
        /// <param name="filter_param"></param>
        /// <returns>zwraca false gdy string jest pusty, w przeciwnym wypadku wzraca true jeœli tak samo siê nazywa jak argument metody</returns>
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