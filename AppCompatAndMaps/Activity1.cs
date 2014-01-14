using Android.App;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.OS;

namespace AppCompatAndMaps
{
    //[Activity(Label = "AppCompat and Maps Sample", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.AppCompat.Light")]
    [Activity(Label = "AppCompat and Maps Sample", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Light")]
    //public class Activity1 : ActionBarActivity
    public class Activity1 : FragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestWindowFeature(WindowFeatures.ActionBar);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_showmap)
            {
                var ft = SupportFragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.contentView, new LocationFragment());
                ft.SetTransition(FragmentTransaction.TransitFragmentFade);
                ft.AddToBackStack(null);
                ft.Commit();
                
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

