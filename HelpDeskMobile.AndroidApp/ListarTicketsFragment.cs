using Android.Content;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using com.refractored;
using HelpDeskMobile.AndroidApp.Helpers;

namespace HelpDeskMobile.AndroidApp
{
    public class ListarTicketsFragment : Android.Support.V4.App.Fragment
    {
        private PagerSlidingTabStrip tabs;
        private ViewPager pager;
        private PagerListarTicketsAdapter adapter;
        private Context context;

        public ListarTicketsFragment(Context _context)
        {
            context = _context;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ListarTicketsLayout, container, false);

            tabs = view.FindViewById<PagerSlidingTabStrip>(Resource.Id.ListarTicketsTabs);
            pager = view.FindViewById<ViewPager>(Resource.Id.ListarTicketsViewPager);

            if (Global.IsNetworkAvailable(context))
            {
                adapter = new PagerListarTicketsAdapter(this.FragmentManager, context);
                pager.Adapter = adapter;
                pager.CurrentItem = 0;
                tabs.SetViewPager(pager);
            }
            else
            {
                Global.ShowAlert("Al parecer no estás conectado a Internet."
                                + "Por favor, verifique su conexión y vuelva a iniciar la aplicación.",
                                context);
            }

            return view;
        }
    }
}