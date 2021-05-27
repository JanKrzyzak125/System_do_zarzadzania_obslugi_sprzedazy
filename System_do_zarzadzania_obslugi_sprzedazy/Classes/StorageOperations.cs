using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    class StorageOperations
    {

        private List<InvoiceProduct> invoiceProducts = new List<InvoiceProduct>();
        private StorageProduct storageProduct;

        private int informationID;
        private string operationName;
        private int quantity;
        private string date;
        private string receiver;
        private string sender;
        private string productName;
        private string color;
        private int invoiceID;
        private int operationID;



        [DisplayName("ID")]
        public int InformationID
        {
            get { return informationID; }
            set { informationID = value; }
        }

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

        

        [DisplayName("Data")]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }


        [DisplayName("Odbiorca")]
        public string Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

        [DisplayName("Nadawca")]
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }


        public string Color
        {
            get { return color; }
            set { color = value;}
        }

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

        private void UpdateList(List<InvoiceProduct> updatedList)
        {
            invoiceProducts = updatedList;
        }
        
        public StorageOperations()
        {

        }

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
