using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System;

namespace TPTtimetable
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static SchoolWeek FullTimeTable { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.experimentalListView);

            var list = FindViewById<ListView>(Resource.Id.listView1);

            GetTimetable getTimeTable = new GetTimetable();
            var timeTable = getTimeTable.Pull("https://tpt.siseveeb.ee/veebivormid/tunniplaan/tunniplaan?oppegrupp=226");
            FullTimeTable = getTimeTable.SortByDay(timeTable);

            list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
        }
    }
}