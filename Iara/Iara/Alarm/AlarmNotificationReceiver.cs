using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Support.V7.App;

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

                AlarmRingtone.PlayRingtone(context);

                NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
                manager.Notify(1, builder.Build());
            }
            catch (System.Exception)
            {

            }            
        }
    }
}