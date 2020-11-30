using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace ClientExtratorTARGIT
{
    public class SQLite
    {   
        public void WriteToSQLite(string cnpj, string ip, string horarios_execucao, int dias_incremental)
        {
            // checar se arquivo existe
            // SQLiteConnection.CreateFile(@'');
            SQLiteConnection sqliteConnection = new SQLiteConnection(@"Data Source=C:\\Users\\ADMINISTRATOR\\Desktop\\cadastro_db.sqlite");
            try
            {
                using (var cmd = sqliteConnection.CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS InfoExtrator(cnpj TEXT, ip TEXT, horarios_execucao TEXT, dias_incremental INTEGER)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // if rowcount > 1: messagebox "cadastro já existe"
            try
            {
                using (var cmd = sqliteConnection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO InfoExtrator(cnpj, ip, horarios_execucao, dias_incremental) values (@cnpj, @ip, @horarios_execucao, @dias_incremental)";
                    cmd.Parameters.AddWithValue("@cnpj", cnpj);
                    cmd.Parameters.AddWithValue("@ip", ip);
                    cmd.Parameters.AddWithValue("@horarios_execucao", horarios_execucao);
                    cmd.Parameters.AddWithValue("@dias_incremental", dias_incremental);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ReadSQLite()
        {
            int RowCount;
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=C:\\Users\\ADMINISTRATOR\\Desktop\\cadastro_db.sqlite"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT count(*) FROM Cadastro";
                    fmd.CommandType = CommandType.Text;

                    RowCount = Convert.ToInt32(fmd.ExecuteScalar());
                }
            }
            return RowCount;
        }

    }
}
