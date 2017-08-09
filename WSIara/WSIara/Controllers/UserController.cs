using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WSIara.Controllers
{
    [RoutePrefix("api/User/")]
    public class UserController : ApiController
    {
        IaraDAO.IUserRepository ur;

        public UserController()
        {
            ur = new IaraDAO.UserRepository();
        }

        [Route("SaveUser")]
        [HttpPost]
        public bool SaveUser([FromBody] IaraModels.User user)
        {
            return ur.SaveUser(user);
        }

        [Route("{email}/DeleteUser")]
        [HttpGet]
        public bool DeleteUser(string email)
        {
            return ur.DeleteUser(email);
        }

        [Route("UpdateUser")]
        [HttpPost]
        public bool UpdateUser([FromBody] IaraModels.User user)
        {
            return ur.UpdateUser(user);
        }

        [Route("{email}/GetUser")]
        [HttpGet]
        public IaraModels.User GetUser(string email)
        {
            return ur.GetUser(email);
        }
    }
}
