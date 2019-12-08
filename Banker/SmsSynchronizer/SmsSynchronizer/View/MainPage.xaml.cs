using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
using SmsSynchronizer.Utils.DB;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmsSynchronizer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private SMSDB smsDb = new SMSDB();
        private SettingsSchemaDB schemaDB = new SettingsSchemaDB();
        private MainPageModel model;

        public MainPage()
        {
            InitializeComponent();
                     
            //smsDb.DropTable();
            //smsDb = new SMSDB();

            model = new MainPageModel();

            BindingContext = model;
            dataGrid.GridLongPressed += DataGrid_GridLongPressed;

            var today = DateTime.Today;
            dtEnd.Date = today;
            dtBegin.Date = new DateTime(today.Date.Year, today.Date.Month, 1);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshPage();
        }

        private void SynchClick(object sender, EventArgs e)
        {
            var serv = DependencyService.Get<BtnClickService>();
            SettingsSchemaModel schemaModel = schemaDB.GetUsersSchema();
            var smss = GetNotSyhchSMS(schemaModel);

            foreach (var item in smss)
            {
                smsDb.AddSMS(item);
            }

            RefreshPage();
        }

        private async void DataGrid_GridLongPressed(object sender, GridLongPressedEventArgs e)
        {
            if (e.RowColumnIndex.ColumnIndex > 0)
            {
                if ((e.RowData is SMSModel sms))
                {
                    await DisplayAlert("SMS", sms.Body, "OK");                   
                }
            }
        }      

        private void DateSelected(object sender, DateChangedEventArgs e)
        {
            RefreshPage();
        }

        private void RefreshPage()
        {
            SettingsSchemaModel schemaModel = schemaDB.GetUsersSchema();

            model.SMSs = smsDb.GetSMSs(dtBegin.Date, dtEnd.Date).Select(t => { t.Checked = true; return t; }).ToList();
            IsCheckedChanged(null, null);
            CheckNotSynchronizedSMS(schemaModel);
        }

        private void IsCheckedChanged(object sender, TappedEventArgs e)
        {
            if (BindingContext != null && BindingContext is MainPageModel bk)
            {
                bk.Salary = 0;
                bk.Expense = 0;

                foreach (var item in bk.SMSs.Where(t => t.Checked))
                {
                    if (item.Profit)
                    {
                        bk.Salary += item.Amount;
                    }
                    else
                    {
                        bk.Expense += item.Amount;
                    }
                }
            }
        }

        private void CheckNotSynchronizedSMS(SettingsSchemaModel schema)
        {
            var smss = GetNotSyhchSMS(schema);

            if (smss.Count > 0)
            {
                btnSynchronize.Text = $"Synchronize ({smss.Count})";
                btnSynchronize.IsEnabled = true;
            }
            else
            {
                btnSynchronize.Text = $"Synchronize";
                btnSynchronize.IsEnabled = false;
            }
        }

        private List<SMSModel> GetNotSyhchSMS(SettingsSchemaModel schema)
        {
            var sms = smsDb.GetMaxSMS();
            int code = sms == null ? 0 : sms.SMSId;
            var serv = DependencyService.Get<BtnClickService>();
            return serv.GetNotSynchSMS(schema, code);
        }
    }
}
