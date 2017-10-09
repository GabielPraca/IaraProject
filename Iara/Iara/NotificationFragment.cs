using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Linq;
using DatabaseManager;
using SQLiteModels;

namespace Iara
{
    class NotificationFragment : DialogFragment
    {
        private Button btnCancelAlarm;
        private Button btnPending;
        private TextView txtTitle;
        private TextView txtExtra;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            PersonalTask pt = BODatabaseManager.GetAllActivePersonalTasks(
            Config.loggedUser.email).Where(p => p.description == TaskTransporter.itens[0] &&
            String.Concat(p.taskDay.Hour.ToString("00"), ":", p.taskDay.Minute.ToString("00")) == TaskTransporter.itens[1]).FirstOrDefault();

            if (pt != null && !pt.finalized)
            {
                base.OnCreateView(inflater, container, savedInstanceState);

                var view = inflater.Inflate(Resource.Layout.NotificationFragment, container, true);

                btnCancelAlarm = view.FindViewById<Button>(Resource.Id.btnCancelAlarm);
                btnCancelAlarm.Click += btnCancelAlarm_Click;
                btnPending = view.FindViewById<Button>(Resource.Id.btnPending);
                btnPending.Click += btnPending_Click;

                txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
                txtExtra = view.FindViewById<TextView>(Resource.Id.txtExtra);

                txtTitle.Text = TaskTransporter.itens[0];
                txtExtra.Text = TaskTransporter.itens[1];

                return view;
            }
            return null;
        }

        private void btnPending_Click(object sender, EventArgs e)
        {
            AlarmRingtone.StopRingtone(Application.Context);
            TaskTransporter.ResetTaskTransporter();

            Activity.FragmentManager.BeginTransaction().Remove(this).Commit();
        }

        private void btnCancelAlarm_Click(object sender, EventArgs e)
        {
            //18-09-2017 AlarmRingtone.StopRingtone(TaskTransporter.context);
            AlarmRingtone.StopRingtone(Application.Context);
            FinishTask();
            TaskTransporter.ResetTaskTransporter();

            Activity.FragmentManager.BeginTransaction().Remove(this).Commit();
        }

        private void FinishTask()
        {
            SQLiteModels.PersonalTask pt = BODatabaseManager.GetAllActivePersonalTasks(
                   Config.loggedUser.email).Where(p => p.description == TaskTransporter.itens[0] &&
                   String.Concat(p.taskDay.Hour.ToString("00"), ":", p.taskDay.Minute.ToString("00")) == TaskTransporter.itens[1]).FirstOrDefault();

            if (pt != null && !pt.repeat)
            {
                pt.finalized = true;
                pt.synchronizedInToServer = false;
                BODatabaseManager.UpdatePersonalTask(pt);
            }
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }
    }
}