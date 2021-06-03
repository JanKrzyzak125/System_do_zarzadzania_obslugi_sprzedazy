using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System_do_zarzadzania_obslugi_sprzedazy.Classes;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Klasa SQLiteDataAccess zawiera metody do łączenia sie z bazą danych
    /// </summary>
    class SQLiteDataAccess
    {
        /// <summary>
        /// Metoda, która wczytuje użytkowników z bazy danych
        /// </summary>
        /// <returns>Listę użytkowników</returns>
        public static List<Company>LoadUsers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Company>("select * from Company", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która zapisuje użytkówników do bazy danych
        /// </summary>
        /// <param name="company">Obiekt klasy Company</param>
        public static void SaveUser(Company company)
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Company(CompanyName,Nip,City,Street,PhoneNumber,Email) values(@companyName,@nip,@city,@street,@phoneNumber,@email)",company);
            }
        }

        /// <summary>
        /// Metoda, która usuwa użytkownika z bazy danych 
        /// </summary>
        /// <param name="company">Obiekt klasy Company</param>
        public static void DeleteUsers(Company company)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                String str = "delete from Company where CompanyID=" + company.CompanyID;
                var output = cnn.Query<Company>(str);
            }
        }

        /// <summary>
        /// Metoda, która usuwa fakturę z bazy danych 
        /// </summary>
        /// <param name="invoice">Obiekt klasy Invoice</param>
        public static void DeleteInvoice(Invoice invoice)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                String str = "delete from Database_for_invoices where IdCompany=" + invoice.IdCompany;
                var output = cnn.Query<Company>(str);
            }
        }

        /// <summary>
        /// Metoda, która usuwa produkt z bazy danych 
        /// </summary>
        /// <param name="product">Obiekt klasy Product</param>
        public static void DeleteProduct(Product product)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                String str = "delete from Product where Id=" + product.Id;
                var output = cnn.Query<Company>(str);
            }
        }

        /// <summary>
        /// Metoda, która usuwa produkt z danej faktury z bazy danych 
        /// </summary>
        /// <param name="invoiceProduct">Obiekt klasy InvoiceProduct</param>
        public static void DeleteProductFromInvoice(InvoiceProduct invoiceProduct)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                String str = "delete from InvoicesProduct where IdProduct=" + invoiceProduct.IdProduct;
                var output = cnn.Query<Company>(str);
            }
        }

        /// <summary>
        /// Metoda, która wczytuje Kontrahentów z bazy danych
        /// </summary>
        /// <returns>listę z obiektami Seller</returns>
        public static List<Seller> LoadSellers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Seller>("select * from Seller", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która usuwa Kontrahenta z bazy danych
        /// </summary>
        /// <param name="seller">Obiekt klasy Seller</param>
        public static void DeleteContractors(Seller seller)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                String str = "delete from Seller where IdSeller=" + seller.IdSeller;
                var output = cnn.Query<Seller>(str);
            }
        }

        /// <summary>
        /// Metoda, która zapisuje kontrahenta do bazy danych
        /// </summary>
        /// <param name="seller">Obiekt klasy Seller</param>
        public static void SaveSeller(Seller seller)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "Name,Surname,City,Street,NumberPhone,Nip";
                cnn.Execute("insert into Seller(" + s1 + ")values(@Name,@Surname,@City,@Street,@NumberPhone,@Nip)", seller);
            }
        }

        /// <summary>
        /// Metoda, która wczytuje faktury z bazy danych
        /// </summary>
        /// <returns>Lista obiektów klasy Invoice</returns>
        public static List<Invoice> LoadInvoices()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Invoice>("select * from Database_for_invoices", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która wczytuje fakturę z bazy danych
        /// </summary>
        /// <param name="ID">ID faktury</param>
        /// <returns>Zwraca listę obiektów klasy Invoice</returns>
        public static List<Invoice> LoadInvoice(int ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Invoice>("select * from Database_for_invoices WHERE Database_for_invoices.Id=" +  ID.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która zapisuje fakturę do bazy danych
        /// </summary>
        /// <param name="invoice">Id faktury</param>
        public static void SaveInvoice(Invoice invoice)
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "IdSeller,  IdCompany,  Number,  CreationDate,  SaleDate,  PaymentType,  PaymentDeadline,  ToPay, ToPayInWords,  Paid,  DateOfIssue, NameOfService, AccountNumber, IsPrinted";
                cnn.Execute("insert into Database_for_invoices(" + s1 + ")values(@idSeller, @idCompany, @number, @creationDate, @saleDate, @paymentType, @paymentDeadline, @toPay, @toPayInWords, @paid, @dateOfIssue, @nameOfService, @accountNumber, @isPrinted)", invoice);
            }
        }

        /// <summary>
        /// Metoda, która wczytuje produkty z faktur z bazy danych 
        /// </summary>
        /// <param name="ID">ID faktury</param>
        /// <returns>Listę obiektów InvoiceProduct</returns>
        public static List<InvoiceProduct> LoadInvoicesProduct(int ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                //var DynamicParameter = new DynamicParameters();
                //DynamicParameter.Add("QuantityUnit")
                string str = "SELECT InvoicesProduct.IdInvoice,InvoicesProduct.IdProduct,InvoicesProduct.ProductName,InvoicesProduct.Quantity,QuantityUnit.QuantityUnitName,InvoicesProduct.NettoPrice,InvoicesProduct.BruttoPrice,InvoicesProduct.Vat FROM InvoicesProduct INNER JOIN QuantityUnit ON InvoicesProduct.QuantityUnits=QuantityUnit.QuantityUnitID WHERE IdInvoice=";
                var output = cnn.Query<InvoiceProduct>(str + ID.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która wczytuje produkty z faktury z bazy danych 
        /// </summary>
        /// <param name="CompanyID">ID Kontrahenta</param>
        /// <returns>Listę obiektów InvoiceProduct</returns>
        public static List<Product> LoadInvoiceProduct(int CompanyID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Product>("SELECT Product.Id, Product.Name,Product.NetPrice,Product.VatPrice FROM CompanyWithPoroduct INNER JOIN Product ON CompanyWithPoroduct.IdProduct = Product.Id WHERE CompanyWithPoroduct.IdCompany=" + CompanyID.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która zapisuje produkty do faktury do bazy danych 
        /// </summary>
        /// <param name="invoiceProduct"> Obiekt klasy InvoiceProduct</param>
        /// <param name="unitId">ID jednostki</param>
        public static void SaveInvoiceProduct(InvoiceProduct invoiceProduct, int unitId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "IdInvoice, IdProduct, ProductName, Quantity, QuantityUnits, NettoPrice, BruttoPrice, Vat";
                cnn.Execute("insert into InvoicesProduct("+s1+ ")values(@idInvoice, @idProduct, @productName, @quantity,"+unitId.ToString() +", @nettoPrice, @bruttoPrice, @vat)", invoiceProduct);
            }
        }

        /// <summary>
        /// Wczytuje id z autoincrementa
        /// </summary>
        /// <param name="name">Nazwa</param>
        /// <returns>Zwraca ID</returns>
        public static List<int> LoadAiCompanyId(string name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<int>("SELECT seq FROM sqlite_sequence WHERE name =\""+name+"\"", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która wczytuje z bazy danych produkty
        /// </summary>
        /// <param name="CompanyID">ID Kontrahenta</param>
        /// <returns>Zwraca listę Produktów</returns>
        public static List<Product> LoadProducts(int CompanyID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Product>("select Product.Id, Product.Name,Product.Quantity,Product.NetPrice,Product.Vat,Product.VatValue,Product.GrossValue FROM CompanyWithPoroduct INNER JOIN Product ON CompanyWithPoroduct.IdProduct = Product.Id WHERE CompanyWithPoroduct.IdCompany=" + CompanyID.ToString(), new DynamicParameters());
                return output.ToList();
                
            }
        }

        /// <summary>
        /// Metoda, która zapisuje produkty do bazy danych 
        /// </summary>
        /// <param name="product">Obiekt klasy Produkt</param>
        public static void SaveProduct(Product product)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "name,quantity,netPrice,vat,vatValue,grossValue";
                cnn.Execute("insert into Product(" + s1 + ")values(@name, @quantity, @netPrice, @vat, @vatValue, @grossValue)", product);
            }
        }

        /// <summary>
        /// Metoda, która ładuje Nazwy kontrahentów z bazy danych
        /// </summary>
        /// <param name="idCompany">Id kontrahenta</param>
        /// <returns>Lista z obiektami klasy Seller</returns>
        public static List<Seller> LoadNameSeller(int idCompany) 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) 
            {                       
                var output =cnn.Query<Seller>("SELECT Seller.Name, Seller.Surname, Seller.Street, Seller.City, Seller.NumberPhone, Seller.Nip, Seller.Regon FROM CompanyWithSellers INNER JOIN Seller ON CompanyWithSellers.IdSeller=Seller.IdSeller WHERE CompanyWithSellers.IdCompany=" + idCompany.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która ładuje Nazwy firm z bazy danych
        /// </summary>
        /// <param name="idCompany">ID firmy</param>
        /// <returns>Listę obiektów klasy Company</returns>
        public static List<Company> LoadNameCompany(int idCompany) 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output=cnn.Query<Company>("select * from Company where CompanyID="+ idCompany.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która ładuje Nazwy jednostek z bazy danych
        /// </summary>
        /// <returns>List stringów</returns>
        public static List<string> LoadQuantityUnitName() 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) 
            {
                var output =cnn.Query<string>("select QuantityUnitName FROM QuantityUnit", new DynamicParameters());
                return output.ToList();
            }

        }

        /// <summary>
        /// Metoda, która zapisuje Produkt do Firmy do bazy danych
        /// </summary>
        /// <param name="productId">ID Produkt</param>
        /// <param name="companyId">Id Firmy</param>
        public static void SaveProductToCustomer(int productId, int companyId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "IdCompany, IdProduct";
                cnn.Execute("insert into CompanyWithPoroduct(" + s1 + ")values("+companyId.ToString()+","+productId.ToString()+")");
            }
        }

        /// <summary>
        /// Metoda, która zapisuje nazwy jednostek do bazy danych
        /// </summary>
        /// <param name="unitId">ID Jednostki</param>
        /// <param name="unitName">Nazwa jednostki</param>
        public static void SaveUnitName(int unitId, string unitName)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "QuantityUnitID, QuantityUnitName";
                cnn.Execute("insert into QuantityUnit(" + s1 + ")values(" + unitId.ToString() + "," + "\'" + unitName + "\'" + ")");
            }
        }

        /// <summary>
        /// Metoda, która wczytuje magazynowe operacje z bazy danych
        /// </summary>
        /// <returns>lista obiektów klasy StorageOperations</returns>
        public static List<StorageOperations> LoadOperations()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<StorageOperations>("SELECT StorageInformation.InformationID,StorageOperations.OperationName,StorageInformation.Quantity,StorageInformation.Date,StorageInformation.Receiver,StorageInformation.Sender,StorageInformation.InvoiceID,StorageInformation.StorageProductID FROM((StorageInformation INNER JOIN StorageOperations ON StorageInformation.OperationID=StorageOperations.OperationID))", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która wczytuje magazynowe produkty z bazy danych
        /// </summary>
        /// <param name="ID">ID </param>
        /// <returns>Listę obiektów klasy StorageProduct</returns>
        public static List<StorageProduct> LoadStorageProduct(int ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<StorageProduct>("SELECT * FROM StorageProduct WHERE StorageProduct.StorageProductID="+ID.ToString()+"", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która zapisuje operacje do bazy danych
        /// </summary>
        /// <param name="storageOperation">Obiekt klasy StorageOperation</param>
        /// <param name="operationID">ID operacji</param>
        /// <param name="invoiceID">ID faktury</param>
        public static void SaveOperation(StorageOperations storageOperation, int operationID, int invoiceID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "OperationID, Date, Receiver, Sender, InvoiceID";
                cnn.Execute("insert into StorageInformation(" + s1 + ")values(" + operationID.ToString() + "," + "\'" + storageOperation.Date + "\'" + "," + "\'" + storageOperation.Receiver + "\'" + "," + "\'" + storageOperation.Sender + "\'" + "," + invoiceID.ToString() + ")");
            }
        }

        /// <summary>
        /// Metoda, która zapisuje operacje do bazy danych
        /// </summary>
        /// <param name="product">obiekt klasy Product</param>
        /// <param name="operationID">ID operacji</param>
        /// <param name="StorageProductID">ID magazynowego produktu</param>
        public static void SaveOperation(Product product, int operationID, int StorageProductID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "OperationID, Date, Receiver, Sender, StorageProductID";
                cnn.Execute("insert into StorageInformation(" + s1 + ")values(" + operationID.ToString() + "," + "\'" + DateTime.Now.ToString("d") + "\'" + "," + "\'" + "Operacja wewnetrzna" + "\'" + "," + "\'"+ "Operacja wewnetrzna" + "\'" + "," + StorageProductID.ToString() + ")");
            }
        }

        /// <summary>
        /// metoda, która zapisuje informacje o produkcie do bazy danych
        /// </summary>
        /// <param name="product">Obiekt klasy Product</param>
        /// <param name="quantity">Ilość</param>
        public static void SaveProductInformation(Product product, int quantity)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "StorageProductName, StorageProductQuantity, StorageProductNettoPrice, StorageProductBruttoPrice";
                cnn.Execute("insert into StorageProduct(" + s1 + ")values(" + "\'" + product.Name + "\'" + ","  + quantity.ToString() + "," +  Int32.Parse(product.NetPrice) + "," + Int32.Parse(product.GrossValue)+ ")");
            }
        }

        /// <summary>
        /// Metoda, która aktualizuje ilość produktu
        /// </summary>
        /// <param name="product">Obiekt klasy Product</param>
        public static void UpdateProductQuantity(Product product)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("UPDATE Product SET Quantity = "+product.Quantity+" WHERE ID = "+product.Id+"");
            }
        }
        /// <summary>
        /// Metoda, która aktualizuje ilość produktu
        /// </summary>
        /// <param name="quantity">ilość</param>
        /// <param name="id">ID</param>
        public static void UpdateProductQuantity(int quantity, int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("UPDATE Product SET Quantity = " + quantity.ToString() + " WHERE ID = " + id.ToString() + "");
            }
        }

        /// <summary>
        /// Metoda, która wczytuje korektę faktur z bazy danych
        /// </summary>
        /// <returns>lista obiektów klasy InoviceCorrection</returns>
        public static List<InvoiceCorrection> LoadCorrection()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<InvoiceCorrection>("SELECT * FROM CorrectedPdf", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda, która zapisuje korektę korekty faktur z bazy danych
        /// </summary>
        /// <param name="invoiceCorrection">Obiekt klasy InvoiceCorrection</param>
        public static void SaveCorrectedInvoice(InvoiceCorrection invoiceCorrection)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "CorrectionID, CorrectionNumber, CorrectionDate, CorrectionReason, InvoiceConnection, CorrectionConnection";
                cnn.Execute("insert into CorrectedPdf(" + s1 + ")values(@correctionID,@correctionNumber,@correctionDate,@correctionReason,@invoiceConnection,@correctionConnection)", invoiceCorrection);
            }
        }

        /// <summary>
        /// Metoda, która zapisuje korektę faktur do bazy danych 
        /// </summary>
        /// <param name="editedInvoiceProduct">obiekt klasy EditedInvoiceProduct</param>
        public static void SaveCorrectedInvoiceProduct(EditedInvoiceProduct editedInvoiceProduct)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string s1 = "IdEditedInvoice, IdEditedProduct, EditedProductName, EditedQuantity, EditedQuantityUnit, EditedNettoPrice, EditedBruttoPrice, EditedVat";
                cnn.Execute("insert into EditedInvoiceProduct(" + s1 + ")values(@idEditedInvoice,@idEditedProduct,@editedProductName,@editedQuantity,@editedQuantityUnit,@editedNettoPrice,@editedBruttoPrice,@editedVat)", editedInvoiceProduct);
            }
        }

        /// <summary>
        /// Metoda, która wczytuje korekty faktur z bazy danych
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>Lista obiektów klasy EditedInvoiceProduct</returns>
        public static List<EditedInvoiceProduct> LoadEditedInvoicesProduct(int ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                //var DynamicParameter = new DynamicParameters();
                //DynamicParameter.Add("QuantityUnit")
                string str = "SELECT *  FROM EditedInvoiceProduct WHERE IdEditedInvoice=";
                var output = cnn.Query<EditedInvoiceProduct>(str + ID.ToString(), new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Metoda do połączenia programu z baza danych
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>ConfigurationManager.ConnectionStrings[id].ConnectionString</returns>
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}