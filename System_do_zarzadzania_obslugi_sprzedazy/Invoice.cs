using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    class Invoice
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int idSeller;
        public int IdSeller
        {
            get { return idSeller; }
            set { idSeller = value; }
        }
        private int idCompany;
        public int IdCompany
        {
            get { return idCompany; }
            set { idCompany = value; }
        }
        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        private string creationDate;
        public string CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }
        private string saleDate;
        public string SaleDate
        {
            get { return saleDate; }
            set { saleDate = value; }
        }
        private int paymentType;
        public int PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }
        private int paymentDeadline;
        public int PaymentDeadline
        {
            get { return paymentDeadline; }
            set { paymentDeadline = value; }
        }
        private string toPay;
        public string ToPay
        {
            get { return toPay; }
            set { toPay = value; }
        }
        private string toPayInWord;
        public string ToPayInWord
        {
            get { return toPayInWord; }
            set { toPayInWord = value; }
        }
        private string paid;
        public string Paid
        {
            get { return paid; }
            set { paid = value; }
        }
        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        private string idProduct;
        public string IdProduct
        {
            get { return idProduct; }
            set { idProduct = value; }
        }

        public Invoice()
        {

        }

        public Invoice(int id, int idSeller, int idCompany, string idProduct, int number, string creationDate, string saleDate, int paymentType, int paymentDeadline, string toPay,
            string toPayInWord, string paid, string remarks)
        {
            Id = id;
            IdSeller = idSeller;
            IdCompany = idCompany;
            IdProduct = idProduct;
            Number = number;
            CreationDate = creationDate;
            SaleDate = saleDate;
            PaymentType = paymentType;
            PaymentDeadline = paymentDeadline;
            ToPay = toPay;
            ToPayInWord = toPayInWord;
            Paid = paid;
            Remarks = remarks;
        }
    }

}