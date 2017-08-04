using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaraDAO
{
    interface IPersonalTaskRepository
    {
        bool SavePersonalTask(IaraModels.PersonalTask personalTask);
    }
}
