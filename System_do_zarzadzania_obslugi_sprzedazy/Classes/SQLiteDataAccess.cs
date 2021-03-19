using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

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
                cnn.Execute("insert into Database_for_invoices(IdSeller, IdCompany, Number, CreationDate, SaleDate, PaymentType, PaymentDeadline, ToPay, ToPayInWords, Paid, dateOfIssue) values(@idSeller, @idCompany, @number, @creationDate, @saleDate, @paymentType, @paymentDeadline, @toPay, @toPayInWord, @paid, @dateOfIssue)", invoice);
            }
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
