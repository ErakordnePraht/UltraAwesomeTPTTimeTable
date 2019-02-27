using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using System.Collections.Generic;
using System;

namespace TPTtimetable
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        public static SchoolWeek FullTimeTable { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var list = FindViewById<ListView>(Resource.Id.listView1);

            GetTimetable getTimeTable = new GetTimetable();
            var timeTable = getTimeTable.Pull("https://tpt.siseveeb.ee/veebivormid/tunniplaan/tunniplaan?oppegrupp=226");
            FullTimeTable = getTimeTable.SortByDay(timeTable);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            View mondayTab = this.FindViewById(Resource.Id.navigation_monday);
            View tuesdayTab = this.FindViewById(Resource.Id.navigation_tuesday);
            View wednesdayTab = this.FindViewById(Resource.Id.navigation_wednesday);
            View thursdayTab = this.FindViewById(Resource.Id.navigation_thursday);
            View fridayTab = this.FindViewById(Resource.Id.navigation_friday);

            DayOfWeek currentDay = DateTime.Now.DayOfWeek;

            switch (currentDay)
            {
                case DayOfWeek.Monday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
                    mondayTab.PerformClick();
                    break;
                case DayOfWeek.Tuesday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Tuesday);
                    tuesdayTab.PerformClick();
                    break;
                case DayOfWeek.Wednesday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Wednesday);
                    wednesdayTab.PerformClick();
                    break;
                case DayOfWeek.Thursday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Thursday);
                    thursdayTab.PerformClick();
                    break;
                case DayOfWeek.Friday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Friday);
                    fridayTab.PerformClick();
                    break;
                default:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
                    mondayTab.PerformClick();
                    break;
            }

        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_monday:
                    //ActionBar.Title = "Esmaspäev";
                    return true;
                case Resource.Id.navigation_tuesday:
                    //ActionBar.Title = "Teisipäev";
                    return true;
                case Resource.Id.navigation_wednesday:
                    //ActionBar.Title = "Kolmapäev";
                    return true;
                case Resource.Id.navigation_thursday:
                    //ActionBar.Title = "Neljapäev";
                    return true;
                case Resource.Id.navigation_friday:
                    //ActionBar.Title = "Reede";
                    return true;
            }
            return false;
        }
    }
}