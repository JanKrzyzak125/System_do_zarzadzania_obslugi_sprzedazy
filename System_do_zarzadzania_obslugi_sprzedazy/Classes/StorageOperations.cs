using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    /// <summary>
    /// Klasa StorageOperations, która zawiera informacje dotyczące operacji na magazynach
    /// </summary>
    class StorageOperations
    {
        private List<InvoiceProduct> invoiceProducts = new List<InvoiceProduct>();
        private StorageProduct storageProduct;

        private int informationID;
        private string operationName;
        private string date;
        private string receiver;
        private string sender;
        private string color;
        private int invoiceID;
        private int operationID;


        /// <summary>
        /// ID Informacji
        /// </summary>
        [DisplayName("ID")]
        public int InformationID
        {
            get { return informationID; }
            set { informationID = value; }
        }

        /// <summary>
        /// Nazwa Operacji
        /// </summary>
        [DisplayName("Nazwa operacji")]
        public string  OperationName
        {
            get { return operationName; }
            set { operationName = value;
                if (operationName.Contains("Przyjęcie"))
                {
                    Color = "Green";
                }
                if (operationName.Contains("Wydanie"))
                {
                    Color = "Orange";
                }
            }
        }

        /// <summary>
        /// Data Operacji 
        /// </summary>
        [DisplayName("Data")]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// Odbiorca operacji 
        /// </summary>
        [DisplayName("Odbiorca")]
        public string Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

        /// <summary>
        /// Nadawca operacji
        /// </summary>
        [DisplayName("Nadawca")]
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        /// <summary>
        /// Kolor operacji
        /// </summary>
        public string Color
        {
            get { return color; }
            set { color = value;}
        }

        /// <summary>
        /// ID Faktury
        /// </summary>
        public int InvoiceID
        {
            get { return invoiceID; }
            set
            {
                invoiceID = value;
                if (invoiceID != 0)
                {
                    invoiceProducts = SQLiteDataAccess.LoadInvoicesProduct(invoiceID);
                }
            }
        }

        /// <summary>
        /// ID produktu na magazynie
        /// </summary>
        public int StorageProductID
        {
            get { return operationID; }
            set { operationID = value;
                if (operationID != 0)
                {
                    storageProduct = SQLiteDataAccess.LoadStorageProduct(operationID)[0];
                }
            }
        }

        /// <summary>
        /// Lista produktów na fakturze
        /// </summary>
        public string InvoiceProducts
        {
            get {
                StringBuilder listText = new StringBuilder("");
                if(InvoiceID!=0)
                {
                    foreach (InvoiceProduct invoiceProduct in invoiceProducts)
                    {
                        listText.Append(invoiceProduct.ToString());
                        listText.Append("\n");
                    }
                }
                else
                {
                    listText.Append(storageProduct.ToString());
                }
                return listText.ToString();
            }            
        }

        /// <summary>
        /// Aktualizacja listy faktury
        /// </summary>
        /// <param name="updatedList">Lista produktów faktury</param>
        private void UpdateList(List<InvoiceProduct> updatedList)
        {
            invoiceProducts = updatedList;
        }
        
        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public StorageOperations()
        {

        }

        /// <summary>
        /// Konstruktor przeładowany danymi
        /// </summary>
        /// <param name="informationID">ID informacji</param>
        /// <param name="operationName">Nazwa Operacji</param>
        /// <param name="date">Data Operacji</param>
        /// <param name="receiver">Odbiorca</param>
        /// <param name="sender">Nadawca</param>
        public StorageOperations(int informationID, string operationName, string date, string receiver, string sender)
        {
            InformationID = informationID;
            OperationName = operationName;         
            Date = date;
            Receiver = receiver;
            Sender = sender;
        }
    }
}
