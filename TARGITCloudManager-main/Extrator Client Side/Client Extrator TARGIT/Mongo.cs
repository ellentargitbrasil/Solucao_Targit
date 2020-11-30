using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ClientExtratorTARGIT
{
    // funcao dessa classe: conectar ao mongodb (na nuvem) e retornar os dados do cnpj correspondente

   public class Mongo
    {
       /* public string cnpj { get; set; }*/
               
        public BsonDocument GetData(string cnpj)
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://dbTesteFree:mother4278@cluster0.oceqg.gcp.mongodb.net/dbTesteFree?retryWrites=true&w=majority");

            var database = dbClient.GetDatabase("dbTesteFree");

            var collection = database.GetCollection<BsonDocument>("clientes");

            var filter = Builders<BsonDocument>.Filter.Eq("cnpj", cnpj);
            var clienteDocument = collection.Find(filter).FirstOrDefault();

            
            /*Console.WriteLine(clienteDocument.ToString());
            Console.WriteLine(teste);*/

           /*Console.WriteLine(teste.ToString());
            if(teste == texto)
            {
                Console.WriteLine("sao iguais");
            }
            else
            {
                Console.WriteLine("são diferentes"); 
            }*/

            return (BsonDocument)clienteDocument;
        }
    }
}
