using Android.Content;
using Android.Support.V4.App;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using String = Java.Lang.String;

namespace HelpDeskMobile.AndroidApp
{
    internal class PagerListarTicketsAdapter : FragmentPagerAdapter
    {
        private readonly string[] titles =
        {
            "Pendientes",
            "Atendidos",
            "Vencidos",
            "Cancelados"
        };

        private Context _context;

        public PagerListarTicketsAdapter(FragmentManager fm, Context context) : base(fm)
        {
            _context = context;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new String(titles[position]);
        }

        public override int Count
        {
            get
            {
                return titles.Length;
            }
        }

        public override Fragment GetItem(int position)
        {
            return new TicketsFragment(position, _context);
        }
    }
}