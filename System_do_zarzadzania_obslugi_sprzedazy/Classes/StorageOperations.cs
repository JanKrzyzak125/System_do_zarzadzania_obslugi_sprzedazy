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

        private int informationID;
        private string operationName;
        private int quantity;
        private string date;
        private string receiver;
        private string sender;
        private string productName;
        private string color;
        private int invoiceID;



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
            set { operationName = value; }
        }

        [DisplayName("Ilość")]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
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


        [DisplayName("Nazwa produktu")]
        public string Name
        {
            get { return productName; }
            set { productName = value;
                if (operationName.Contains("Przyjęcie"))
                {
                    Color = "Green";
                }
                if (operationName.Contains("Wydanie"))
                {
                    Color = "Red";
                }
            }
        }


        public string Color
        {
            get { return color; }
            set { color = value;}
        }

       

        public string InvoiceProducts
        {
            get {
                StringBuilder listText = new StringBuilder("");
                foreach(InvoiceProduct invoiceProduct in invoiceProducts)
                {
                    listText.Append(invoiceProduct.ToString());
                    listText.Append("\n");
                }               
                return listText.ToString(); }            
        }



        public int InvoiceID
        {
            get { return invoiceID; }
            set { invoiceID = value;
                if (invoiceID != 0)
                {
                    invoiceProducts = SQLiteDataAccess.LoadInvoicesProduct(invoiceID);
                }
                }
        }


        public StorageOperations()
        {

        }

        public StorageOperations(int informationID, string operationName, int quantity, string date, string receiver, string sender, string name)
        {
            InformationID = informationID;
            OperationName = operationName;
            Quantity = quantity;
            Date = date;
            Receiver = receiver;
            Sender = sender;
            Name = name;
            
        }
    }

}
