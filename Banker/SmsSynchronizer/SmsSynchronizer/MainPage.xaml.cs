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

namespace SmsSynchronizer
{
    public partial class MainPage : ContentPage
    {
        private SMSDB smsDb;
        private MainPageModel model;

        public MainPage()
        {
            InitializeComponent();
            smsDb = new SMSDB();
            //smsDb.DropTable();
            //smsDb = new SMSDB();

            model = new MainPageModel();

            BindingContext = model;
            dataGrid.GridLongPressed += DataGrid_GridLongPressed;

            var today = DateTime.Today;
            dtEnd.Date = today.AddDays(1).AddSeconds(-1);
            dtBegin.Date = new DateTime(today.Date.Year, today.Date.Month, 1);

            CheckNotSynchronizedSMS();
        }

        private void SynchClick(object sender, EventArgs e)
        {
            var serv = DependencyService.Get<BtnClickService>();
            var smss = GetNotSyhchSMS();

            foreach (var item in smss)
            {
                smsDb.AddSMS(item);
            }

            RefreshGrid();
            CheckNotSynchronizedSMS();
        }

        private async void DataGrid_GridLongPressed(object sender, GridLongPressedEventArgs e)
        {
            if (e.RowColumnIndex.ColumnIndex > 0)
            {
                if ((e.RowData is SMS sms))
                {
                    await DisplayAlert("SMS", sms.Body, "OK");                   
                }
            }
        }      

        private void DateSelected(object sender, DateChangedEventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            model.SMSs = smsDb.GetSMSs(dtBegin.Date, dtEnd.Date).Select(t => { t.Checked = true; return t; }).ToList();
            IsCheckedChanged(null, null);
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

        private void CheckNotSynchronizedSMS()
        {
            var smss = GetNotSyhchSMS();

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

        private List<SMS> GetNotSyhchSMS()
        {
            var sms = smsDb.GetMaxSMS();
            int code = sms == null ? 0 : sms.SMSId;
            var serv = DependencyService.Get<BtnClickService>();
            return serv.GetNotSynchSMS(code);
        }
    }
}
