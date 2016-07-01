using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace HelpDeskMobile.AndroidApp
{
    internal class MySpinnerAdapter : BaseAdapter<SpinnerItem>
    {
        private List<SpinnerItem> list;

        public MySpinnerAdapter(List<SpinnerItem> list)
        {
            this.list = list;
        }

        public override SpinnerItem this[int position]
        {
            get
            {
                return list[position];
            }
        }

        public override int Count
        {
            get
            {
                return (list.Count == 0) ? 0 : list.Count - 1;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView textview = (TextView)LayoutInflater.FromContext(parent.Context)
                .Inflate(Resource.Layout.support_simple_spinner_dropdown_item, parent, false);
            textview.Text = list[position].Text;
            return textview;
        }
    }
}