using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public interface IUser
    {
        string DeleteUser(string key);
        List<SQLiteModels.User> GetAllUsers();
        SQLiteModels.User GetUser(string key);        
        string SaveObject(SQLiteModels.User user);
        string UpdateObject(SQLiteModels.User user);
    }
}
