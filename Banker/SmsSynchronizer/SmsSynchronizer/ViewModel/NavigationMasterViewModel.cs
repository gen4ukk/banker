using SmsSynchronizer.Model;
using SmsSynchronizer.Utils.DB;
using SmsSynchronizer.Utils.Localization;
using SmsSynchronizer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmsSynchronizer.ViewModel
{
    public class NavigationMasterViewModel : BaseViewModel
    {
        public UserModel User { get; set; }
        public ICommand TapCommand { get; }
        public Page Page { get; set; }

        public ObservableCollection<NavigationMenuItemModel> MenuItems
        {
            get { return menuItems; }
            set
            {
                menuItems = value;
                OnPropertyChanged(nameof(MenuItems));
            }
        }
        private ObservableCollection<NavigationMenuItemModel> menuItems;

        public ImageSource Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        private UserDB userDB;
        private ImageSource language;

        public NavigationMasterViewModel()
        {
            userDB = DBHelper.Instance().UserDB;
            User = userDB.GetDefaultUser();
            Language = SetLanguageFlag(User);
            TapCommand = new Command(ImageTapAsync);
            FillMenuItems();
        }

        private ImageSource SetLanguageFlag(UserModel user)
        {
            ImageSource res;
            switch (user.Language)
            {
                case "en-US":
                    res = "united_states_of_america_flag_waving.png";
                    break;
                case "ru":
                    res = "russia_flag_waving.png";
                    break;
                case "uk":
                    res = "ukraine_flag_waving.png";
                    break;
                default:
                    res = "russia-flag-waving-icon-32.png";
                    break;
            }
            return res;
        }

        private void FillMenuItems()
        {
            MenuItems = new ObservableCollection<NavigationMenuItemModel>(new[]{
                    new NavigationMenuItemModel {
                        Id = 0,
                        Title = AppResources.NavigationMasterPageNameCalculations,
                        IconSource = "calculator.png",
                        PageType = typeof(View.MainPage) },

                    new NavigationMenuItemModel {
                        Id = 1,
                        Title = AppResources.NavigationMasterPageNameSettings,
                        IconSource = "settings.png",
                        PageType = typeof(SettingsListViewPage) },

                    new NavigationMenuItemModel {
                        Id = 2,
                        Title = AppResources.NavigationMasterPageNameCurrencyExchange,
                        IconSource = "dollar.png",
                        PageType = typeof(CurrencyExchange) },
                });
        }

        private async void ImageTapAsync()
        {
            string action = await Page.DisplayActionSheet(AppResources.NavigationMasterViewModelSelectLanguage, AppResources.Cancel, null, LanguageDictionary.Keys.ToArray());

            if (!string.IsNullOrEmpty(action) && action != AppResources.Cancel)
            {
                var cultureName = LanguageDictionary[action];

                if (cultureName != User.Language)
                {
                    User.Language = cultureName;
                    Language = SetLanguageFlag(User);
                    var culture = new CultureInfo(cultureName);
                    AppResources.Culture = culture;
                    FillMenuItems();
                    userDB.UpdateUser(User);
                }
            }
        }

        private static Dictionary<string, string> LanguageDictionary = new Dictionary<string, string>()
        {
            {"English","en-US"},
            {"Русский","ru"},
            {"Українська","uk"},
        };
    }
}
