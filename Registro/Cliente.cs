using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Registro
{
   public class Cliente
    {
        public long? Id { get; set; }
        public string Cnpj { get; set; }
        public string Ip { get; set; }


    }
}