using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace Iara
{
    [Activity(Label = "Registre uma Tarefa", ParentActivity = typeof(RootActivity),
              ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]

    public class RegisterTaskActivity : Activity
    {
        LinearLayout componentWeek;
        LinearLayout componentMonth;

        Button btnWeek;
        Button btnMonth;
        Button btnSaveTask;

        TextView txtTime;
        TextView txtMonth;

        EditText edtDesc;

        CheckBox ckSun;
        CheckBox ckMon;
        CheckBox ckTue;
        CheckBox ckWed;
        CheckBox ckThu;
        CheckBox ckFri;
        CheckBox ckSat;
        CheckBox ckRep;

        DateTime dateSelected;
        TimeSpan timeSelected;

        bool weekly = true;

        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        eTaskType actualTaskType = eTaskType.week;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterTask);

            componentMonth = (LinearLayout)FindViewById(Resource.Id.ComponentMonth);
            componentWeek = (LinearLayout)FindViewById(Resource.Id.ComponentWeek);

            componentWeek.Visibility = ViewStates.Visible;

            edtDesc = (EditText)FindViewById(Resource.Id.edtDesc);

            ckRep = (CheckBox)FindViewById(Resource.Id.ckRep);

            ckSun = (CheckBox)FindViewById(Resource.Id.ckSun);
            ckMon = (CheckBox)FindViewById(Resource.Id.ckMon);
            ckTue = (CheckBox)FindViewById(Resource.Id.ckTue);
            ckWed = (CheckBox)FindViewById(Resource.Id.ckWed);
            ckThu = (CheckBox)FindViewById(Resource.Id.ckThu);
            ckFri = (CheckBox)FindViewById(Resource.Id.ckFri);
            ckSat = (CheckBox)FindViewById(Resource.Id.ckSat);
            InitializeCheckBox();

            btnWeek = (Button)FindViewById(Resource.Id.btnWeek);
            btnWeek.Click += btnWeek_Click;

            btnMonth = (Button)FindViewById(Resource.Id.btnMonth);
            btnMonth.Click += btnMonth_Click;

            btnSaveTask = (Button)FindViewById(Resource.Id.btnSaveTask);
            btnSaveTask.Click += btnSaveTask_Click;

            txtTime = (TextView)FindViewById(Resource.Id.txtTime);
            txtTime.Text = String.Concat(DateTime.Now.Hour.ToString("00"), ":", DateTime.Now.Minute.ToString("00"));
            timeSelected = DateTime.Now.TimeOfDay;
            txtTime.Click += btnTimePicker_Click;

            txtMonth = (TextView)FindViewById(Resource.Id.txtMonth);
            txtMonth.Text = DateTime.Now.ToShortDateString();
            dateSelected = DateTime.Now;
            txtMonth.Click += txtMonth_Click;
        }

        private void btnSaveTask_Click(object sender, EventArgs e)
        {
            SQLiteModels.PersonalTask personalTask = new SQLiteModels.PersonalTask();

            if (actualTaskType == eTaskType.week && ValidateTask())
            {
                int day = 0;

                //personalTask.sun = ckSun.Checked;
                if (ckSun.Checked)
                {
                    personalTask.sun = ckSun.Checked;
                    if(day == 0)
                    {
                        day = DateTime.Now.Day + GetNextWeekday(DayOfWeek.Sunday);
                    }
                }
                //personalTask.mon = ckMon.Checked;
                if (ckMon.Checked)
                {
                    personalTask.mon = ckMon.Checked;
                    if (day == 0)
                    {
                        day = DateTime.Now.Day + GetNextWeekday(DayOfWeek.Monday);
                    }
                }
                //personalTask.tue = ckTue.Checked;
                if (ckTue.Checked)
                {
                    personalTask.tue = ckTue.Checked;
                    if (day == 0)
                    {
                        day = DateTime.Now.Day + GetNextWeekday(DayOfWeek.Tuesday);
                    }
                }
                //personalTask.wed = ckWed.Checked;
                if (ckWed.Checked)
                {
                    personalTask.wed = ckWed.Checked;
                    if (day == 0)
                    {
                        day = DateTime.Now.Day + GetNextWeekday(DayOfWeek.Wednesday);
                    }
                }
                //personalTask.thu = ckThu.Checked;
                if (ckThu.Checked)
                {
                    personalTask.thu = ckThu.Checked;
                    if (day == 0)
                    {
                        day = DateTime.Now.Day + GetNextWeekday(DayOfWeek.Thursday);
                    }
                }
                //personalTask.fri = ckFri.Checked;
                if (ckFri.Checked)
                {
                    personalTask.fri = ckFri.Checked;
                    if (day == 0)
                    {
                        day = DateTime.Now.Day + GetNextWeekday(DayOfWeek.Friday);
                    }
                }
                //personalTask.sat = ckSat.Checked;
                if (ckSat.Checked)
                {
                    personalTask.sat = ckSat.Checked;
                    if (day == 0)
                    {
                        day = DateTime.Now.Day + GetNextWeekday(DayOfWeek.Saturday);
                    }
                }

                dateSelected = new DateTime(DateTime.Now.Year,
                                            DateTime.Now.Month,
                                            day,
                                            timeSelected.Hours,
                                            timeSelected.Minutes,
                                            timeSelected.Seconds);

                personalTask.description = edtDesc.Text;
                personalTask.email = Config.loggedUser.email;
                personalTask.repeat = ckRep.Checked;
                personalTask.taskDay = dateSelected;
            }
            if (actualTaskType == eTaskType.month)
            {
                dateSelected = new DateTime(dateSelected.Year,
                                            dateSelected.Month,
                                            dateSelected.Day,
                                            timeSelected.Hours,
                                            timeSelected.Minutes,
                                            timeSelected.Seconds);
                personalTask.sun = false;
                personalTask.mon = false;
                personalTask.tue = false;
                personalTask.wed = false;
                personalTask.thu = false;
                personalTask.fri = false;
                personalTask.sat = false;
                personalTask.description = edtDesc.Text;
                personalTask.email = Config.loggedUser.email;
                personalTask.repeat = ckRep.Checked;
                personalTask.taskDay = dateSelected;
            }

            DatabaseManager.BODatabaseManager.CreatePersonalTask(personalTask);

            StartAlarm(false, personalTask);
            Toast.MakeText(this.ApplicationContext, "Salvo", ToastLength.Short).Show();
            Finish();
        }
        public int GetNextWeekday(DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            return ((int)day - (int)DateTime.Now.DayOfWeek + 7) % 7;
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            weekly = false;
            ckRep.Visibility = ViewStates.Visible;
            actualTaskType = eTaskType.month;
            componentMonth.Visibility = ViewStates.Visible;
            componentWeek.Visibility = ViewStates.Gone;
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            weekly = true;
            ckRep.Checked = true;
            ckRep.Visibility = ViewStates.Invisible;
            actualTaskType = eTaskType.week;
            componentWeek.Visibility = ViewStates.Visible;
            componentMonth.Visibility = ViewStates.Gone;
        }

        private void btnTimePicker_Click(object sender, EventArgs e)
        {
            ClockFragment frag = ClockFragment.NewInstance(delegate (TimeSpan time)
            {
                timeSelected = time;
                txtTime.Text = String.Concat(time.Hours.ToString("00"), ":", time.Minutes.ToString("00"));
            });
            frag.Show(FragmentManager, ClockFragment.TAG);
        }

        private void txtMonth_Click(object sender, EventArgs e)
        {
            CalendarFragment frag = CalendarFragment.NewInstance(delegate (DateTime time)
            {
                dateSelected = time;
                txtMonth.Text = time.ToShortDateString();
            });

            frag.Show(FragmentManager, CalendarFragment.TAG);
        }

        private void InitializeCheckBox()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    ckSun.Checked = true;
                    break;
                case DayOfWeek.Monday:
                    ckMon.Checked = true;
                    break;
                case DayOfWeek.Tuesday:
                    ckTue.Checked = true;
                    break;
                case DayOfWeek.Wednesday:
                    ckWed.Checked = true;
                    break;
                case DayOfWeek.Thursday:
                    ckThu.Checked = true;
                    break;
                case DayOfWeek.Friday:
                    ckFri.Checked = true;
                    break;
                case DayOfWeek.Saturday:
                    ckSat.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private bool ValidateTask()
        {
            if (actualTaskType == eTaskType.week)
            {
                bool hasDayChecked = false;

                if (ckSun.Checked)
                    hasDayChecked = true;
                if (ckMon.Checked)
                    hasDayChecked = true;
                if (ckThu.Checked)
                    hasDayChecked = true;
                if (ckFri.Checked)
                    hasDayChecked = true;
                if (ckSat.Checked)
                    hasDayChecked = true;
                if (ckTue.Checked)
                    hasDayChecked = true;
                if (ckWed.Checked)
                    hasDayChecked = true;

                if (!hasDayChecked)
                {
                    Toast.MakeText(this, "Selecione um dia da semana!", ToastLength.Short).Show();
                    return false;
                }
            }

            return true;
        }

        private void StartAlarm(bool isRepeating, SQLiteModels.PersonalTask personalTask)
        {
            if(personalTask.taskDay > DateTime.Now)
            {
                AlarmManager alarm = (AlarmManager)GetSystemService(Context.AlarmService);
                Config.alarm = alarm;
                Intent myIntent;
                PendingIntent pendingIntent;

                myIntent = new Intent(Application.Context, typeof(AlarmNotificationReceiver));

                myIntent.PutStringArrayListExtra("task", BuildtaskItens(personalTask).ToArray<string>());
                myIntent.SetAction(TimeMillis(DateTime.Now).ToString());
                pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, myIntent, PendingIntentFlags.UpdateCurrent);

                SetAlarm(alarm, TimeMillis(personalTask), pendingIntent);
            }
            #region OLD
            //if (actualTaskType == eTaskType.month)
            //{
            //    DateTime utcAlarmTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(personalTask.taskDay.Ticks, DateTimeKind.Local));
            //    long timeMillis = (long)(utcAlarmTime - Jan1st1970).TotalMilliseconds;

            //    SetAlarm(alarm, timeMillis, pendingIntent);
            //}
            //else
            //{
            //    long alarmTime = 0;
            //    if (ckSun.Checked)
            //    {
            //        alarmTime = BuildCalendar(DayOfWeek.Sunday, personalTask).TimeInMillis;
            //        SetAlarm(alarm, alarmTime, pendingIntent);
            //    }
            //    if (ckMon.Checked)
            //    {
            //        alarmTime = BuildCalendar(DayOfWeek.Monday, personalTask).TimeInMillis;
            //        SetAlarm(alarm, alarmTime, pendingIntent);
            //    }
            //    if (ckTue.Checked)
            //    {
            //        alarmTime = BuildCalendar(DayOfWeek.Tuesday, personalTask).TimeInMillis;
            //        SetAlarm(alarm, alarmTime, pendingIntent);
            //    }
            //    if (ckWed.Checked)
            //    {
            //        alarmTime = BuildCalendar(DayOfWeek.Wednesday, personalTask).TimeInMillis;
            //        SetAlarm(alarm, alarmTime, pendingIntent);
            //    }
            //    if (ckThu.Checked)
            //    {
            //        alarmTime = BuildCalendar(DayOfWeek.Thursday, personalTask).TimeInMillis;
            //        SetAlarm(alarm, alarmTime, pendingIntent);
            //    }
            //    if (ckFri.Checked)
            //    {
            //        alarmTime = BuildCalendar(DayOfWeek.Friday, personalTask).TimeInMillis;
            //        SetAlarm(alarm, alarmTime, pendingIntent);
            //    }
            //    if (ckSat.Checked)
            //    {
            //        alarmTime = BuildCalendar(DayOfWeek.Saturday, personalTask).TimeInMillis;
            //        SetAlarm(alarm, alarmTime, pendingIntent);
            //    }
            //}
            #endregion
        }

        private void SetAlarm(AlarmManager alarm, long alarmTime, PendingIntent pendingIntent)
        {
            if (ckRep.Checked && weekly)
            {
                alarm.SetRepeating(AlarmType.RtcWakeup, alarmTime, AlarmManager.IntervalDay * 7, pendingIntent);
            }
            else
            {
                alarm.Set(AlarmType.RtcWakeup, alarmTime, pendingIntent);
            }
        }

        private long TimeMillis(SQLiteModels.PersonalTask personalTask)
        {
            DateTime utcAlarmTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(personalTask.taskDay.Ticks, DateTimeKind.Local));
            long timeMillis = (long)(utcAlarmTime - Jan1st1970).TotalMilliseconds;

            return timeMillis;
        }
        private long TimeMillis(DateTime dt)
        {
            DateTime utcAlarmTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(dt.Ticks, DateTimeKind.Local));
            long timeMillis = (long)(utcAlarmTime - Jan1st1970).TotalMilliseconds;

            return timeMillis;
        }

        //private Calendar BuildCalendar(DayOfWeek dayOfWeek, SQLiteModels.PersonalTask personalTask)
        //{
        //    DateTime utcAlarmTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(personalTask.taskDay.Ticks, DateTimeKind.Local));
        //    long timeMillis = (long)(utcAlarmTime - Jan1st1970).TotalMilliseconds;

        //    Calendar alarmCalendar = Calendar.Instance;
        //    if (personalTask.taskDay.Hour >= 0 && personalTask.taskDay.Hour <= 12)
        //    {
        //        alarmCalendar.Set(CalendarField.AmPm, Calendar.Am);
        //    }
        //    else
        //    {
        //        alarmCalendar.Set(CalendarField.AmPm, Calendar.Pm);
        //    }
        //    alarmCalendar.Set(CalendarField.DayOfWeek, (int)dayOfWeek);
        //    //alarmCalendar.Set(CalendarField.DayOfMonth, DateTime.Now.Day + GetNextWeekday(dayOfWeek));
        //    //DateTime date = Convert.ToDateTime(personalTask.taskDay.ToString("dd/MM/yyyy, hh:mm:ss tt"));
        //    alarmCalendar.TimeInMillis  = System.CurrentTimeMillis();
        //    alarmCalendar.Set(CalendarField.HourOfDay, personalTask.taskDay.Hour);
        //    alarmCalendar.Set(CalendarField.Minute, personalTask.taskDay.Minute);
        //    //alarmCalendar.Set(CalendarField.Second, 0);

        //    return alarmCalendar;
        //}

        private List<string> BuildtaskItens(SQLiteModels.PersonalTask personalTask)
        {
            List<string> ret = new List<string>();
            if (personalTask != null)
            {
                ret.Add(personalTask.description);
                ret.Add(String.Concat(personalTask.taskDay.Hour.ToString("00"), ":", personalTask.taskDay.Minute.ToString("00")));
            }
            return ret;
        }
    }
}