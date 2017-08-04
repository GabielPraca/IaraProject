using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using DatabaseManager;

namespace DatabaseManager.DAO
{
    class User : IUser
    {
        public User()
        {
            try
            {
                Config.databaseConn = new SQLiteConnection(Config._dbPath);
                Config.databaseConn.CreateTable<SQLiteModels.User>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteUser(string key)
        {
            throw new NotImplementedException();
        }

        public List<SQLiteModels.User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public SQLiteModels.User GetUser(string key)
        {
            try
            {
                var result = Config.databaseConn.Table<SQLiteModels.User>().AsEnumerable<SQLiteModels.User>().Where(o => o.email == key).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string SaveObject(SQLiteModels.User user)
        {
            try
            {
                //CreateTable();

                var all = Config.databaseConn.Table<SQLiteModels.User>().AsEnumerable<SQLiteModels.User>().Where(o => o.email == user.email).ToList();

                if (all.Count == 0)
                    Config.databaseConn.Insert(user);
                else
                    return DatabaseAnswer.Error.ToString();

                return DatabaseAnswer.Sucess.ToString();
            }
            catch (Exception)
            {
                return DatabaseAnswer.Error.ToString();
            }
        }

        public string UpdateObject(SQLiteModels.User user)
        {
            try
            {
                var all = Config.databaseConn.Table<SQLiteModels.User>().AsEnumerable<SQLiteModels.User>().Select(o => o.email == user.email).ToList();

                if (all.Count.Equals(0))
                    return DatabaseAnswer.Error.ToString();
                else
                    Config.databaseConn.Update(user);

                return DatabaseAnswer.Sucess.ToString();

            }
            catch (Exception)
            {
                return DatabaseAnswer.Error.ToString();
            }
        }
    }
}