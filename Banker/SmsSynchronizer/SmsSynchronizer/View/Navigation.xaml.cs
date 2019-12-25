using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
using SmsSynchronizer.Utils.DB;
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
        private static bool IsAddressesSynchronize = false;
        public static BtnClickService DependencyServices = DependencyService.Get<BtnClickService>();
        public Navigation()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Utils.license.Syncfusion.LICENSE);
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as NavigationMenuItemModel;
            if (item == null)
                return;

            await Task.Run(() =>
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.PageType));
                //Detail = item.PageObj;
            });

            if (!IsAddressesSynchronize)
            {
                SynchronizeAddresses();                
            }

            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }

        private async void SynchronizeAddresses()
        {
            await Task.Run(() =>
            {
                var list = DependencyServices.GetAllSMSAddresses();

                if (list.Count>0)
                {
                    DBHelper.Instance().AddressesDB.DeleteAllData();
                    DBHelper.Instance().AddressesDB.AddAddressesd(list);
                    IsAddressesSynchronize = DBHelper.Instance().AddressesDB.GetAddresses().Count > 0;
                }
                else
                {
                    IsAddressesSynchronize = true;
                }
            });
        }
    }
}