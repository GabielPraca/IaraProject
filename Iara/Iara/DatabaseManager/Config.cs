using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public static class Config
    {
        public static SQLiteConnection databaseConn;
        private static readonly string _dbName = "Iara.db3";
        public static string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), _dbName);
    }
}
