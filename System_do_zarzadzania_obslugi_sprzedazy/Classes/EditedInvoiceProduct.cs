using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    /// <summary>
    /// Klasa EditedInvoiceProduct zawiera dane do korekt faktur
    /// </summary>
    public class EditedInvoiceProduct
    {
        private int idEditedInvoice;
        private int idEditedProduct;
        private string editedProductName;
        private int editedQuantity;
        private string editedQuantityUnit;
        private double editedNettoPrice;
        private double editedBruttoPrice;
        private int editedVat;

        /// <summary>
        /// ID faktury
        /// </summary>
        [DisplayName("ID Faktury")]
        public int IdEditedInvoice
        {
            get { return idEditedInvoice; }
            set { idEditedInvoice = value; }
        }

        /// <summary>
        /// ID produktu
        /// </summary>
        [DisplayName("ID Produktu")]
        public int IdEditedProduct
        {
            get { return idEditedProduct; }
            set { idEditedProduct = value; }
        }

        /// <summary>
        /// Nazwa produktu
        /// </summary>
        [DisplayName("Nazwa  Produktu")]
        public string EditedProductName
        {
            get { return editedProductName; }
            set { editedProductName = value; }
        }

        /// <summary>
        /// Ilość danego produktu
        /// </summary>
        [DisplayName("Ilość")]
        public int EditedQuantity
        {
            get { return editedQuantity; }
            set { editedQuantity = value; }
        }

        /// <summary>
        /// Dana jednostka
        /// </summary>
        [DisplayName("Jednostka")]
        public string EditedQuantityUnit
        {
            get { return editedQuantityUnit; }
            set { editedQuantityUnit = value; }
        }

        /// <summary>
        /// Cena Netto produktu
        /// </summary>
        [DisplayName("Cena Netto")]
        public double EditedNettoPrice
        {
            get { return editedNettoPrice; }
            set { editedNettoPrice = value; }
        }

        /// <summary>
        /// Cena Brutto produktu
        /// </summary>
        [DisplayName("Cena Brutto")]
        public double EditedBruttoPrice
        {
            get { return editedBruttoPrice; }
            set { editedBruttoPrice = value; }
        }

        /// <summary>
        /// Stawka VAT produktu
        /// </summary>
        [DisplayName("Stawka Vat")]
        public int EditedVat
        {
            get { return editedVat; }
            set { editedVat = value; }
        }

        /// <summary>
        /// Metoda, która konwertuje Invoice z nie-edytowanego na edytowalnego
        /// </summary>
        /// <param name="invoiceProduct">obiekt klasy InvoiceProduct</param>
        public void ConvertInvoiceProduct(InvoiceProduct invoiceProduct)
        {
            if(invoiceProduct.IdInvoice == 0 || invoiceProduct.IdProduct == 0)
            {
                IdEditedInvoice = 1;
                IdEditedProduct = 1;
            }
            else
            {
                IdEditedInvoice = invoiceProduct.IdInvoice;
                IdEditedProduct = invoiceProduct.IdProduct;
            }           
            EditedProductName = invoiceProduct.ProductName;
            EditedQuantity = invoiceProduct.Quantity;
            EditedQuantityUnit = invoiceProduct.QuantityUnitName;
            EditedNettoPrice = invoiceProduct.NettoPrice;
            EditedBruttoPrice = invoiceProduct.BruttoPrice;
            EditedVat = invoiceProduct.Vat;
        }

        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public EditedInvoiceProduct()
        {

        }

        /// <summary>
        /// Konstruktor przeładowany danymi
        /// </summary>
        /// <param name="idInvoice">ID faktury</param>
        /// <param name="idProduct">ID Produktu</param>
        /// <param name="productName">Nazwa produktu</param>
        /// <param name="quantity">ilość produktu</param>
        /// <param name="quantityUnits">Nazwa jednostki</param>
        /// <param name="nettoPrice">Cena netto</param>
        /// <param name="bruttoPrice">Cena brutto</param>
        /// <param name="vat">Stawka Vat</param>
        public EditedInvoiceProduct(int idInvoice, int idProduct, string productName, int quantity, string quantityUnits, double nettoPrice, double bruttoPrice, int vat)
        {
            IdEditedInvoice = idInvoice;
            IdEditedProduct = idProduct;
            EditedProductName = productName;
            EditedQuantity = quantity;
            EditedQuantityUnit = quantityUnits;
            EditedNettoPrice = nettoPrice;
            EditedBruttoPrice = bruttoPrice;
            EditedVat = vat;
        }
    }
}
