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
                personalTask.synchronizedInToServer = true;
                _Context.PersonalTask.Add(personalTask);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SavePersonalTasks(List<PersonalTask> personalTasks)
        {
            List<PersonalTask> ptToAdd = new List<PersonalTask>();
            try
            {
                foreach(PersonalTask pt in personalTasks)
                {
                    pt.synchronizedInToServer = true;
                    var res = GetPersonalTask(pt);
                    if (res != null)
                    {
                        UpdatePersonalTask(pt);
                    }
                    else
                    {
                        ptToAdd.Add(pt);
                    }
                }

                _Context.PersonalTask.AddRange(ptToAdd);
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
                _Context.PersonalTask.Attach(personalTask);
                _Context.Entry(personalTask).State = System.Data.Entity.EntityState.Deleted;
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePersonalTasks(List<PersonalTask> personalTasks)
        {
            try
            {
                foreach(PersonalTask pt in personalTasks)
                {
                    _Context.PersonalTask.Attach(pt);
                    _Context.Entry(pt).State = System.Data.Entity.EntityState.Deleted;
                    _Context.SaveChanges();
                }
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
                if (personalTask != null)
                {
                    var res = GetPersonalTask(personalTask);
                    res.deleted = personalTask.deleted;
                    res.description = personalTask.description;
                    res.email = personalTask.email;
                    res.finalized = personalTask.finalized;
                    res.fri = personalTask.fri;
                    res.mon = personalTask.mon;
                    res.repeat = personalTask.repeat;
                    res.sat = personalTask.sat;
                    res.sun = personalTask.sun;
                    res.synchronizedInToMobile = personalTask.synchronizedInToMobile;
                    res.synchronizedInToServer = personalTask.synchronizedInToServer;
                    res.taskDay = personalTask.taskDay;
                    res.thu = personalTask.thu;
                    res.tue = personalTask.tue;
                    res.wed = personalTask.wed;

                    //_Context.PersonalTask.Attach(res);
                    //_Context.Entry(res).CurrentValues.SetValues(personalTask);
                    _Context.Entry(res).State = System.Data.Entity.EntityState.Modified;
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
