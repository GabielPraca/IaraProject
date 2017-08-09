using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaraDAO
{
    public interface IUserRepository
    {
        bool DeleteUser(string email);
        List<IaraModels.User> GetAllUsers();
        IaraModels.User GetUser(string email);
        bool SaveUser(IaraModels.User user);
        bool UpdateUser(IaraModels.User user);
    }
}
