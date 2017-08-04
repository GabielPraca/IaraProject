using Android.App;
using Android.Widget;
using Android.OS;
using System;

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
                Toast.MakeText(ApplicationContext, authAnswer, ToastLength.Short).Show();
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

