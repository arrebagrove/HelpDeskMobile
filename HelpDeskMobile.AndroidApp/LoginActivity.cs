using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using HelpDeskMobile.AndroidApp.Helpers;
using HelpDeskMobile.Shared.Models;
using HelpDeskMobile.Shared.Services;
using System;

namespace HelpDeskMobile.AndroidApp
{
    [Activity(Label = "HelpDesk Mobile", Icon = "@drawable/icon", Theme = "@style/MyTheme", NoHistory = true)]
    public class LoginActivity : AppCompatActivity
    {
        private EditText username;
        private EditText password;
        private Button ingresarBtn;
        private Usuario user;
        private ProgressDialog waiter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.LoginLayout);

            username = FindViewById<EditText>(Resource.Id.LoginUsuarioTxt);
            password = FindViewById<EditText>(Resource.Id.LoginPassTxt);
            ingresarBtn = FindViewById<Button>(Resource.Id.LoginIngresarBtn);

            ingresarBtn.Click += IngresarBtn_Click;
        }

        private async void IngresarBtn_Click(object sender, EventArgs e)
        {
            waiter = ProgressDialog.Show(this, "Espera un momento", "No tomará mucho tiempo...");
            user = await WebServiceConnection.WSLogin(username.Text, password.Text);
            if (user.VerificarAccesoClienteResult.idUsuario != null)
            {
                Global.GlobalUserSessionInstance().createUserLoginSession(
                    username.Text,
                    password.Text,
                    user.VerificarAccesoClienteResult.id_Empresa.ToString(),
                    user.VerificarAccesoClienteResult.correo,
                    user.VerificarAccesoClienteResult.Empresa,
                    user.VerificarAccesoClienteResult.Nombres_Cliente);
                Intent i = new Intent(this, typeof(Shell));
                StartActivity(i);
            }
            else
            {
                Toast.MakeText(this, "Login Incorrecto", ToastLength.Short).Show();
            }
            waiter.Dismiss();
        }
    }
}