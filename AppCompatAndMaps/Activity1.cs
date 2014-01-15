using Android.App;
using Android.Gms.Common;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.OS;

namespace AppCompatAndMaps
{
    //[Activity(Label = "AppCompat and Maps Sample", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.AppCompat.Light")]
    [Activity(Label = "AppCompat and Maps Sample", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Light")]
    //public class Activity1 : ActionBarActivity
    public class Activity1 : FragmentActivity
    {
        public static readonly int InstallGooglePlayServicesId = 1000;
        private bool _playServicesInstalled;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestWindowFeature(WindowFeatures.ActionBar);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        protected override void OnResume()
        {
            base.OnResume();

            _playServicesInstalled = TestIfGooglePlayServicesIsInstalled();
            SupportInvalidateOptionsMenu();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.FindItem(Resource.Id.action_showmap)
                .SetVisible(_playServicesInstalled);

            return base.OnPrepareOptionsMenu(menu);
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

        private bool TestIfGooglePlayServicesIsInstalled()
        {
            var queryResult = GooglePlayServicesUtil.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info("SimpleMapDemo", "Google Play Services is installed on this device.");
                return true;
            }

            if (GooglePlayServicesUtil.IsUserRecoverableError(queryResult))
            {
                var errorString = GooglePlayServicesUtil.GetErrorString(queryResult);
                Log.Error("SimpleMapDemo", "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);
                var errorDialog = GooglePlayServicesUtil.GetErrorDialog(queryResult, this, InstallGooglePlayServicesId);
                errorDialog.Show();
            }
            return false;
        }
    }
}

