using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace Tracker
{
    [Activity(Label = "@string/app_name", Theme = "@android:style/Theme.Material.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
    }
}

