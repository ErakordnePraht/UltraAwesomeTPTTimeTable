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

            DayOfWeek currentDay = DateTime.Now.DayOfWeek;

            switch (currentDay)
            {
                case DayOfWeek.Monday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
                    break;
                case DayOfWeek.Tuesday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Tuesday);
                    break;
                case DayOfWeek.Wednesday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Wednesday);
                    break;
                case DayOfWeek.Thursday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Thursday);
                    break;
                case DayOfWeek.Friday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Friday);
                    break;
                default:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
                    break;
            }

        }
    }
}