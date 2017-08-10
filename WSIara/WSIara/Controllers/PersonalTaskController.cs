﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WSIara.Controllers
{
    [RoutePrefix("api/PersonalTask/")]
    public class PersonalTaskController : ApiController
    {
        IaraDAO.IPersonalTaskRepository ptr;

        public PersonalTaskController()
        {
            ptr = new IaraDAO.PersonalTaskRepository();
        }

        [Route("SavePersonalTask")]
        [HttpPost]
        public bool SavePersonalTask([FromBody] IaraModels.PersonalTask personalTask)
        {
            return ptr.SavePersonalTask(personalTask);
        }

        [Route("DeletePersonalTask")]
        [HttpPost]
        public bool DeletePersonalTask([FromBody] IaraModels.PersonalTask personalTask)
        {
            return ptr.DeletePersonalTask(personalTask);
        }

        [Route("UpdatePersonalTask")]
        [HttpPost]
        public bool UpdatePersonalTask([FromBody] IaraModels.PersonalTask personalTask)
        {
            return ptr.UpdatePersonalTask(personalTask);
        }

        [Route("GetPersonalTask")]
        [HttpPost]
        public IaraModels.PersonalTask GetPersonalTask([FromBody] IaraModels.PersonalTask personalTask)
        {
            return ptr.GetPersonalTask(personalTask);
        }

        [Route("{email}/GetAllPersonalTasks")]
        [HttpGet]
        public List<IaraModels.PersonalTask> GetAllPersonalTasks(string email)
        {
            return ptr.GetAllPersonalTasks(email);
        }

        [Route("{email}/GetAllActivePersonalTasks")]
        [HttpGet]
        public List<IaraModels.PersonalTask> GetAllActivePersonalTasks(string email)
        {
            return ptr.GetAllActivePersonalTasks(email);
        }
    }
}