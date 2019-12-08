using SmsSynchronizer.Model;
using SmsSynchronizer.Utils.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmsSynchronizer.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationMaster : ContentPage
    {
        public ListView ListView;
        private UserDB userDB = new UserDB();
        private UserModel userModel;

        public NavigationMaster()
        {
            InitializeComponent();

            userModel = userDB.GetDefaultUser();

            BindingContext = new NavigationMasterViewModel(userModel);
            ListView = MenuItemsListView;
        }

        class NavigationMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NavigationMenuItem> MenuItems { get; set; }
            public UserModel User { get; set; }

            public NavigationMasterViewModel(UserModel user)
            {
                MenuItems = new ObservableCollection<NavigationMenuItem>(new[]
                {
                    new NavigationMenuItem { Id = 0, Title = "Calculations", IconSource = "calculator.png", PageObj =  new NavigationPage(new MainPage()) },
                    new NavigationMenuItem { Id = 1, Title = "Settings", IconSource = "settings.png", PageObj = new NavigationPage(new SettingsPage()) },
                });

                User = user;
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}