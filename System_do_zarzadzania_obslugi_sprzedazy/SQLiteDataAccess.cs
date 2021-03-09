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
        public static List<Firma>LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Firma>("select * from Firma", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveUser(Firma firma)
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Firma(Nazwa_Firmy,Nip,Miasto,Ulica,Numer_Telefonu,Email) values(@Nazwa_Firmy,@Nip,@Miasto,@Ulica,@Numer_Telefonu,@Email)",firma);
            }
        }

        public static void DeletePeople(Firma firma)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Firma>("delete from Firma where Nazwa_Firmy=" + firma.Nazwa_Firmy);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
