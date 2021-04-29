using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{

    class StorageProduct
    {

        private int storageProductID;
        private string storageProductName;
        private int storageProductQuantity;
        private string storageProductQuantityName;
        private int storageProductNettoPrice;
        private int storageProductBruttoPrice;

        public int StorageProductID
        {
            get { return storageProductID; }
            set { storageProductID = value; }
        }

        public string StorageProductName
        {
            get { return storageProductName; }
            set { storageProductName = value; }
        }

        public int StorageProductQuantity
        {
            get { return storageProductQuantity; }
            set { storageProductQuantity = value; }
        }

        public string StorageProductQuantityName
        {
            get { return storageProductQuantityName; }
            set { storageProductQuantityName = value; }
        }

        public int StorageProductNettoPrice
        {
            get { return storageProductNettoPrice; }
            set { storageProductNettoPrice = value; }
        }

        public int StorageProductBruttoPrice
        {
            get { return storageProductBruttoPrice; }
            set { storageProductBruttoPrice = value; }
        }


        public override string ToString()
        {
            return "Nazwa Produktu: " + storageProductName + " Ilość: " + StorageProductQuantity.ToString() + " " + StorageProductQuantityName;
        }

        public StorageProduct()
        {

        }

    }
}
