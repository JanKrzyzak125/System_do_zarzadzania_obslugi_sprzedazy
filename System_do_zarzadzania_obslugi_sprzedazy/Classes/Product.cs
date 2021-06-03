using System;
using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Klasa Product, kt�ra zawiera informacje odno�nie produktu
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
        /// Ilo�� produktu
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
        /// Warto�� Vat
        /// </summary>
        [DisplayName("Wartosc Vat")]
        public string VatValue
        {
            get { return vatValue; }
            set { vatValue = value; }
        }

        /// <summary>
        /// Warto�� Brutto 
        /// </summary>
        [DisplayName("Wartosc Brutto")]
        public string GrossValue
        {
            get { return grossValue; }
            set { grossValue = value; }
        }

        /// <summary>
        /// Konstruktor domy�lny
        /// </summary>
        public Product()
        {

        }

        /// <summary>
        /// Konstruktor prze�adowany danymi
        /// </summary>
        /// <param name="id">Id Produktu</param>
        /// <param name="name">Nazwa Produktu</param>
        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Konstruktor prze�adowany danymi
        /// </summary>
        /// <param name="id">Id Produktu</param>
        /// <param name="name">Nazwa Produktu</param>
        /// <param name="quantity">Ilo�� produktu</param>
        /// <param name="netPrice">Cena Netto</param>
        /// <param name="vat">Vat Produktu</param>
        /// <param name="vatValue">Warto�� Vat</param>
        /// <param name="grossValue">Warto�� Brutto</param>
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
        /// Konstruktor prze�adowany danymi
        /// </summary>
        /// <param name="name">Nazwa Produktu</param>
        /// <param name="quantity">Ilo�� produktu</param>
        /// <param name="netPrice">Cena Netto</param>
        /// <param name="vat">Vat Produktu</param>
        /// <param name="vatValue">Warto�� Vat</param>
        /// <param name="grossValue">Warto�� Brutto</param>
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
        /// Por�wnywanie Nazw
        /// </summary>
        /// <param name="other">Drugi produkt klasy Product</param>
        /// <returns>true w przypadku r�wno�ci, false w przypadku braku</returns>
        public bool Equals(Product other)
        {
            if(other == null)
            {
                return false;
            }
            return (this.Name.Equals(other.Name));
        }

        /// <summary>
        /// Por�wnywanie tekstu
        /// </summary>
        /// <param name="other">Drugi tekst</param>
        /// <returns>true w przypadku r�wno�ci, false w przypadku braku</returns>
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
        /// <returns>Zwraca false je�li b�dzie pusty obiekt, w przeciwnym wypadku wzraca nazw� produktu</returns>
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
        /// <returns>zwraca baz� objektu</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Metoda bool, kt�ra wzraca czy nazwa si� zaczyna tak samo jak nazwa argumencie metody
        /// </summary>
        /// <param name="filter_param"></param>
        /// <returns>zwraca false gdy string jest pusty, w przeciwnym wypadku wzraca true je�li tak samo si� nazywa jak argument metody</returns>
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