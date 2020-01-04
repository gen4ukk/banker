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
using Plugin.Permissions;
using Plugin.CurrentActivity;
using SmsSynchronizer.ViewModel;

[assembly: Xamarin.Forms.Dependency(typeof(SmsSynchronizer.Droid.MainActivity))]
namespace SmsSynchronizer.Droid
{
    [Activity(Label = "SmsSynchronizer", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity , BtnClickService
    {
        public MainPageViewModel CalculateSalary(DateTime dtBegin, DateTime dtEnd)
        {
            //var listOfSMS = SMSAnalyzer.GetSMSbyAddress("OTP Bank", dtBegin, dtEnd);
            //var parsedSMS = SMSAnalyzer.ParseSMSBody(listOfSMS, "OTP Bank");

            //return new MainPageModel() { SMSs = parsedSMS };
            return new MainPageViewModel();
        }

        public List<SMSModel> GetNotSynchSMS(SettingsSchemaModel model, int code)
        {
            var listOfSMS = SMSAnalyzer.GetSMSaboveCode(model.BankName, code);
            var parsedSMS = SMSAnalyzer.ParseSMSBody(listOfSMS, model);

            return parsedSMS;
        }

        public List<AddressesModel> GetAllSMSAddresses()
        {
            return SMSAnalyzer.GetAllAddresses();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());        
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}