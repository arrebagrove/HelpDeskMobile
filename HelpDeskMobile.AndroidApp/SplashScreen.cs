using Android.App;
using Android.Content;
using Android.OS;
using HelpDeskMobile.AndroidApp.Helpers;

namespace HelpDeskMobile.AndroidApp
{
    [Activity(Label = "HelpDesk", MainLauncher = true, NoHistory = true, Theme = "@style/ThemeSplash")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Global.CreateGlobalSession(this);
            if (!Global.GlobalUserSessionInstance().isUserLoggedIn())
            {
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);
            }
            else
            {
                Intent i = new Intent(this, typeof(Shell));
                StartActivity(i);
            }
        }
    }
}