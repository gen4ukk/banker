using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace SmsSynchronizer.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", Icon = "@drawable/logo", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SmsSynchronizer.Utils.license.Syncfusion.LICENSE);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        async void SimulateStartup()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SmsSynchronizer.Utils.license.Syncfusion.LICENSE);
            await Task.Run(() => { StartActivity(new Intent(Application.Context, typeof(MainActivity))); });
        }
    }
}