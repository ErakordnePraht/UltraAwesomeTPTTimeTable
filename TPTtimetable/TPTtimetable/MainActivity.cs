using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Runtime;
using Xamarin.Essentials;
using Android.Widget;
using Android.Views;
using System.Collections.Generic;
using System;
using Android.Support.V4.Widget;
using Android.Content;

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
        public static string ClassNum { get; set; }

        GestureDetector _gestureDetector;
        GestureListener _gestureListener;
        int crntSelection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            ClassNum = Preferences.Get("class_num", "226");
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.side_panel);

            list = FindViewById<ListView>(Resource.Id.listView1);

            GetTimetable getTimeTable = new GetTimetable();
            var timeTable = getTimeTable.Pull("https://tpt.siseveeb.ee/veebivormid/tunniplaan/tunniplaan?oppegrupp=" + ClassNum);
            FullTimeTable = getTimeTable.SortByDay(timeTable);

            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            GetWeekDates getWeekDates = new GetWeekDates();
            ChosenMonday = getWeekDates.GetMonday(DateTime.Now);
            ChosenSunday = getWeekDates.GetSunday(ChosenMonday);
            toolbar.Title = ChosenMonday.ToString("dd/MM") + " - " + ChosenSunday.ToString("dd/MM");

            ClickCurrentDay();

            var nextWeekBtn = FindViewById<ImageButton>(Resource.Id.nextWeekBtn);
            var prevWeekBtn = FindViewById<ImageButton>(Resource.Id.prevWeekBtn);
            var drawer = FindViewById<NavigationView>(Resource.Id.nav_view);

            drawer.NavigationItemSelected += Drawer_NavigationItemSelected;
            nextWeekBtn.Click += NextWeekBtn_Click;
            prevWeekBtn.Click += PrevWeekBtn_Click;

            _gestureListener = new GestureListener();
            _gestureListener.LeftEvent += GestureLeft;
            _gestureListener.RightEvent += GestureRight;
            _gestureDetector = new GestureDetector(this, _gestureListener);
        }

        private void Drawer_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var drawerlayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            e.MenuItem.SetChecked(true);
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.open_github:
                    var uri = Android.Net.Uri.Parse("https://github.com/truberton/UltraAwesomeTPTTimeTable");
                    var intent = new Intent(Intent.ActionView, uri);
                    StartActivity(intent);
                    return;
                case Resource.Id.AA17:
                    ClassNum = Resources.GetString(Resource.String.AA17);
                    break;
                case Resource.Id.AA18:
                    ClassNum = Resources.GetString(Resource.String.AA18);
                    break;
                case Resource.Id.AV17:
                    ClassNum = Resources.GetString(Resource.String.AV17);
                    break;
                case Resource.Id.AV18:
                    ClassNum = Resources.GetString(Resource.String.AV18);
                    break;
                case Resource.Id.EA17:
                    ClassNum = Resources.GetString(Resource.String.EA17);
                    break;
                case Resource.Id.EA18:
                    ClassNum = Resources.GetString(Resource.String.EA18);
                    break;
                case Resource.Id.EV17:
                    ClassNum = Resources.GetString(Resource.String.EV17);
                    break;
                case Resource.Id.EV18:
                    ClassNum = Resources.GetString(Resource.String.EV18);
                    break;
                case Resource.Id.FS18:
                    ClassNum = Resources.GetString(Resource.String.FS18);
                    break;
                case Resource.Id.IT16E:
                    ClassNum = Resources.GetString(Resource.String.IT16E);
                    break;
                case Resource.Id.IT16V:
                    ClassNum = Resources.GetString(Resource.String.IT16V);
                    break;
                case Resource.Id.IT17E:
                    ClassNum = Resources.GetString(Resource.String.IT17E);
                    break;
                case Resource.Id.IT17V:
                    ClassNum = Resources.GetString(Resource.String.IT17V);
                    break;
                case Resource.Id.IT18E:
                    ClassNum = Resources.GetString(Resource.String.IT18E);
                    break;
                case Resource.Id.IT18V:
                    ClassNum = Resources.GetString(Resource.String.IT18V);
                    break;
                case Resource.Id.KEV17:
                    ClassNum = Resources.GetString(Resource.String.KEV17);
                    break;
                case Resource.Id.KIT17V:
                    ClassNum = Resources.GetString(Resource.String.KIT17V);
                    break;
                case Resource.Id.KIT18E:
                    ClassNum = Resources.GetString(Resource.String.KIT18E);
                    break;
                case Resource.Id.KK218V:
                    ClassNum = Resources.GetString(Resource.String.KK218V);
                    break;
                case Resource.Id.KTA17E:
                    ClassNum = Resources.GetString(Resource.String.KTA17E);
                    break;
                case Resource.Id.MM17:
                    ClassNum = Resources.GetString(Resource.String.MM17);
                    break;
                case Resource.Id.MM18:
                    ClassNum = Resources.GetString(Resource.String.MM18);
                    break;
                case Resource.Id.MS18:
                    ClassNum = Resources.GetString(Resource.String.MS18);
                    break;
                case Resource.Id.SA17:
                    ClassNum = Resources.GetString(Resource.String.SA17);
                    break;
                case Resource.Id.SA18:
                    ClassNum = Resources.GetString(Resource.String.SA18);
                    break;
                case Resource.Id.TA16E:
                    ClassNum = Resources.GetString(Resource.String.TA16E);
                    break;
                case Resource.Id.TA16V:
                    ClassNum = Resources.GetString(Resource.String.TA16V);
                    break;
                case Resource.Id.TA17E:
                    ClassNum = Resources.GetString(Resource.String.TA17E);
                    break;
                case Resource.Id.TA17V:
                    ClassNum = Resources.GetString(Resource.String.TA17V);
                    break;
                case Resource.Id.TA18E:
                    ClassNum = Resources.GetString(Resource.String.TA18E);
                    break;
                case Resource.Id.TA18V:
                    ClassNum = Resources.GetString(Resource.String.TA18V);
                    break;
                case Resource.Id.TJ18A:
                    ClassNum = Resources.GetString(Resource.String.TJ18A);
                    break;
                case Resource.Id.TS18E:
                    ClassNum = Resources.GetString(Resource.String.TS18E);
                    break;
                case Resource.Id.TS18T:
                    ClassNum = Resources.GetString(Resource.String.TS18T);
                    break;
                case Resource.Id.TT17E:
                    ClassNum = Resources.GetString(Resource.String.TT17E);
                    break;
                case Resource.Id.TT17T:
                    ClassNum = Resources.GetString(Resource.String.TT17T);
                    break;
                case Resource.Id.TT18E:
                    ClassNum = Resources.GetString(Resource.String.TT18E);
                    break;
                case Resource.Id.TT18T:
                    ClassNum = Resources.GetString(Resource.String.TT18T);
                    break;
                default:
                    ClassNum = "226";
                    break;
            }
            Preferences.Set("class_num", ClassNum);
            GetTimetable getTimeTable = new GetTimetable();
            var timeTable = getTimeTable.Pull("https://tpt.siseveeb.ee/veebivormid/tunniplaan/tunniplaan?oppegrupp=" + ClassNum);
            FullTimeTable = getTimeTable.SortByDay(timeTable);
            Preferences.Set("class_num", ClassNum);
            ClickCurrentDay();

            drawerlayout.CloseDrawers();
        }

        private void PrevWeekBtn_Click(object sender, EventArgs e)
        {
            GetWeekDates getWeekDates = new GetWeekDates();
            GetTimetable getTimeTable = new GetTimetable();

            ChosenMonday = getWeekDates.GetPreviousWeek(ChosenMonday);
            ChosenSunday = getWeekDates.GetSunday(ChosenMonday);
            var timeTable = getTimeTable.Pull(string.Format("https://tpt.siseveeb.ee/veebivormid/tunniplaan/tunniplaan?oppegrupp={0}&nadal={1}", ClassNum, ChosenMonday.ToString("dd.MM.yyyy")));
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
            var timeTable = getTimeTable.Pull(string.Format("https://tpt.siseveeb.ee/veebivormid/tunniplaan/tunniplaan?oppegrupp={0}&nadal={1}", ClassNum, ChosenMonday.ToString("dd.MM.yyyy")));
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