using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    public class Invoice : BaseInvoice
    {
        private int idSeller;
        private int idCompany;
        private string saleDate;
        private string paymentType;
        private string paymentDeadline;
        private string toPayInWord;
        private string toPay;
        private string remarks;
        private int idProduct;
        private int idParent;


        public int IdSeller
        {
            get { return idSeller; }
            set { idSeller = value; }
        }

        public int IdCompany
        {
            get { return idCompany; }
            set { idCompany = value; }
        }

        public string SaleDate
        {
            get { return saleDate; }
            set { saleDate = value; }
        }

        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }

        public string PaymentDeadline
        {
            get { return paymentDeadline; }
            set { paymentDeadline = value; }
        }

        public string ToPay
        {
            get { return toPay; }
            set { toPay = value; }
        }

        public string ToPayInWords
        {
            get { return toPayInWord; }
            set { toPayInWord = value; }
        }

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public int IdProduct
        {
            get { return idProduct; }
            set { idProduct = value; }
        }

        public int IdParent
        {
            get { return idParent; }
            set { idParent = value; }
        }


        public Invoice()
        {

        }

        public Invoice(int idSeller, int idCompany, int idProduct, int number, string creationDate, string saleDate, string paymentType, string paymentDeadline, string toPay,
            string toPayInWord, string paid, string remarks, int idParent)
        {
            IdSeller = idSeller;
            IdCompany = idCompany;
            IdProduct = idProduct;
            Number = number;
            CreationDate = creationDate;
            SaleDate = saleDate;
            PaymentType = paymentType;
            PaymentDeadline = paymentDeadline;
            ToPay = toPay;
            ToPayInWords = toPayInWord;
            Paid = paid;
            Remarks = remarks;
            IdParent = idParent;
        }
        public Invoice(int id, int idSeller, int idCompany, int idProduct, int number, string creationDate, string saleDate, string paymentType, string paymentDeadline, string toPay,
            string toPayInWord, string paid, string remarks, int idParent)
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
            ToPayInWords = toPayInWord;
            Paid = paid;
            Remarks = remarks;
            IdParent = idParent;
        }
    }

}