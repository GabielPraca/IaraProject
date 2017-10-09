
using System;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;
using Android.Views;
using Android.Content.PM;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using DatabaseManager;
using System.Threading;
using System.Timers;
using Android.Content;

namespace Iara
{
    [Activity(Label = "Tarefas Agendadas", LaunchMode = LaunchMode.SingleTop,
              ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]

    public class RootActivity : Activity
    {
        private DrawerLayout m_Drawer;
        private ListView m_DrawerList;
        private Android.Support.V7.App.ActionBarDrawerToggle m_Toggle;
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        List<SQLiteModels.PersonalTask> mPersonalTasks;

        private static readonly string[] menuItems = new[]
        {
            "Home", "Tarefas", "Sair"
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Root);

            #region Configures the action bar

            m_Drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            m_DrawerList = FindViewById<ListView>(Resource.Id.left_drawer);

            m_DrawerList.Adapter = new ArrayAdapter<string>(this, Resource.Layout.Item_Menu, menuItems);
            m_DrawerList.ItemClick += DrawerListOnItemClick;

            m_Toggle = new Android.Support.V7.App.ActionBarDrawerToggle(this, m_Drawer, Resource.String.Open, Resource.String.Close);

            m_Drawer.AddDrawerListener(m_Toggle);
            m_Toggle.SyncState();

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            #endregion

            #region RecyclerView

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.content_frame);
            mPersonalTasks = new List<SQLiteModels.PersonalTask>();

            mPersonalTasks = DatabaseManager.BODatabaseManager.GetAllActivePersonalTasks(Config.loggedUser.email);

            //layout manager
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerAdapter(mPersonalTasks);
            mRecyclerView.SetAdapter(mAdapter);
            #endregion

            InitializeWorkers();
        }

        private void InitializeWorkers()
        {
            #region Sync
            //Instancia do timer
            System.Timers.Timer tSync = new System.Timers.Timer();
            //chama o timer
            tSync.Elapsed += new ElapsedEventHandler(Workers.DatabaseWorker.Run);
            //1 min
            tSync.Interval = 10000;//100000;
            //Inicia o Timer       
            tSync.Enabled = true;
            #endregion
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            if (TaskTransporter.isActive)
            {
                //para garantir unica instancia do fragment
                TaskTransporter.isActive = false;
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                NotificationFragment nf = new NotificationFragment();
                nf.Show(transaction, "Notification Fragment");
            }

            //update itens of adapter
            RecyclerAdapter controlAdapter = (RecyclerAdapter)mRecyclerView.GetAdapter();
            controlAdapter.UpdateData(DatabaseManager.BODatabaseManager.GetAllActivePersonalTasks(Config.loggedUser.email));
        }

        private void DrawerListOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            switch (itemClickEventArgs.Position)
            {
                case 0:
                    //Iara.Workers.DatabaseWorker.Run();
                    break;
                case 1:
                    StartActivity(typeof(RegisterTaskActivity));
                    break;
                case 2:
                    System.Environment.Exit(0);
                    break;
            }

            //change the action bar of main layout
            m_DrawerList.SetItemChecked(itemClickEventArgs.Position, true);
            m_Drawer.CloseDrawer(m_DrawerList);
        }

        //Evento de click no menu
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (m_Toggle.OnOptionsItemSelected(item))
            {
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }

    public class RecyclerAdapter : RecyclerView.Adapter
    {
        private List<SQLiteModels.PersonalTask> mPersonalTasks = new List<SQLiteModels.PersonalTask>();

        public RecyclerAdapter(List<SQLiteModels.PersonalTask> personalTasks)
        {
            mPersonalTasks = personalTasks;
        }

        public class MyView : RecyclerView.ViewHolder
        {
            public View mMainView { get; set; }
            public TextView mDesc { get; set; }
            public TextView mHour { get; set; }
            public TextView mDate { get; set; }

            public TextView mTxtSun { get; set; }
            public TextView mTxtMon { get; set; }
            public TextView mTxtTue { get; set; }
            public TextView mTxtWed { get; set; }
            public TextView mTxtThu { get; set; }
            public TextView mTxtFri { get; set; }
            public TextView mTxtSat { get; set; }

            public Button mBtnDel { get; set; }
            public Button mBtnFinish { get; set; }

            public MyView(View view) : base(view)
            {
                mMainView = view;
            }
        }

        public static class TagButtonControl
        {
            public static int buttonCountDel = 0;
            public static int buttonCountFinish = 0;
        }
        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PersonalTask, parent, false);

            TextView txtDesc = row.FindViewById<TextView>(Resource.Id.txtDesc);
            TextView txtHour = row.FindViewById<TextView>(Resource.Id.txtHour);
            TextView txtDate = row.FindViewById<TextView>(Resource.Id.txtDate);

            TextView txtSun = row.FindViewById<TextView>(Resource.Id.txtSun);
            TextView txtMon = row.FindViewById<TextView>(Resource.Id.txtMon);
            TextView txtTue = row.FindViewById<TextView>(Resource.Id.txtTue);
            TextView txtWed = row.FindViewById<TextView>(Resource.Id.txtWed);
            TextView txtThu = row.FindViewById<TextView>(Resource.Id.txtThu);
            TextView txtFri = row.FindViewById<TextView>(Resource.Id.txtFri);
            TextView txtSat = row.FindViewById<TextView>(Resource.Id.txtSat);

            Button btnDel = row.FindViewById<Button>(Resource.Id.btnDel);
            Button btnFinish = row.FindViewById<Button>(Resource.Id.btnFinish);
            btnDel.Click += btnDel_Click;
            btnFinish.Click += btnFinish_Click;

            MyView view = new MyView(row)
            {
                mDesc = txtDesc,
                mHour = txtHour,
                mDate = txtDate,
                mTxtSun = txtSun,
                mTxtMon = txtMon,
                mTxtTue = txtTue,
                mTxtWed = txtWed,
                mTxtThu = txtThu,
                mTxtFri = txtFri,
                mTxtSat = txtSat,
                mBtnDel = btnDel,
                mBtnFinish = btnFinish
            };
            return view;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int pos = (int)(((Button)sender).GetTag(Resource.Id.btnDel));
            mPersonalTasks[pos].deleted = true;
            BODatabaseManager.UpdatePersonalTask(mPersonalTasks[pos]);
            UpdateData(BODatabaseManager.GetAllActivePersonalTasks(Config.loggedUser.email));

            AlarmRingtone.StopRingtone(Application.Context);
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            int pos = (int)(((Button)sender).GetTag(Resource.Id.btnFinish));
            mPersonalTasks[pos].finalized = true;
            mPersonalTasks[pos].synchronizedInToServer = false;
            BODatabaseManager.UpdatePersonalTask(mPersonalTasks[pos]);
            UpdateData(BODatabaseManager.GetAllActivePersonalTasks(Config.loggedUser.email));

            AlarmRingtone.StopRingtone(Application.Context);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyView myHolder = holder as MyView;
            //Finalizado
            if (mPersonalTasks[position].finalized)
            {
                myHolder.mMainView.SetBackgroundColor(Android.Graphics.Color.LightGreen);
            }
            //Atrasado
            else if (mPersonalTasks[position].taskDay < DateTime.Now && !mPersonalTasks[position].repeat)
            {
                myHolder.mMainView.SetBackgroundColor(Android.Graphics.Color.Salmon);
            }
            //No dia atual       
            else if (mPersonalTasks[position].taskDay.Day == DateTime.Now.Day &&
                         mPersonalTasks[position].taskDay.Month == DateTime.Now.Month &&
                         mPersonalTasks[position].taskDay.Year == DateTime.Now.Year)
            {
                myHolder.mMainView.SetBackgroundColor(Android.Graphics.Color.Yellow);
            }
            //Distante
            else
            {
                myHolder.mMainView.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);
            }
            
            //semanal repetição
            if (mPersonalTasks[position].taskDay < DateTime.Now && mPersonalTasks[position].repeat)
            {
                myHolder.mMainView.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);
            }

            myHolder.mDesc.Text = mPersonalTasks[position].description;
            myHolder.mHour.Text = String.Concat(mPersonalTasks[position].taskDay.Hour.ToString("00"), ":", mPersonalTasks[position].taskDay.Minute.ToString("00"));
            myHolder.mDate.Text = mPersonalTasks[position].taskDay.ToShortDateString();

            myHolder.mBtnDel.SetTag(Resource.Id.btnDel, TagButtonControl.buttonCountDel++);
            myHolder.mBtnFinish.SetTag(Resource.Id.btnFinish, TagButtonControl.buttonCountFinish++);

            bool weekly = false;

            if (mPersonalTasks[position].sun)
            {
                myHolder.mTxtSun.SetTextColor(Android.Graphics.Color.Black);
                weekly = true;
            }
            else
            {
                myHolder.mTxtSun.SetTextColor(Android.Graphics.Color.LightGray);
            }
            if (mPersonalTasks[position].mon)
            {
                myHolder.mTxtMon.SetTextColor(Android.Graphics.Color.Black);
                weekly = true;
            }
            else
            {
                myHolder.mTxtMon.SetTextColor(Android.Graphics.Color.LightGray);
            }
            if (mPersonalTasks[position].tue)
            {
                myHolder.mTxtTue.SetTextColor(Android.Graphics.Color.Black);
                weekly = true;
            }
            else
            {
                myHolder.mTxtTue.SetTextColor(Android.Graphics.Color.LightGray);
            }
            if (mPersonalTasks[position].wed)
            {
                myHolder.mTxtWed.SetTextColor(Android.Graphics.Color.Black);
                weekly = true;
            }
            else
            {
                myHolder.mTxtWed.SetTextColor(Android.Graphics.Color.LightGray);
            }
            if (mPersonalTasks[position].thu)
            {
                myHolder.mTxtThu.SetTextColor(Android.Graphics.Color.Black);
                weekly = true;
            }
            else
            {
                myHolder.mTxtThu.SetTextColor(Android.Graphics.Color.LightGray);
            }
            if (mPersonalTasks[position].fri)
            {
                myHolder.mTxtFri.SetTextColor(Android.Graphics.Color.Black);
                weekly = true;
            }
            else
            {
                myHolder.mTxtFri.SetTextColor(Android.Graphics.Color.LightGray);
            }
            if (mPersonalTasks[position].sat)
            {
                myHolder.mTxtSat.SetTextColor(Android.Graphics.Color.Black);
                weekly = true;
            }
            else
            {
                myHolder.mTxtSat.SetTextColor(Android.Graphics.Color.LightGray);
            }

            if (weekly)
            {
                myHolder.mDate.Text = "Semanal";
            }
        }

        public override int ItemCount
        {
            get { return mPersonalTasks.Count; }
        }

        public void UpdateData(List<SQLiteModels.PersonalTask> pTasks)
        {
            TagButtonControl.buttonCountDel = 0;
            TagButtonControl.buttonCountFinish = 0;
            mPersonalTasks.Clear();
            mPersonalTasks.AddRange(pTasks);
            NotifyDataSetChanged();
        }
    }
}