using SmsSynchronizer.Model;
using SmsSynchronizer.Utils.DB;
using SmsSynchronizer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SmsSynchronizer.ViewModel
{
    public class NavigationMasterViewModel : BaseViewModel
    {
        public ObservableCollection<NavigationMenuItemModel> MenuItems { get; set; }
        public UserModel User { get; set; }

        private UserDB userDB;

        public NavigationMasterViewModel()
        {
            userDB = DBHelper.Instance().UserDB;
            User = userDB.GetDefaultUser();

            MenuItems = new ObservableCollection<NavigationMenuItemModel>(new[]
                {
                    new NavigationMenuItemModel {
                        Id = 0,
                        Title = "Calculations",
                        IconSource = "calculator.png",
                        //PageObj =  new NavigationPage(new View.MainPage()),
                        PageType = typeof(View.MainPage) },

                    new NavigationMenuItemModel {
                        Id = 1,
                        Title = "Settings",
                        IconSource = "settings.png",
                        //PageObj = new NavigationPage(new SettingsListViewPage()),
                        PageType = typeof(SettingsListViewPage) },
                });
        }
    }
}
