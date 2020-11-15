using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace Extrator_TARGIT
{
    public class Connections
    {
       
        public static string Teste(string data)
        {
            string path = "C:\\Users\\ADMINISTRATOR\\Desktop\\logs";
            string cnpj = GetSQLiteData();
            string result = GetMongoDBData(cnpj);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var arquivo = path + "\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(arquivo))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(arquivo))
                {
                    //sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(arquivo))
                {
                    //sw.WriteLine(Message);
                    //sw.WriteLine(GetSQLiteData());

                    sw.WriteLine(result);

                }

            }
            return result;

        }
    

        public static string GetSQLiteData()
        {
            int RowCount = 0;
            string cnpj = "";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=C:\\Users\\ADMINISTRATOR\\Desktop\\cadastro_db.sqlite"))
            {

                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT count(*) FROM Cadastro";
                    fmd.CommandType = CommandType.Text;

                    RowCount = Convert.ToInt32(fmd.ExecuteScalar());
                }

                // Exceção se tiver mais de um registro na base sqlite
                // Não executar se tiver mais de um
                if (RowCount > 1)
                {
                    return "";
                }

                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT * FROM Cadastro";
                    fmd.CommandType = CommandType.Text;

                    SQLiteDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        cnpj = Convert.ToString(r["Cnpj"]);
                    }
                }

            }

            return cnpj;
        }

        public static string GetMongoDBData(string cnpj)
        {
            /* var credential = MongoCredential.CreateCredential("dbTesteFree", "admin", "0123456");*/
            MongoClient dbClient = new MongoClient("mongodb+srv://dbTesteFree:mother4278@cluster0.oceqg.gcp.mongodb.net/dbTesteFree?retryWrites=true&w=majority");

            var database = dbClient.GetDatabase("dbTesteFree");

            var collection = database.GetCollection<BsonDocument>("clientes");

            var filter = Builders<BsonDocument>.Filter.Eq("cnpj", cnpj);
            var document = collection.Find(filter).FirstOrDefault();

            string result = document.ToString();

            return result;
        }
        public Connections(string teste)
        {
          
        }
    }
   
}
