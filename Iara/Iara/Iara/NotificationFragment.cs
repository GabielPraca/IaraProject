using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Iara
{
    class NotificationFragment : DialogFragment
    {
        private Button btnCancelAlarm;
        private TextView txtTitle;
        private TextView txtExtra;



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.NotificationFragment, container, false);

            btnCancelAlarm = view.FindViewById<Button>(Resource.Id.btnCancelAlarm);
            btnCancelAlarm.Click += btnCancelAlarm_Click;

            txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
            txtExtra = view.FindViewById<TextView>(Resource.Id.txtExtra);

            txtTitle.Text = TaskTransporter.itens[0];
            txtExtra.Text = TaskTransporter.itens[1];

            return view;
        }

        private void btnCancelAlarm_Click(object sender, EventArgs e)
        {
            AlarmRingtone.StopRingtone(TaskTransporter.context);
            TaskTransporter.ResetTaskTransporter();
            Activity.FragmentManager.BeginTransaction().Remove(this).Commit();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }
    }
}