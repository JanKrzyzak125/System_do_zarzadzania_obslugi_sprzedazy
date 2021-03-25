using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System_do_zarzadzania_obslugi_sprzedazy.Classes;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    class SQLiteDataAccess
    {
        public static List<Company>LoadUsers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Company>("select * from Company", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveUser(Company company)
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Company(CompanyName,Nip,City,Street,PhoneNumber,Email) values(@companyName,@nip,@city,@street,@phoneNumber,@email)",company);
            }
        }

        public static void DeleteUsers(Company company)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                String str = "delete from Company where CompanyID=" + company.CompanyID;
                var output = cnn.Query<Company>(str);
            }
        }

        public static void DeleteInvoice(Invoice invoice)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                String str = "delete from Database_for_invoices where IdCompany=" + invoice.IdCompany;
                var output = cnn.Query<Company>(str);
            }
        }
        public static void DeleteProduct(Product product)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                String str = "delete from Product where Id=" + product.Id;
                var output = cnn.Query<Company>(str);
            }
        }

        public static void DeleteProductFromInvoice(InvoiceProduct invoiceProduct)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                String str = "delete from InvoicesProduct where IdProduct=" + invoiceProduct.IdProduct;
                var output = cnn.Query<Company>(str);
            }
        }
        public static List<Seller> LoadSellers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Seller>("select * from Seller", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<Invoice> LoadInvoices()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Invoice>("select * from Database_for_invoices", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveInvoice(Invoice invoice)
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "IdSeller,  IdCompany,  Number,  CreationDate,  SaleDate,  PaymentType,  PaymentDeadline,  ToPay, ToPayInWords,  Paid,  DateOfIssue, NameOfService, AccountNumber";
                cnn.Execute("insert into Database_for_invoices(" + s1 + ")values(@idSeller, @idCompany, @number, @creationDate, @saleDate, @paymentType, @paymentDeadline, @toPay, @toPayInWords, @paid, @dateOfIssue, @nameOfService, @accountNumber)", invoice);
            }
        }

        public static List<InvoiceProduct> LoadInvoicesProduct(int ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<InvoiceProduct>("select * from InvoicesProduct where IdInvoice=" + ID.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<Product> LoadInvoiceProduct(int CompanyID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Product>("SELECT Product.Id, Product.Name FROM CompanyWithPoroduct INNER JOIN Product ON CompanyWithPoroduct.IdProduct = Product.Id WHERE CompanyWithPoroduct.IdCompany=" + CompanyID.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveInvoiceProduct(InvoiceProduct invoiceProduct)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "IdInvoice, IdProduct, ProductName, Quantity, QuantityUnits, NettoPrice, BruttoPrice, Vat";
                cnn.Execute("insert into InvoicesProduct("+s1+ ")values(@idInvoice, @idProduct, @productName, @quantity, @quantityUnits, @nettoPrice, @bruttoPrice, @vat)", invoiceProduct);
            }
        }
        
        //Wczytuje id z autoincrementa
        public static List<int> LoadAiCompanyId(string name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<int>("SELECT seq FROM sqlite_sequence WHERE name =\""+name+"\"", new DynamicParameters());
                return output.ToList();
            }
        }
        public static List<Product> LoadProducts()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Product>("select * from Product", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveProduct(Product product)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "name,quantity,netPrice,vat,vatValue,grossValue";
                cnn.Execute("insert into Product(" + s1 + ")values(@name, @quantity, @netPrice, @vat, @vatValue, @grossValue)", product);
            }
        }

        public static List<Seller> LoadNameSeller(int idCompany) 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) 
            {                       
                var output =cnn.Query<Seller>("SELECT Seller.Name FROM CompanyWithSellers INNER JOIN Seller ON CompanyWithSellers.IdSeller=Seller.IdSeller WHERE CompanyWithSellers.IdCompany=" + idCompany.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<Company> LoadNameCompany(int idCompany) 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output=cnn.Query<Company>("select companyName from Company where CompanyID="+ idCompany.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
