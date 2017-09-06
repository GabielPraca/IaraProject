using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteModels;

namespace DatabaseManager
{
    public interface IPersonalTask
    {
        bool DeletePersonalTasks(List<SQLiteModels.PersonalTask> tasks); 
        List<SQLiteModels.PersonalTask> GetAllActivePersonalTasks(string email);
        List<SQLiteModels.PersonalTask> GetAllPersonalTasks(string email);
        SQLiteModels.PersonalTask GetPersonalTask(string key);
        string SaveObject(SQLiteModels.PersonalTask task);
        string UpdatePersonalTask(SQLiteModels.PersonalTask task);
    }
}
