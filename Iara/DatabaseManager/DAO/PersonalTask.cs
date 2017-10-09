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

        public bool DeletePersonalTasks(List<SQLiteModels.PersonalTask> tasks)
        {
            try
            {
                foreach(SQLiteModels.PersonalTask pt in tasks)
                {
                    Config.databaseConn.Delete(pt);
                }
                return true;
            }
            catch (Exception ex)
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
                {
                    task.synchronizedInToMobile = true;
                    Config.databaseConn.Insert(task);
                }
                else
                {
                    return DatabaseAnswer.Error.ToString();
                }

                return DatabaseAnswer.Sucess.ToString();
            }
            catch (Exception)
            {
                return DatabaseAnswer.Error.ToString();
            }
        }

        public string UpdatePersonalTask(SQLiteModels.PersonalTask task)
        {
            try
            {
                var all = Config.databaseConn.Table<SQLiteModels.PersonalTask>().AsEnumerable<SQLiteModels.PersonalTask>().Select(o => o.personalTaskID == task.personalTaskID).ToList();

                if (all.Count.Equals(0))
                    return DatabaseAnswer.Error.ToString();
                else
                {
                    SQLiteModels.PersonalTask ptToUpdt = GetAllActivePersonalTasks(task.email).Where(t => t.description == task.description &&
                                                                                                     t.email == task.email && t.taskDay == task.taskDay).FirstOrDefault();

                    ptToUpdt.deleted = task.deleted;
                    ptToUpdt.description = task.description;
                    ptToUpdt.email = task.email;
                    ptToUpdt.finalized = task.finalized;
                    ptToUpdt.fri = task.fri;
                    ptToUpdt.mon = task.mon;
                    ptToUpdt.repeat = task.repeat;
                    ptToUpdt.sat = task.sat;
                    ptToUpdt.sun = task.sun;
                    ptToUpdt.synchronizedInToMobile = task.synchronizedInToMobile;
                    ptToUpdt.synchronizedInToServer = task.synchronizedInToServer;
                    ptToUpdt.taskDay = task.taskDay;
                    ptToUpdt.thu = task.thu;
                    ptToUpdt.tue = task.tue;
                    ptToUpdt.wed = task.wed;

                    Config.databaseConn.Update(ptToUpdt);
                }

                return DatabaseAnswer.Sucess.ToString();

            }
            catch (Exception)
            {
                return DatabaseAnswer.Error.ToString();
            }
        }
    }
}
