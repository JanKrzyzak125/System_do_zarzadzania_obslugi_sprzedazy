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
        private int informationID;
        private string operationName;
        private int quantity;
        private string date;
        private string receiver;
        private string sender;


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

        public StorageOperations()
        {

        }

        public StorageOperations(int informationID, string operationName, int quantity, string date, string receiver, string sender)
        {
            InformationID = informationID;
            OperationName = operationName;
            Quantity = quantity;
            Date = date;
            Receiver = receiver;
            Sender = sender;
        }
    }

}
