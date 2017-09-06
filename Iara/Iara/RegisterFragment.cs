using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Text.RegularExpressions;

namespace Iara
{
    class RegisterFragment : DialogFragment
    {
        private Button btnRegister;
        private EditText edtEmail;
        private EditText edtUser;
        private EditText edtPass;
        private EditText edtConfirmPass;
        private string emailRule = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.RegisterFragment, container, false);

            edtEmail = view.FindViewById<EditText>(Resource.Id.edtEmail);
            edtUser = view.FindViewById<EditText>(Resource.Id.edtUser);
            edtPass = view.FindViewById<EditText>(Resource.Id.edtPassword);
            edtConfirmPass = view.FindViewById<EditText>(Resource.Id.edtConfirmPassword);
            btnRegister = view.FindViewById<Button>(Resource.Id.btnRegister);
            btnRegister.Click += btnRegister_Click;

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            CreateUser();
            Activity.FragmentManager.BeginTransaction().Remove(this).Commit();
        }

        private void CreateUser()
        {
            if(!Regex.IsMatch(edtEmail.Text, emailRule))
            {
                Toast.MakeText(Activity.ApplicationContext, "Email Inválido", ToastLength.Short).Show();
            }

            else if(edtUser.Text == String.Empty || edtPass.Text == String.Empty)
            {
                Toast.MakeText(Activity.ApplicationContext, "Todos os campos são obrigatórios", ToastLength.Short).Show();
            }

            else if(edtPass.Text == edtConfirmPass.Text)
            {
                IaraWrapper.IaraWrapper iw = new IaraWrapper.IaraWrapper(String.Empty, String.Empty);

                SQLiteModels.User user = new SQLiteModels.User();
                user.email = edtEmail.Text;
                user.userName = edtUser.Text;
                user.password = edtPass.Text;
                user.updtDTime = DateTime.Now;
                user.synchronizedInToMobile = true;
                user.synchronizedInToServer = true;

                IaraModels.User bdUser = new IaraModels.User
                {
                    email = edtEmail.Text,
                    userName = edtUser.Text,
                    password = edtPass.Text,
                    updtDTime = DateTime.Now,
                    synchronizedInToMobile = true,
                    synchronizedInToServer = true
                };

                bool? res = iw.SaveUser(bdUser);
                if (res != null)
                {
                    if ((bool)res)
                    {
                        if (DatabaseManager.BODatabaseManager.CreateUser(user) != DatabaseManager.DatabaseAnswer.Sucess.ToString())
                        {
                            Toast.MakeText(Activity.ApplicationContext, "Usuário já existe", ToastLength.Short).Show();
                        }
                        else
                        {
                            Toast.MakeText(Activity.ApplicationContext, "Registrado com sucesso", ToastLength.Short).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(Activity.ApplicationContext, "Não foi possivel registrar, verifique sua conexão", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(Activity.ApplicationContext, "Usuário já existe", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(Activity.ApplicationContext, "Confirmação da senha inválida", ToastLength.Short).Show();
            }
        }
    }
}