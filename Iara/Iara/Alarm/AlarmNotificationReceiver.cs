using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Support.V7.App;
using DatabaseManager;
using System;

namespace Iara
{
    [BroadcastReceiver(Enabled = true)]
    public class AlarmNotificationReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                TaskTransporter.CallTaskTransporter(context, intent);

                SQLiteModels.PersonalTask pt = BODatabaseManager.GetAllActivePersonalTasks(
                       Config.loggedUser.email).Where(p => p.description == TaskTransporter.itens[0] &&
                       string.Concat(p.taskDay.Hour.ToString("00"), ":", p.taskDay.Minute.ToString("00")) == TaskTransporter.itens[1]).FirstOrDefault();

                if (pt != null && !pt.finalized)
                {
                    NotificationCompat.Builder builder = new NotificationCompat.Builder(context);

                    Intent notificationIntent = new Intent(context, typeof(RootActivity));

                    PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);

                    List<string> itens = intent.GetStringArrayListExtra("task").ToList();
                    builder.SetAutoCancel(true)
                        .SetDefaults((int)NotificationDefaults.All)
                        .SetSmallIcon(Resource.Drawable.Icon)
                        .SetContentIntent(pendingIntent)
                        .SetContentTitle("Tarefa Pendente!")
                        .SetContentText(itens[0])
                        .SetContentInfo(itens[1])
                        .Build();

                    AlarmRingtone.PlayRingtone(Application.Context);
                    //18-09-2017 AlarmRingtone.PlayRingtone(context);

                    NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
                    var oldNotification = manager.GetActiveNotifications();
                    if (oldNotification != null)
                    {
                        foreach (var notification in oldNotification)
                        {
                            notification.Dispose();
                        }
                    }

                    manager.Notify(1, builder.Build());
                }
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}