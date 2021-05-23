using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
   public class EditedInvoiceProduct
    {
        private int idEditedInvoice;
        private int idEditedProduct;
        private string editedProductName;
        private int editedQuantity;
        private string editedQuantityUnit;
        private int editedNettoPrice;
        private int editedBruttoPrice;
        private int editedVat;

        [DisplayName("ID Faktury")]
        public int IdEditedInvoice
        {
            get { return idEditedInvoice; }
            set { idEditedInvoice = value; }
        }

        [DisplayName("ID Produktu")]
        public int IdEditedProduct
        {
            get { return idEditedProduct; }
            set { idEditedProduct = value; }
        }

        [DisplayName("Nazwa  Produktu")]
        public string EditedProductName
        {
            get { return editedProductName; }
            set { editedProductName = value; }
        }

        [DisplayName("Ilość")]
        public int EditedQuantity
        {
            get { return editedQuantity; }
            set { editedQuantity = value; }
        }

        [DisplayName("Jednostka")]
        public string EditedQuantityUnit
        {
            get { return editedQuantityUnit; }
            set { editedQuantityUnit = value; }
        }

        [DisplayName("Cena Netto")]
        public int EditedNettoPrice
        {
            get { return editedNettoPrice; }
            set { editedNettoPrice = value; }
        }

        [DisplayName("Cena Brutto")]
        public int EditedBruttoPrice
        {
            get { return editedBruttoPrice; }
            set { editedBruttoPrice = value; }
        }

        [DisplayName("Stawka Vat")]
        public int EditedVat
        {
            get { return editedVat; }
            set { editedVat = value; }
        }

        public void ConvertInvoiceProduct(InvoiceProduct invoiceProduct)
        {
            if(invoiceProduct.IdInvoice == 0 || invoiceProduct.IdProduct == 0)
            {
                IdEditedInvoice = 1;
                IdEditedProduct = 1;
            }
            else
            {
                IdEditedInvoice = invoiceProduct.IdInvoice;
                IdEditedProduct = invoiceProduct.IdProduct;
            }           
            EditedProductName = invoiceProduct.ProductName;
            EditedQuantity = invoiceProduct.Quantity;
            EditedQuantityUnit = invoiceProduct.QuantityUnitName;
            EditedNettoPrice = invoiceProduct.NettoPrice;
            EditedBruttoPrice = invoiceProduct.BruttoPrice;
            EditedVat = invoiceProduct.Vat;
        }


        public EditedInvoiceProduct()
        {

        }


        public EditedInvoiceProduct(int idInvoice, int idProduct, string productName, int quantity, string quantityUnits, int nettoPrice, int bruttoPrice, int vat)
        {
            IdEditedInvoice = idInvoice;
            IdEditedProduct = idProduct;
            EditedProductName = productName;
            EditedQuantity = quantity;
            EditedQuantityUnit = quantityUnits;
            EditedNettoPrice = nettoPrice;
            EditedBruttoPrice = bruttoPrice;
            EditedVat = vat;
        }

    }
}
