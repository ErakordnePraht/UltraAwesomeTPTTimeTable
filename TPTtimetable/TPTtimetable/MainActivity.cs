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
        Android.Support.V7.Widget.Toolbar toolbar;
        ListView list;
        public static SchoolWeek FullTimeTable { get; set; }
        public static DateTime ChosenMonday { get; set; }
        public static DateTime ChosenSunday { get; set; }

        GestureDetector _gestureDetector;
        GestureListener _gestureListener;
        int crntSelection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.side_panel);

            list = FindViewById<ListView>(Resource.Id.listView1);

            GetTimetable getTimeTable = new GetTimetable();
            var timeTable = getTimeTable.Pull("https://tpt.siseveeb.ee/veebivormid/tunniplaan/tunniplaan?oppegrupp=226");
            FullTimeTable = getTimeTable.SortByDay(timeTable);

            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            GetWeekDates getWeekDates = new GetWeekDates();
            ChosenMonday = getWeekDates.GetMonday(DateTime.Now);
            ChosenSunday = getWeekDates.GetSunday(ChosenMonday);
            toolbar.Title = ChosenMonday.ToString("dd/MM") + " - " + ChosenSunday.ToString("dd/MM");

            ClickCurrentDay();

            var nextWeekBtn = FindViewById<ImageButton>(Resource.Id.nextWeekBtn);
            var prevWeekBtn = FindViewById<ImageButton>(Resource.Id.prevWeekBtn);

            nextWeekBtn.Click += NextWeekBtn_Click;
            prevWeekBtn.Click += PrevWeekBtn_Click;

            _gestureListener = new GestureListener();
            _gestureListener.LeftEvent += GestureLeft;
            _gestureListener.RightEvent += GestureRight;
            _gestureDetector = new GestureDetector(this, _gestureListener);
        }

        private void PrevWeekBtn_Click(object sender, EventArgs e)
        {
            GetWeekDates getWeekDates = new GetWeekDates();
            GetTimetable getTimeTable = new GetTimetable();

            ChosenMonday = getWeekDates.GetPreviousWeek(ChosenMonday);
            ChosenSunday = getWeekDates.GetSunday(ChosenMonday);
            var timeTable = getTimeTable.Pull("https://tpt.siseveeb.ee/veebivormid/tunniplaan/tunniplaan?oppegrupp=226&nadal=" + ChosenMonday.ToString("dd.MM.yyyy"));
            FullTimeTable = getTimeTable.SortByDay(timeTable);

            ClickCurrentDay(crntSelection);
            toolbar.Title = ChosenMonday.ToString("dd/MM") + " - " + ChosenSunday.ToString("dd/MM");
        }

        private void NextWeekBtn_Click(object sender, EventArgs e)
        {
            GetWeekDates getWeekDates = new GetWeekDates();
            GetTimetable getTimeTable = new GetTimetable();

            ChosenMonday = getWeekDates.GetNextWeek(ChosenMonday);
            ChosenSunday = getWeekDates.GetSunday(ChosenMonday);
            var timeTable = getTimeTable.Pull("https://tpt.siseveeb.ee/veebivormid/tunniplaan/tunniplaan?oppegrupp=226&nadal=" + ChosenMonday.ToString("dd.MM.yyyy"));
            FullTimeTable = getTimeTable.SortByDay(timeTable);

            ClickCurrentDay(crntSelection);
            toolbar.Title = ChosenMonday.ToString("dd/MM") + " - " + ChosenSunday.ToString("dd/MM");
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_monday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
                    return true;
                case Resource.Id.navigation_tuesday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Tuesday);
                    return true;
                case Resource.Id.navigation_wednesday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Wednesday);
                    return true;
                case Resource.Id.navigation_thursday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Thursday);
                    return true;
                case Resource.Id.navigation_friday:
                    list.Adapter = new ListAdapter(this, FullTimeTable.Friday);
                    return true;
            }
            return false;
        }
        public void ClickCurrentDay(int selection = 0)
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            View mondayTab = this.FindViewById(Resource.Id.navigation_monday);
            View tuesdayTab = this.FindViewById(Resource.Id.navigation_tuesday);
            View wednesdayTab = this.FindViewById(Resource.Id.navigation_wednesday);
            View thursdayTab = this.FindViewById(Resource.Id.navigation_thursday);
            View fridayTab = this.FindViewById(Resource.Id.navigation_friday);

            if (selection == 0)
            {
                DayOfWeek currentDay = DateTime.Now.DayOfWeek;

                switch (currentDay)
                {
                    case DayOfWeek.Monday:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
                        mondayTab.PerformClick();
                        crntSelection = 1;
                        break;
                    case DayOfWeek.Tuesday:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Tuesday);
                        tuesdayTab.PerformClick();
                        crntSelection = 2;
                        break;
                    case DayOfWeek.Wednesday:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Wednesday);
                        wednesdayTab.PerformClick();
                        crntSelection = 3;
                        break;
                    case DayOfWeek.Thursday:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Thursday);
                        thursdayTab.PerformClick();
                        crntSelection = 4;
                        break;
                    case DayOfWeek.Friday:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Friday);
                        fridayTab.PerformClick();
                        crntSelection = 5;
                        break;
                    default:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
                        mondayTab.PerformClick();
                        crntSelection = 1;
                        break;
                }
            }
            else
            {
                switch (selection)
                {
                    case 1:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
                        mondayTab.PerformClick();
                        crntSelection = 1;
                        break;
                    case 2:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Tuesday);
                        tuesdayTab.PerformClick();
                        crntSelection = 2;
                        break;
                    case 3:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Wednesday);
                        wednesdayTab.PerformClick();
                        crntSelection = 3;
                        break;
                    case 4:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Thursday);
                        thursdayTab.PerformClick();
                        crntSelection = 4;
                        break;
                    case 5:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Friday);
                        fridayTab.PerformClick();
                        crntSelection = 5;
                        break;
                    default:
                        list.Adapter = new ListAdapter(this, FullTimeTable.Monday);
                        mondayTab.PerformClick();
                        crntSelection = 1;
                        break;
                }
            }
        }

        void GestureLeft()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            ClickCurrentDay(crntSelection + 1);
        }

        void GestureRight()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            if (crntSelection == 1)
            {
                crntSelection = 6;
            }
            ClickCurrentDay(crntSelection - 1);
        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            _gestureDetector.OnTouchEvent(ev);
            return base.DispatchTouchEvent(ev);
        }
    }
}