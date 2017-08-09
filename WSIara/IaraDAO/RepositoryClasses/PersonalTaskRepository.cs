using System;
using IaraModels;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace IaraDAO
{
    public class PersonalTaskRepository : IPersonalTaskRepository
    {
        Context _Context = new Context();

        public PersonalTaskRepository()
        {
            _Context.Database.CreateIfNotExists();
        }

        public bool SavePersonalTask(PersonalTask personalTask)
        {
            try
            {
                _Context.PersonalTask.Add(personalTask);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePersonalTask(PersonalTask personalTask)
        {
            try
            {
                _Context.PersonalTask.Remove(personalTask);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePersonalTask(PersonalTask personalTask)
        {
            try
            {
                PersonalTask taskToUpdate = GetPersonalTask(personalTask);

                if (taskToUpdate != null)
                {
                    taskToUpdate = personalTask;
                    _Context.PersonalTask.Attach(taskToUpdate);
                    _Context.Entry(taskToUpdate).State = System.Data.Entity.EntityState.Modified;
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

        public PersonalTask GetPersonalTask(PersonalTask personalTask)
        {
            try
            {
                return _Context.PersonalTask.Where(p => p.description == personalTask.description
                                                   && p.email == personalTask.email
                                                   && p.taskDay == personalTask.taskDay).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PersonalTask> GetAllPersonalTasks(string email)
        {
            try
            {
                return _Context.PersonalTask.Where(p => p.email == email).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PersonalTask> GetAllActivePersonalTasks(string email)
        {
            try
            {
                return _Context.PersonalTask.Where(p => p.email == email && !p.deleted).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
