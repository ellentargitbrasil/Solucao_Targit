using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Registro
{
    public class DalHelper: Cliente
    {
        private static SQLiteConnection sqliteConnection;
        public DalHelper()
        {
        }
        private static SQLiteConnection DbConnection()
        {
            string password = "1223334444";
            byte[] passwordBytes = GetBytes(password);

            sqliteConnection = new SQLiteConnection(@"Data Source=C:\\Users\\ADMINISTRATOR\Desktop\\Solucao Extrator TARGIT\\Registro\\bin\banco\\cadastro_db.sqlite"+ "Password=" + password);
            sqliteConnection.Open();
            return sqliteConnection;
        }

        private static byte[] GetBytes(string password)
        {

            byte[] bytes = new byte[password.Length * sizeof(char)];
            bytes = System.Text.Encoding.Default.GetBytes(password);
            return bytes;
        }


        public static void CriarBancoSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile("cadastro_db.sqlite");
            }
            catch
            {
                throw;
            }
        }
        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Cadastro(Cnpj Numeric, Ip Numeric)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetCadastro()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Cadastro";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetCadastro(int id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Cadastro Where Id=" + id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         public static void Add(Cliente cli)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Cadastro(Cnpj, Ip) values (@Cnpj, @Ip)";
                    cmd.Parameters.AddWithValue("@Cnpj", cli.Cnpj);
                    cmd.Parameters.AddWithValue("@Ip", cli.Ip);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
          public static void Uptade (Cliente cliente)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (cliente.Id != null)
                    {
                        cmd.CommandText = "UPDATE Cadastro SET Cnpj=@Cnpj, Ip=@Ip WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("@Id", cliente.Id);
                        cmd.Parameters.AddWithValue("@Cnpj", cliente.Cnpj);
                        cmd.Parameters.AddWithValue("@Ip", cliente.Ip);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Delete(int Id)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "DELETE FROM Cadastro Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
