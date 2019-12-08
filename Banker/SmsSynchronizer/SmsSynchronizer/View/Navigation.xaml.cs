using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmsSynchronizer.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigation : MasterDetailPage
    {
        public Navigation()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Utils.license.Syncfusion.LICENSE);
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as NavigationMenuItem;
            if (item == null)
                return;

            await Task.Run(() =>
            {
                Detail = item.PageObj;
            });

            await Task.Delay(100);
            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }
    }
}