using System;
using System.Collections.Generic;
using System.Linq;

using DatabaseManager;
using System.Threading.Tasks;
using System.Timers;

namespace Iara.Workers
{
    public static class DatabaseWorker
    {
        private static IaraWrapper.IaraWrapper iw = null;
        private static bool running = false;

        public static void Run(object source, ElapsedEventArgs e)
        {
            try
            {
                if (!running)
                {
                    running = true;
                    iw = new IaraWrapper.IaraWrapper(Config.loggedUser.email, Config.loggedUser.password);

                    //Sincroniza do local para o WS
                    SynchronizeToServer();

                    //Sincroniza do server para o local
                    SynchronizeToLocal();

                    //Efetiva a deleção
                    EffectiveDeletion();
                    running = false;
                }
            }
            catch (Exception ex)
            {
                running = false;
            }
        }

        private static void SynchronizeToServer()
        {
            List<SQLiteModels.PersonalTask> tasksToUpdt = BODatabaseManager.GetAllPersonalTask(Config.loggedUser.email).Where(p => !p.synchronizedInToServer).ToList();
            List<IaraModels.PersonalTask> tasksToUpdtConverted = new List<IaraModels.PersonalTask>();

            foreach (SQLiteModels.PersonalTask pt in tasksToUpdt)
            {
                pt.synchronizedInToServer = true;
            }

            foreach (SQLiteModels.PersonalTask pt in tasksToUpdt)
            {
                tasksToUpdtConverted.Add(new IaraModels.PersonalTask
                {
                    email = pt.email,
                    deleted = pt.deleted,
                    finalized = pt.finalized,
                    description = pt.description,
                    repeat = pt.repeat,
                    synchronizedInToServer = pt.synchronizedInToServer,
                    synchronizedInToMobile = pt.synchronizedInToMobile,
                    taskDay = pt.taskDay,
                    fri = pt.fri,
                    mon = pt.mon,
                    sat = pt.sat,
                    sun = pt.sun,
                    thu = pt.thu,
                    tue = pt.tue,
                    wed = pt.wed
                });
            }

            //Atualiza no servidor
            if (tasksToUpdtConverted != null && tasksToUpdtConverted.Count > 0)
            {
                iw.SavePersonalTasks(tasksToUpdtConverted);
            }
            //Atualiza Local
            foreach (SQLiteModels.PersonalTask task in tasksToUpdt)
            {
                BODatabaseManager.UpdatePersonalTask(task);
            }
        }

        private static void SynchronizeToLocal()
        {
            List<IaraModels.PersonalTask> tasksToUpdt = iw.GetAllPersonalTasks(Config.loggedUser.email);
            if (tasksToUpdt != null && tasksToUpdt.Count > 0)
            {
                tasksToUpdt = tasksToUpdt.Where(p => !p.synchronizedInToMobile).ToList();

                foreach (IaraModels.PersonalTask pt in tasksToUpdt)
                {
                    pt.synchronizedInToMobile = true;
                }
            }
            List<SQLiteModels.PersonalTask> tasksToUpdtConverted = new List<SQLiteModels.PersonalTask>();

            //Atualiza no Servidor
            if (tasksToUpdt != null && tasksToUpdt.Count > 0)
            {
                foreach (IaraModels.PersonalTask pt in tasksToUpdt)
                {
                    tasksToUpdtConverted.Add(new SQLiteModels.PersonalTask
                    {
                        email = pt.email,
                        deleted = pt.deleted,
                        finalized = pt.finalized,
                        description = pt.description,
                        repeat = pt.repeat,
                        synchronizedInToServer = pt.synchronizedInToServer,
                        synchronizedInToMobile = pt.synchronizedInToMobile,
                        taskDay = pt.taskDay,
                        fri = pt.fri,
                        mon = pt.mon,
                        sat = pt.sat,
                        sun = pt.sun,
                        thu = pt.thu,
                        tue = pt.tue,
                        wed = pt.wed
                    });
                }
                iw.SavePersonalTasks(tasksToUpdt);
            }
            //Atualiza Local
            foreach (SQLiteModels.PersonalTask task in tasksToUpdtConverted)
            {
                BODatabaseManager.UpdatePersonalTask(task);
            }
        }

        private static void EffectiveDeletion()
        {
            List<SQLiteModels.PersonalTask> tasksToDeleteMobile = BODatabaseManager.GetAllPersonalTask(Config.loggedUser.email).Where(p => p.deleted).ToList();
            List<IaraModels.PersonalTask> tasksToDeleteServer = iw.GetAllPersonalTasks(Config.loggedUser.email);
            if (tasksToDeleteServer != null && tasksToDeleteServer.Count > 0)
            {
                tasksToDeleteServer = tasksToDeleteServer.Where(p => p.deleted).ToList();
            }

            //Deleta do Servidor
            iw.DeletePersonalTasks(tasksToDeleteServer);
            //Deleta do Local
            BODatabaseManager.DeletePersonalTasks(tasksToDeleteMobile);
        }
    }
}