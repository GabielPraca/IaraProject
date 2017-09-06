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
                dateSelected = new DateTime(DateTime.Now.Year,
                                            DateTime.Now.Month,
                                            DateTime.Now.Day,
                                            timeSelected.Hours,
                                            timeSelected.Minutes,
                                            timeSelected.Seconds);
                personalTask.sun = ckSun.Checked;
                personalTask.mon = ckMon.Checked;
                personalTask.tue = ckTue.Checked;
                personalTask.wed = ckWed.Checked;
                personalTask.thu = ckThu.Checked;
                personalTask.fri = ckFri.Checked;
                personalTask.sat = ckSat.Checked;

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
                personalTask.repeat = false;
                personalTask.taskDay = dateSelected;
            }

            DatabaseManager.BODatabaseManager.CreatePersonalTask(personalTask);

            StartAlarm(false, personalTask);
            Toast.MakeText(this.ApplicationContext, "Salvo", ToastLength.Short).Show();
            Finish();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            weekly = false;
            actualTaskType = eTaskType.month;
            componentMonth.Visibility = ViewStates.Visible;
            componentWeek.Visibility = ViewStates.Gone;
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            weekly = true;
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
            AlarmManager alarm = (AlarmManager)GetSystemService(Context.AlarmService);
            Intent myIntent;
            PendingIntent pendingIntent;

            myIntent = new Intent(Application.Context, typeof(AlarmNotificationReceiver));

            myIntent.PutStringArrayListExtra("task", BuildtaskItens(personalTask).ToArray<string>());
            pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, myIntent, PendingIntentFlags.UpdateCurrent);

            if (actualTaskType == eTaskType.month)
            {
                DateTime utcAlarmTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(personalTask.taskDay.Ticks, DateTimeKind.Local));
                long timeMillis = (long)(utcAlarmTime - Jan1st1970).TotalMilliseconds;

                SetAlarm(alarm, timeMillis, pendingIntent);
            }
            else
            {
                long alarmTime = 0;
                if (ckSun.Checked)
                {
                    alarmTime = BuildCalendar(Calendar.Sunday, personalTask).TimeInMillis;
                    SetAlarm(alarm, alarmTime, pendingIntent);
                }
                if (ckMon.Checked)
                {
                    alarmTime = BuildCalendar(Calendar.Monday, personalTask).TimeInMillis;
                    SetAlarm(alarm, alarmTime, pendingIntent);
                }
                if (ckTue.Checked)
                {
                    alarmTime = BuildCalendar(Calendar.Tuesday, personalTask).TimeInMillis;
                    SetAlarm(alarm, alarmTime, pendingIntent);
                }
                if (ckWed.Checked)
                {
                    alarmTime = BuildCalendar(Calendar.Wednesday, personalTask).TimeInMillis;
                    SetAlarm(alarm, alarmTime, pendingIntent);
                }
                if (ckThu.Checked)
                {
                    alarmTime = BuildCalendar(Calendar.Thursday, personalTask).TimeInMillis;
                    SetAlarm(alarm, alarmTime, pendingIntent);
                }
                if (ckFri.Checked)
                {
                    alarmTime = BuildCalendar(Calendar.Friday, personalTask).TimeInMillis;
                    SetAlarm(alarm, alarmTime, pendingIntent);
                }
                if (ckSat.Checked)
                {
                    alarmTime = BuildCalendar(Calendar.Saturday, personalTask).TimeInMillis;
                    SetAlarm(alarm, alarmTime, pendingIntent);
                }
            }
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

        private Calendar BuildCalendar(int dayOfWeek, SQLiteModels.PersonalTask personalTask)
        {
            Calendar alarmCalendar = Calendar.Instance;
            if (personalTask.taskDay.Hour >= 0 && personalTask.taskDay.Hour <= 12)
            {
                alarmCalendar.Set(CalendarField.AmPm, Calendar.Am);
            }
            else
            {
                alarmCalendar.Set(CalendarField.AmPm, Calendar.Pm);
            }
            alarmCalendar.Set(CalendarField.DayOfWeek, dayOfWeek-1);
            alarmCalendar.Set(CalendarField.Hour, personalTask.taskDay.Hour);
            alarmCalendar.Set(CalendarField.Minute, personalTask.taskDay.Minute);
            alarmCalendar.Set(CalendarField.Second, 0);

            return alarmCalendar;
        }

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