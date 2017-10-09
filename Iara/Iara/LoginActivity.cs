using Android.App;
using Android.Widget;
using Android.OS;
using System;
using IaraWrapper;
using System.Collections.Generic;

namespace Iara
{
    [Activity(Label = "Iara", MainLauncher = true, Icon = "@drawable/icon",
              ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]

    public class MainActivity : Activity
    {
        private TextView txtRegister;
        private Button btnLogin;
        private EditText edtEmail;
        private EditText edtPass;

        protected override void OnCreate(Bundle bundle)
        {
            Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Login);

            txtRegister = FindViewById<TextView>(Resource.Id.txtRegister);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            edtPass = FindViewById<EditText>(Resource.Id.edtPass);

            txtRegister.Click += txtRegister_Click;
            btnLogin.Click += btnLogin_Click;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string authAnswer = DatabaseManager.BODatabaseManager.UserAuthentication(edtEmail.Text, edtPass.Text);
            if (authAnswer == DatabaseManager.DatabaseAnswer.Sucess.ToString())
            {
                Config.loggedUser = DatabaseManager.BODatabaseManager.GetUser(edtEmail.Text);
                StartActivity(typeof(RootActivity));
                Finish();
            }
            else
            {
                IaraWrapper.IaraWrapper iw = new IaraWrapper.IaraWrapper(edtEmail.Text, edtPass.Text);

                if (iw.IsAuthenticated())
                {
                    //pega o usuário existente
                    IaraModels.User userToAdd = iw.GetUser(edtEmail.Text);
                    //Converte para o modelo do SQLite
                    SQLiteModels.User user = new SQLiteModels.User();
                    user.email = userToAdd.email;
                    user.userName = userToAdd.userName;
                    user.password = userToAdd.password;
                    user.updtDTime = userToAdd.updtDTime;
                    user.synchronizedInToMobile = userToAdd.synchronizedInToMobile;
                    user.synchronizedInToServer = userToAdd.synchronizedInToServer;
                    //Salva no SQLite
                    DatabaseManager.BODatabaseManager.CreateUser(user);

                    //Pega as tarefas para adicionar
                    List<IaraModels.PersonalTask> ptsToAdd = iw.GetAllActivePersonalTasks(edtEmail.Text);

                    foreach(var pt in ptsToAdd)
                    {
                        SQLiteModels.PersonalTask ptToAdd = new SQLiteModels.PersonalTask()
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
                        };
                        DatabaseManager.BODatabaseManager.CreatePersonalTask(ptToAdd);
                    }

                    Config.loggedUser = DatabaseManager.BODatabaseManager.GetUser(edtEmail.Text);
                    StartActivity(typeof(RootActivity));
                    Finish();
                }
                else
                {
                    Toast.MakeText(ApplicationContext, authAnswer, ToastLength.Short).Show();
                }
            }           
        }

        private void txtRegister_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            RegisterFragment rf = new RegisterFragment();
            rf.Show(transaction, "Register Fragment");
        }
    }
}

