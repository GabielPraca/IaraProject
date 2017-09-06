using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaraDAO
{
    public interface IPersonalTaskRepository
    {
        bool SavePersonalTask(IaraModels.PersonalTask personalTask); 
        bool DeletePersonalTasks(List<IaraModels.PersonalTask> personalTask);
        bool DeletePersonalTask(IaraModels.PersonalTask personalTask);
        bool UpdatePersonalTask(IaraModels.PersonalTask personalTask);
        bool SavePersonalTasks(List<IaraModels.PersonalTask> personalTasks);
        IaraModels.PersonalTask GetPersonalTask(IaraModels.PersonalTask personalTask);
        List<IaraModels.PersonalTask> GetAllPersonalTasks(string email);
        List<IaraModels.PersonalTask> GetAllActivePersonalTasks(string email);
    }
}
