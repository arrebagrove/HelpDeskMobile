using Android.App;
using Android.Content;
using Android.Net;

namespace HelpDeskMobile.AndroidApp.Helpers
{
    public class Global
    {
        private static UserSessionManager session;

        public static void CreateGlobalSession(Context context)
        {
            session = new UserSessionManager(context);
        }

        public static UserSessionManager GlobalUserSessionInstance()
        {
            return session;
        }

        public static bool IsNetworkAvailable(Context context)
        {
            ConnectivityManager connectivityManager =
                (ConnectivityManager)context
                .ApplicationContext
                .GetSystemService(Context.ConnectivityService);
            NetworkInfo activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            return activeNetworkInfo != null && activeNetworkInfo.IsConnected;
        }

        public static void ShowAlert(string message, Context context)
        {
            var alerta = new AlertDialog.Builder(context);
            alerta.SetTitle("Lo sentimos :(");
            alerta.SetMessage(message);
            alerta.SetPositiveButton("Entendido", (senderAlert, args) =>
             {
                 alerta.Dispose();
                 ((Activity)context).Finish();
                 System.Environment.Exit(0);
             });
            alerta.Show();
        }
    }
}