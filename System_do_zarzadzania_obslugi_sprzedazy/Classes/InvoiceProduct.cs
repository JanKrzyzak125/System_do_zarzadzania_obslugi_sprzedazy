using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    /// <summary>
    /// Klasa InvoiceProduct, która zawiera informacje o produktach na fakturze 
    /// </summary>
    public class InvoiceProduct
    {
        private int idInvoice;
        private int idProduct;
        private string productName;
        private int quantity;
        private string quantityUnits;
        private double nettoPrice;
        private double bruttoPrice;
        private int vat;

        /// <summary>
        /// ID Faktury
        /// </summary>
        [DisplayName("ID Faktury")]
        public int IdInvoice
        {
            get { return idInvoice; }
            set { idInvoice = value; }
        }

        /// <summary>
        /// ID produktu
        /// </summary>
        [DisplayName("ID Produktu")]
        public int IdProduct
        {
            get { return idProduct; }
            set { idProduct = value; }
        }

        /// <summary>
        /// Nazwa Produktu
        /// </summary>
        [DisplayName("Nazwa  Produktu")]
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        /// <summary>
        /// Ilość produktu
        /// </summary>
        [DisplayName("Ilość")]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /// <summary>
        /// Jednostka produktu
        /// </summary>
        [DisplayName("Jednostka")]
        public string QuantityUnitName
        {
            get { return quantityUnits; }
            set { quantityUnits = value; }
        }

        /// <summary>
        /// Cena Netto produktu
        /// </summary>
        [DisplayName("Cena Netto")]
        public double NettoPrice
        {
            get { return nettoPrice; }
            set { nettoPrice = value; }
        }

        /// <summary>
        /// Cena Brutto produktu
        /// </summary>
        [DisplayName("Cena Brutto")]
        public double BruttoPrice
        {
            get { return bruttoPrice; }
            set { bruttoPrice = value; }
        }

        /// <summary>
        /// Stawka Vat produktu
        /// </summary>
        [DisplayName("Stawka Vat")]
        public int Vat
        {
            get { return vat; }
            set { vat = value; }
        }

        /// <summary>
        /// Nadpisana metoda ToString
        /// </summary>
        /// <returns>zwraca nazwę produktu z ilością na fakturze</returns>
        public override string ToString()
        {
            return "Nazwa Produktu: " + productName + " Ilość: " + Quantity.ToString(); 
        }

        /// <summary>
        /// Konstruktor domyślny klasy
        /// </summary>
        public InvoiceProduct()
        {

        }

        /// <summary>
        /// Konstruktor przeładowany danymi
        /// </summary>
        /// <param name="idInvoice">Id Faktury</param>
        /// <param name="idProduct">Id Produktu</param>
        /// <param name="productName">Nazwa Produktu</param>
        /// <param name="quantity">Ilość produktu</param>
        /// <param name="quantityUnits">Nazwa jednostki</param>
        /// <param name="nettoPrice">Netto cena</param>
        /// <param name="bruttoPrice">Brutto cena</param>
        /// <param name="vat">Stawka Vat</param>
        public InvoiceProduct(int idInvoice, int idProduct, string productName, int quantity, string quantityUnits, double nettoPrice, double bruttoPrice, int vat)
        {
            IdInvoice = idInvoice;
            IdProduct = idProduct;
            ProductName = productName;
            Quantity = quantity;
            QuantityUnitName = quantityUnits;
            NettoPrice = nettoPrice;
            BruttoPrice = bruttoPrice;
            Vat = vat;
        }
    }
}
