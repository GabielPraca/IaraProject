using Android.App;
using Android.Content;
using Android.Icu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using SQLiteModels;

namespace Iara.Workers
{
    public static class AlarmWorker
    {
        private static IaraWrapper.IaraWrapper iw = null;

        //public static List<SQLiteModels.PersonalTask> personalTasks = null;
        public static SQLiteModels.PersonalTask personalTask = null;
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static bool weekly = false;

        private static bool running = false;

        public static void Run(object source, ElapsedEventArgs e)
        {
        //    if (!running)
        //    {
        //        running = true;
        //        iw = new IaraWrapper.IaraWrapper(Config.loggedUser.email, Config.loggedUser.password);

        //        //Pega todas as tarefas
        //        GetTasksToAlert();

        //        running = false;
        //    }
        //}

        //private static void GetTasksToAlert()
        //{

        //    personalTask = DatabaseManager.BODatabaseManager.GetAllActivePersonalTasks(Config.loggedUser.email).Where(
        //                                                                                                        p => p.taskDay > DateTime.Now && !p.finalized).OrderBy(
        //                                                                                                        p => p.taskDay).ToList().FirstOrDefault();

        //    StartAlarm(personalTask.repeat, personalTask);
        //    //personalTasks = DatabaseManager.BODatabaseManager.GetAllActivePersonalTasks(Config.loggedUser.email).Where(
        //    //                                                                                                    p => p.taskDay > DateTime.Now && !p.finalized).OrderBy(
        //    //                                                                                                    p => p.taskDay).ToList();

        //    //foreach (SQLiteModels.PersonalTask pst in personalTasks)
        //    //{
        //    //    StartAlarm(pst.repeat, pst);
        //    //}
        //}

        //private static void StartAlarm(bool isRepeating, SQLiteModels.PersonalTask personalTask)
        //{
        //    var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, new Intent(Application.Context, typeof(AlarmNotificationReceiver)), PendingIntentFlags.UpdateCurrent);
        //    AlarmManager alarm = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);

        //    weekly = VerifyTimerType(personalTask);

        //    if (!weekly)
        //    {
        //        DateTime utcAlarmTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(personalTask.taskDay.Ticks, DateTimeKind.Local));
        //        long timeMillis = (long)(utcAlarmTime - Jan1st1970).TotalMilliseconds;

        //        SetAlarm(alarm, timeMillis, pendingIntent, personalTask.repeat);
        //    }
        //    else
        //    {
        //        long alarmTime = 0;
        //        if (personalTask.sun)
        //        {
        //            alarmTime = BuildCalendar(Calendar.Sunday, personalTask).TimeInMillis;
        //            SetAlarm(alarm, alarmTime, pendingIntent, personalTask.repeat);
        //        }
        //        if (personalTask.mon)
        //        {
        //            alarmTime = BuildCalendar(Calendar.Monday, personalTask).TimeInMillis;
        //            SetAlarm(alarm, alarmTime, pendingIntent, personalTask.repeat);
        //        }
        //        if (personalTask.tue)
        //        {
        //            alarmTime = BuildCalendar(Calendar.Tuesday, personalTask).TimeInMillis;
        //            SetAlarm(alarm, alarmTime, pendingIntent, personalTask.repeat);
        //        }
        //        if (personalTask.wed)
        //        {
        //            alarmTime = BuildCalendar(Calendar.Wednesday, personalTask).TimeInMillis;
        //            SetAlarm(alarm, alarmTime, pendingIntent, personalTask.repeat);
        //        }
        //        if (personalTask.thu)
        //        {
        //            alarmTime = BuildCalendar(Calendar.Thursday, personalTask).TimeInMillis;
        //            SetAlarm(alarm, alarmTime, pendingIntent, personalTask.repeat);
        //        }
        //        if (personalTask.fri)
        //        {
        //            alarmTime = BuildCalendar(Calendar.Friday, personalTask).TimeInMillis;
        //            SetAlarm(alarm, alarmTime, pendingIntent, personalTask.repeat);
        //        }
        //        if (personalTask.sat)
        //        {
        //            alarmTime = BuildCalendar(Calendar.Saturday, personalTask).TimeInMillis;
        //            SetAlarm(alarm, alarmTime, pendingIntent, personalTask.repeat);
        //        }
        //    }
        //}

        //private static void SetAlarm(AlarmManager alarm, long alarmTime, PendingIntent pendingIntent, bool reapeat)
        //{
        //    if (reapeat && weekly)
        //    {
        //        alarm.SetRepeating(AlarmType.RtcWakeup, alarmTime, AlarmManager.IntervalDay * 7, pendingIntent);
        //    }
        //    else
        //    {
        //        alarm.Set(AlarmType.RtcWakeup, alarmTime, pendingIntent);
        //    }
        //}

        //private static Calendar BuildCalendar(int dayOfWeek, SQLiteModels.PersonalTask personalTask)
        //{
        //    Calendar alarmCalendar = Calendar.Instance;
        //    if (personalTask.taskDay.Hour >= 0 && personalTask.taskDay.Hour <= 12)
        //    {
        //        alarmCalendar.Set(CalendarField.AmPm, Calendar.Am);
        //    }
        //    else
        //    {
        //        alarmCalendar.Set(CalendarField.AmPm, Calendar.Pm);
        //    }
        //    alarmCalendar.Set(CalendarField.DayOfWeek, dayOfWeek - 1);
        //    alarmCalendar.Set(CalendarField.Hour, personalTask.taskDay.Hour);
        //    alarmCalendar.Set(CalendarField.Minute, personalTask.taskDay.Minute);
        //    alarmCalendar.Set(CalendarField.Second, 0);

        //    return alarmCalendar;
        //}

        //private static bool VerifyTimerType(PersonalTask personalTask)
        //{
        //    if (personalTask.fri || personalTask.mon || personalTask.sat || personalTask.sun || personalTask.thu || personalTask.tue || personalTask.wed)
        //    {
        //        return true;
        //    }

        //    return false;
        }
    }
}