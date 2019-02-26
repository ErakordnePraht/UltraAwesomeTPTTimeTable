using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TPTtimetable
{
    class ListAdapter : BaseAdapter<Tund>
    {
        public List<Tund> items;


        Activity context;

        public ListAdapter(Activity context, List<Tund> items)
        {
            this.context = context;
            this.items = items;
        }


        public override Tund this[int position]
        {
            get { return items[position]; }
        }

        public override int Count { get { return items.Count; } }


        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.experimental, null);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = items[position].lessonname;
            view.FindViewById<TextView>(Resource.Id.textView2).Text = items[position].classname;
            view.FindViewById<TextView>(Resource.Id.textView3).Text = items[position].teachername;
            return view;

        }

    }

}