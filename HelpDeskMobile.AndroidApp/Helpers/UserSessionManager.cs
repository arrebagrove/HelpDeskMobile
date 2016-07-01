using Android.Content;
using System;
using System.Collections.Generic;

namespace HelpDeskMobile.AndroidApp.Helpers
{
    public class UserSessionManager
    {
        private ISharedPreferences pref;
        private ISharedPreferencesEditor editor;
        private Context _context;

        //Constants
        private const String PREFER_NAME = "XamarinBlinkPref";

        private const String IS_USER_LOGIN = "IsUserLoggedIn";
        public const String KEY_USERNAME = "username";
        public const String KEY_PASSWORD = "password";
        public const String KEY_EMAIL = "email";
        public const String KEY_ID = "id";
        public const String KEY_NOMBRE_EMPRESA = "empresa";
        public const String KEY_NOMBRE_CLIENTE = "cliente";

        // Constructor
        public UserSessionManager(Context context)
        {
            this._context = context;
            pref = _context.GetSharedPreferences(PREFER_NAME, FileCreationMode.Private);
            editor = pref.Edit();
        }

        public void createUserLoginSession(String mail, String pass, String id, String correo, String empresa, String nombre)
        {
            editor.PutBoolean(IS_USER_LOGIN, true);
            editor.PutString(KEY_USERNAME, mail);
            editor.PutString(KEY_PASSWORD, pass);
            editor.PutString(KEY_ID, id);
            editor.PutString(KEY_EMAIL, correo);
            editor.PutString(KEY_NOMBRE_EMPRESA, empresa);
            editor.PutString(KEY_NOMBRE_CLIENTE, nombre);
            editor.Commit();
        }

        public bool checkLogin()
        {
            if (!this.isUserLoggedIn())
            {
                Intent i = new Intent(_context, typeof(LoginActivity));
                i.AddFlags(ActivityFlags.ClearTop);
                i.SetFlags(ActivityFlags.NewTask);
                _context.StartActivity(i);
                return true;
            }
            return false;
        }

        public Dictionary<String, String> getUserDetails()
        {
            Dictionary<String, String> user = new Dictionary<String, String>();
            user[KEY_USERNAME] = pref.GetString(KEY_USERNAME, null);
            user[KEY_PASSWORD] = pref.GetString(KEY_PASSWORD, null);
            user[KEY_EMAIL] = pref.GetString(KEY_EMAIL, null);
            user[KEY_ID] = pref.GetString(KEY_ID, null);
            user[KEY_NOMBRE_EMPRESA] = pref.GetString(KEY_NOMBRE_EMPRESA, null);
            user[KEY_NOMBRE_CLIENTE] = pref.GetString(KEY_NOMBRE_CLIENTE, null);
            return user;
        }

        public void logoutUser()
        {
            editor.Clear();
            editor.Commit();

            Intent i = new Intent(_context, typeof(LoginActivity));
            i.AddFlags(ActivityFlags.ClearTop);
            i.SetFlags(ActivityFlags.NewTask);
            _context.StartActivity(i);
        }

        public bool isUserLoggedIn()
        {
            return pref.GetBoolean(IS_USER_LOGIN, false);
        }
    }
}