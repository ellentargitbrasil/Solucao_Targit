using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace Extrator_TARGIT
{
    public partial class ExtratorTARGITService : ServiceBase
    {
        
        Timer timer = new Timer(); // name space(using System.Timers;)
        public ExtratorTARGITService()
        {
            InitializeComponent();
        }
      

        protected override void OnStart(string[] args)
        {
            
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds  
            timer.Enabled = true;

        }
        protected override void OnStop()
        {
            WriteFile("Serviço parado" + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            string horarioExecucao0 = GetHorarioExecucao();
            RodaAgora(horarioExecucao0);

            string horarioExecucao1 = GetSegundoHorarioExecucao();
            RodaAgora(horarioExecucao1);

        }
        public string GetHorarioExecucao()
        {
            // pega cnpj que está no sqlite
            string cnpj = GetSQLiteData();

            // pega as informações do mongo referentes ao cnpj
            BsonDocument mongoData = GetMongoDBData(cnpj);

            int ativo = mongoData["ativo"].ToInt32();
            List<String> listaHorario = mongoData["horario"].AsBsonArray.Select(p => p.AsString).ToList();

            string horarioExecucao = listaHorario[0].ToString();
           
            return horarioExecucao;
        }
        public string GetSegundoHorarioExecucao()
        {
            // pega cnpj que está no sqlite
            string cnpj = GetSQLiteData();

            // pega as informações do mongo referentes ao cnpj
            BsonDocument mongoData = GetMongoDBData(cnpj);

            int ativo = mongoData["ativo"].ToInt32();
            List<String> listaHorario = mongoData["horario"].AsBsonArray.Select(p => p.AsString).ToList();

            string segundoHorarioExecucao = listaHorario[1].ToString();


            return segundoHorarioExecucao;
        }


        public void RodaAgora(string horarioExecucao)
        {
            var user_time = DateTime.Parse(horarioExecucao);
            var curTime = DateTime.Now;

            WriteFile(user_time.ToString());
            WriteFile(curTime.ToString());
            
            if (user_time.Hour == curTime.Hour && user_time.Minute == curTime.Minute) // If now 5 min of any hour
            {
                WriteFile("TA RODANO");
            } else
            {
                WriteFile("NAO TA RODANO");
            }
        }

        public void WriteFile(string Message)
        {
            
            string path = "C:\\Users\\ADMINISTRATOR\\Desktop\\logs";
            
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
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(arquivo))
                {

                    sw.WriteLine(Message);

                }
            }
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
                if(RowCount > 1)
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

        public static BsonDocument GetMongoDBData(string cnpj) 
        {
           /* var credential = MongoCredential.CreateCredential("dbTesteFree", "admin", "0123456");*/
            MongoClient dbClient = new MongoClient("mongodb+srv://dbTesteFree:mother4278@cluster0.oceqg.gcp.mongodb.net/dbTesteFree?retryWrites=true&w=majority");
       
            var database = dbClient.GetDatabase("dbTesteFree");

            var collection = database.GetCollection<BsonDocument>("clientes");

            var filter = Builders<BsonDocument>.Filter.Eq("cnpj", cnpj);

            var document = collection.Find(filter).FirstOrDefault();

            

            return document;
        }
    }
}
