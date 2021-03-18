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
        private string saleDate;
        public string SaleDate
        {
            get { return saleDate; }
            set { saleDate = value; }
        }
        private string paymentType;
        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }
        private string paymentDeadline;
        public string PaymentDeadline
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
        public string ToPayInWords
        {
            get { return toPayInWord; }
            set { toPayInWord = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        private int idProduct;
        public int IdProduct
        {
            get { return idProduct; }
            set { idProduct = value; }
        }

        public Invoice()
        {

        }

        public Invoice(int idSeller, int idCompany, int idProduct, int number, string creationDate, string saleDate, string paymentType, string paymentDeadline, string toPay,
            string toPayInWord, string paid, string remarks)
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
        }
        public Invoice(int id, int idSeller, int idCompany, int idProduct, int number, string creationDate, string saleDate, string paymentType, string paymentDeadline, string toPay,
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
            ToPayInWords = toPayInWord;
            Paid = paid;
            Remarks = remarks;
        }
    }

}