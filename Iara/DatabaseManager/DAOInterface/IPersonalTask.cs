using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public interface IPersonalTask
    {
        bool DeletePersonalTask(SQLiteModels.PersonalTask task); 
         List<SQLiteModels.PersonalTask> GetAllActivePersonalTasks(string email);
        List<SQLiteModels.PersonalTask> GetAllPersonalTasks(string email);
        SQLiteModels.PersonalTask GetPersonalTask(string key);
        string SaveObject(SQLiteModels.PersonalTask task);
        string UpdateObject(SQLiteModels.PersonalTask task);
    }
}
