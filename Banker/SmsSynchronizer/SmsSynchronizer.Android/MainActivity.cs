using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Android.Content;
using Android;
using SmsSynchronizer.Services;
using SmsSynchronizer.Model;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(SmsSynchronizer.Droid.MainActivity))]
namespace SmsSynchronizer.Droid
{
    [Activity(Label = "SmsSynchronizer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity , BtnClickService
    {
        public MainPageModel CalculateSalary(DateTime dtBegin, DateTime dtEnd)
        {
            var listOfSMS = SMSAnalyzer.GetSMSbyAddress("OTP Bank", dtBegin, dtEnd);
            var parsedSMS = SMSAnalyzer.ParseSMSBody(listOfSMS, "OTP Bank");

            return new MainPageModel() { SMSs = parsedSMS };
        }

        public List<SMS> GetNotSynchSMS(int code)
        {
            var listOfSMS = SMSAnalyzer.GetSMSaboveCode("OTP Bank", code);
            var parsedSMS = SMSAnalyzer.ParseSMSBody(listOfSMS, "OTP Bank");

            return parsedSMS;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}