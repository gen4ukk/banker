using SmsSynchronizer.Utils.DB;
using SmsSynchronizer.View;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SmsSynchronizer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            
            var culture = new CultureInfo(DBHelper.Instance().UserDB.GetDefaultUser().Language);
            Utils.Localization.AppResources.Culture = culture;
            MainPage = new Navigation();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
