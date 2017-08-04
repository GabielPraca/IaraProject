using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public class BODatabaseManager
    {
        private static IUser userMethods = new DAO.User();
        private static IPersonalTask personalTask = new DAO.PersonalTask();

        public BODatabaseManager()
        {
            userMethods = new DAO.User();
            personalTask = new DAO.PersonalTask();
        }

        #region user
        public static string CreateUser(SQLiteModels.User user)
        {
            return userMethods.SaveObject(user);
        }

        public static string UpdateUser(SQLiteModels.User user)
        {
            return userMethods.UpdateObject(user);
        }

        public static string DeleteUser(string email)
        {
            return userMethods.DeleteUser(email);
        }

        public static SQLiteModels.User GetUser(string email)
        {
            return userMethods.GetUser(email);
        }

        public static string UserAuthentication(string email, string pass)
        {
            SQLiteModels.User ret = userMethods.GetUser(email);

            if (ret == null)
                return "Usuário não existe!";
            else if (ret.email == email && ret.password != pass)
                return "Login e ou Senha Inválida!";
            else
                return DatabaseAnswer.Sucess.ToString();
        }

        #endregion

        #region PersonalTask
        public static string CreatePersonalTask(SQLiteModels.PersonalTask task)
        {
            return personalTask.SaveObject(task);
        }

        public static List<SQLiteModels.PersonalTask> GetAllPersonalTask(string email)
        {
            return personalTask.GetAllPersonalTasks(email);
        }

        public static List<SQLiteModels.PersonalTask> GetAllActivePersonalTasks(string email)
        {
            return personalTask.GetAllActivePersonalTasks(email).OrderBy(p => p.taskDay).ToList();
        }

        public static string UpdateObject(SQLiteModels.PersonalTask task)
        {
            return personalTask.UpdateObject(task);
        }
        #endregion
    }
}
