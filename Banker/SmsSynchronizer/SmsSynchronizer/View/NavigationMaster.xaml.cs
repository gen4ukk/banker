using SmsSynchronizer.Model;
using SmsSynchronizer.Utils.DB;
using SmsSynchronizer.ViewModel;
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

        public NavigationMaster()
        {
            InitializeComponent();
            BindingContext = new NavigationMasterViewModel() { Page = this };

            ListView = MenuItemsListView;
        }
    }
}