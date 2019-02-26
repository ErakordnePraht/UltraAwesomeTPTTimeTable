using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Views;

namespace TPTtimetable
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            textMessage = FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_monday:
                    textMessage.SetText(Resource.String.title_monday);
                    return true;
                case Resource.Id.navigation_tuesday:
                    textMessage.SetText(Resource.String.title_tuesday);
                    return true;
                case Resource.Id.navigation_wednesday:
                    textMessage.SetText(Resource.String.title_wednesday);
                    return true;
                case Resource.Id.navigation_thursday:
                    textMessage.SetText(Resource.String.title_thursday);
                    return true;
                case Resource.Id.navigation_friday:
                    textMessage.SetText(Resource.String.title_friday);
                    return true;
            }
            return false;
        }
    }
}