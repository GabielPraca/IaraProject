using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteModels;
using SQLite;

namespace DatabaseManager.DAO
{
    class PersonalTask : IPersonalTask
    {
        public PersonalTask()
        {
            try
            {
                Config.databaseConn = new SQLiteConnection(Config._dbPath);
                Config.databaseConn.CreateTable<SQLiteModels.PersonalTask>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeletePersonalTask(SQLiteModels.PersonalTask task)
        {
            try
            {
                task.deleted = true;
                UpdateObject(task);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SQLiteModels.PersonalTask> GetAllPersonalTasks(string email)
        {
            try
            {
                var result = Config.databaseConn.Table<SQLiteModels.PersonalTask>().AsEnumerable().Where(o => o.email == email).ToList();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SQLiteModels.PersonalTask> GetAllActivePersonalTasks(string email)
        {
            try
            {
                var result = Config.databaseConn.Table<SQLiteModels.PersonalTask>().AsEnumerable().Where(o => o.email == email && !o.deleted).ToList();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SQLiteModels.PersonalTask GetPersonalTask(string key)
        {
            throw new NotImplementedException();
        }

        public string SaveObject(SQLiteModels.PersonalTask task)
        {
            try
            {
                var all = Config.databaseConn.Table<SQLiteModels.PersonalTask>().AsEnumerable().Where(o => o.personalTaskID == task.personalTaskID).ToList();

                if (all.Count == 0)
                    Config.databaseConn.Insert(task);
                else
                    return DatabaseAnswer.Error.ToString();

                return DatabaseAnswer.Sucess.ToString();
            }
            catch (Exception)
            {
                return DatabaseAnswer.Error.ToString();
            }
        }

        public string UpdateObject(SQLiteModels.PersonalTask task)
        {
            try
            {
                var all = Config.databaseConn.Table<SQLiteModels.PersonalTask>().AsEnumerable<SQLiteModels.PersonalTask>().Select(o => o.personalTaskID == task.personalTaskID).ToList();

                if (all.Count.Equals(0))
                    return DatabaseAnswer.Error.ToString();
                else
                    Config.databaseConn.Update(task);

                return DatabaseAnswer.Sucess.ToString();

            }
            catch (Exception)
            {
                return DatabaseAnswer.Error.ToString();
            }
        }
    }
}
