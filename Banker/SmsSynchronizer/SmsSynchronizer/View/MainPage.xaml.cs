using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
using SmsSynchronizer.Utils.DB;
using SmsSynchronizer.Utils.Localization;
using SmsSynchronizer.ViewModel;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmsSynchronizer.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CheckPermission();
        }

        private void DataGrid_ValueChanged(object sender, Syncfusion.SfDataGrid.XForms.ValueChangedEventArgs e)
        {
            if (e.RowColumnIndex.ColumnIndex != 0 || ((SMSModel)e.RowData).Type == SmsType.Another)
                return;

            if (BindingContext != null && BindingContext is MainPageViewModel bk)
            {
                bk.CalculateSalaryAndExpense();
            }
        }

        private async void DataGrid_GridLongPressed(object sender, GridLongPressedEventArgs e)
        {
            if (e.RowColumnIndex.ColumnIndex > 0)
            {
                if (e.RowData is SMSModel sms)
                {
                    await DisplayAlert("SMS", sms.Body, "OK");                   
                }
            }
        }      

        private async void CheckPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Sms);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Sms))
                    {
                        await DisplayAlert(AppResources.Attention, AppResources.NeedAccessToSms, AppResources.OK);
                    }
                    await CrossPermissions.Current.RequestPermissionsAsync(Permission.Sms);
                };
            }
            catch (Exception ex)
            {

            }
        }
    }
}
