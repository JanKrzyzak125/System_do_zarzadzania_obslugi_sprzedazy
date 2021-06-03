namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    /// <summary>
    /// Klasa StorageProduct zawierająca informacje odnośnie Magazynu
    /// </summary>
    class StorageProduct
    {
        private int storageProductID;
        private string storageProductName;
        private int storageProductQuantity;
        private string storageProductQuantityName;
        private int storageProductNettoPrice;
        private int storageProductBruttoPrice;

        /// <summary>
        /// ID Produktu na magazynie
        /// </summary>
        public int StorageProductID
        {
            get { return storageProductID; }
            set { storageProductID = value; }
        }

        /// <summary>
        /// Nazwa produktu na magazynie
        /// </summary>
        public string StorageProductName
        {
            get { return storageProductName; }
            set { storageProductName = value; }
        }

        /// <summary>
        /// Ilość produktu na magazynie
        /// </summary>
        public int StorageProductQuantity
        {
            get { return storageProductQuantity; }
            set { storageProductQuantity = value; }
        }

        /// <summary>
        /// Nazwa jednostki produktu na magazynie
        /// </summary>
        public string StorageProductQuantityName
        {
            get { return storageProductQuantityName; }
            set { storageProductQuantityName = value; }
        }

        /// <summary>
        /// Netto cena produktu na magazynie
        /// </summary>
        public int StorageProductNettoPrice
        {
            get { return storageProductNettoPrice; }
            set { storageProductNettoPrice = value; }
        }

        /// <summary>
        /// Cena Brutto produktu na magazynie
        /// </summary>
        public int StorageProductBruttoPrice
        {
            get { return storageProductBruttoPrice; }
            set { storageProductBruttoPrice = value; }
        }

        /// <summary>
        /// Nadpisana metoda ToString
        /// </summary>
        /// <returns>Nazwę produktu i ilość produktu na magazynie wraz z jednostka</returns>
        public override string ToString()
        {
            return "Nazwa Produktu: " + storageProductName + " Ilość: " + StorageProductQuantity.ToString() + " " + StorageProductQuantityName;
        }

        /// <summary>
        /// Domyślny konstruktor klasy
        /// </summary>
        public StorageProduct()
        {

        }
    }
}
