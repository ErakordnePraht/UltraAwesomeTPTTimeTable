using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
        public Timer timer;

        TextView remainingTimerText;
        TextView untilTimeText;
        DateTime endTime;
        //DateTime startTime;
        //bool nextTimer = false;

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
            //Commented this out so that it doesn't re-use cells that are not in the view of the screen.
            //if (view == null)
            view = context.LayoutInflater.Inflate(Resource.Layout.experimental, null);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = items[position].lessonname;
            view.FindViewById<TextView>(Resource.Id.textView2).Text = items[position].classname;
            view.FindViewById<TextView>(Resource.Id.textView3).Text = items[position].teachername;
            view.FindViewById<TextView>(Resource.Id.textView4).Text = items[position].start.ToString("HH:mm");
            view.FindViewById<TextView>(Resource.Id.textView5).Text = items[position].end.ToString("HH:mm");
            view.FindViewById<TextView>(Resource.Id.textView6).Text = "";
            view.FindViewById<TextView>(Resource.Id.textView7).Text = "";

            var timeOfDay = DateTime.Now;
            if (timeOfDay > items[position].start && timeOfDay < items[position].end)
            {
                remainingTimerText = view.FindViewById<TextView>(Resource.Id.textView6);
                view.SetBackgroundColor(Android.Graphics.Color.ParseColor("#424242"));
                endTime = items[position].end;
                var remainingTime = endTime - timeOfDay;
                remainingTime = remainingTime + new TimeSpan(0, 1, 0);
                remainingTimerText.Text = "Tunni lõpuni: " + remainingTime.Minutes.ToString() + " min.";
                //nextTimer = false;
                TimerClass();
            }
            //if (timeOfDay < items[position].start && !(timeOfDay > items[position].start && timeOfDay < items[position].end) && nextTimer == false)
            //{
            //    startTime = items[position + 1].start;
            //    var remainingTime = startTime - timeOfDay;
            //    remainingTime = remainingTime + new TimeSpan(0, 1, 0);
            //    untilTimeText.Text = "Tunni alguseni: " + remainingTime.Minutes.ToString() + " min.";
            //    nextTimer = true;
            //}

            return view;

        }
        public void TimerClass()
        {
            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(OnElapsed);
            timer.AutoReset = true;
            timer.Start();
        }
        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            var timeOfDay = DateTime.Now;
            var remainingTime = endTime - timeOfDay;
            remainingTime = remainingTime + new TimeSpan(0,1,0);
            remainingTimerText.Text = "Tunni lõpuni: " + remainingTime.Minutes.ToString() + " min.";
            timer.Start(); // Restart timer
        }
    }

}