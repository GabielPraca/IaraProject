using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IaraModels;

namespace IaraDAO
{
    public class UserRepository : IUserRepository
    {
        Context _Context = new Context();

        public UserRepository()
        {
            _Context.Database.CreateIfNotExists();
        }

        public bool DeleteUser(string email)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUser(string email)
        {
            try
            {
                return _Context.User.Where(u => u.email == email).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveUser(User user)
        {
            try
            {
                _Context.User.Add(user);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                User userToUpdate = _Context.User.Where(u => u.email == user.email).FirstOrDefault();

                if (userToUpdate != null)
                {
                    userToUpdate = user;
                    _Context.User.Attach(userToUpdate);
                    _Context.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;
                    _Context.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
