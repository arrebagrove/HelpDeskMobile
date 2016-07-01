using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using HelpDeskMobile.AndroidApp.Helpers;
using System.Collections.Generic;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace HelpDeskMobile.AndroidApp
{
    [Activity(Label = "Shell", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, ConfigurationChanges = Android.Content.PM.ConfigChanges.LayoutDirection)]
    public class Shell : AppCompatActivity
    {
        private UserSessionManager session;

        private DrawerLayout drawerLayout;
        private MyActionBarDrawerToggle drawerToggle;
        private List<string> listItems = new List<string>();
        private ArrayAdapter leftAdapter;
        private ListView leftDrawer;
        private SupportToolbar toolbar;
        private RelativeLayout frame;
        private Android.Support.V4.App.FragmentManager fr;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ShellLayout);

            session = new UserSessionManager(this);
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.ShellDrawerLayout);
            leftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            frame = FindViewById<RelativeLayout>(Resource.Id.ShellContentFrame);

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "HelpDesk";

            //  Left drawer
            listItems.Add("Lista de tickets");
            listItems.Add("Registrar tickets");
            leftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, listItems);
            leftDrawer.Adapter = leftAdapter;
            leftDrawer.ItemClick += LeftDrawer_ItemClick;

            //  Left drawer icon
            drawerToggle = new MyActionBarDrawerToggle(
                this,                           //Host Activity
                drawerLayout,                   //DrawerLayout
                Resource.String.openDrawer,     //Opened Message
                Resource.String.closeDrawer     //Closed Message
            );
            drawerLayout.SetDrawerListener(drawerToggle);

            //  Show left drawer icon
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            drawerToggle.SyncState();

            //  Select first item (RegistrarTicket)
            fr = SupportFragmentManager;
            SetFragment(new RegistrarTicketFragment(this), null);
        }

        private void LeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            switch (e.Position)
            {
                case 0:
                    SetFragment(new ListarTicketsFragment(this), e);
                    break;

                case 1:
                    SetFragment(new RegistrarTicketFragment(this), e);
                    break;

                default: break;
            }
        }

        private void SetFragment(Android.Support.V4.App.Fragment fragment, AdapterView.ItemClickEventArgs e)
        {
            drawerLayout.CloseDrawer(leftDrawer);
            fr.BeginTransaction().Replace(Resource.Id.ShellContentFrame, fragment).Commit();
            leftDrawer.SetItemChecked((e != null) ? e.Position : 1, true);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (drawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }
            else
            {
                outState.PutString("DrawerState", "Closed");
            }

            base.OnSaveInstanceState(outState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            drawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            drawerToggle.OnConfigurationChanged(newConfig);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    //The hamburger icon was clicked which means the drawer toggle will handle the event
                    //all we need to do is ensure the right drawer is closed so the don't overlap
                    drawerToggle.OnOptionsItemSelected(item);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}