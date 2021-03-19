﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    public class InvoiceProduct
    {
        private int idInvoice;
        private int idProduct;
        private string productName;
        private int quantity;
        private string quantityUnits;
        private int nettoPrice;
        private int bruttoPrice;
        private int vat;

        [DisplayName("ID Faktury")]
        public int IdInvoice
        {
            get { return idInvoice; }
            set { idInvoice = value; }
        }

        [DisplayName("ID Produktu")]
        public int IdProduct
        {
            get { return idProduct; }
            set { idProduct = value; }
        }

        [DisplayName("Nazwa  Produktu")]
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        [DisplayName("Ilość")]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [DisplayName("Jednostka")]
        public string QuantityUnits
        {
            get { return quantityUnits; }
            set { quantityUnits = value; }
        }

        [DisplayName("Cena Netto")]
        public int NettoPrice
        {
            get { return nettoPrice; }
            set { nettoPrice = value; }
        }

        [DisplayName("Cena Brutto")]
        public int BruttoPrice
        {
            get { return bruttoPrice; }
            set { bruttoPrice = value; }
        }

        [DisplayName("Stawka Vat")]
        public int Vat
        {
            get { return vat; }
            set { vat = value; }
        }

        public InvoiceProduct()
        {

        }


        public InvoiceProduct(int idInvoice, int idProduct, string productName, int quantity, string quantityUnits, int nettoPrice, int bruttoPrice, int vat)
        {
            IdInvoice = idInvoice;
            IdProduct = idProduct;
            ProductName = productName;
            Quantity = quantity;
            QuantityUnits = quantityUnits;
            NettoPrice = nettoPrice;
            BruttoPrice = bruttoPrice;
            Vat = vat;
        }
    }
}
