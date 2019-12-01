using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
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
        public MainPage()
        {
            InitializeComponent();
            dataGrid.GridLongPressed += DataGrid_GridLongPressed;
            dtEnd.Date = DateTime.Now;
            dtBegin.Date = new DateTime(dtEnd.Date.Year, dtEnd.Date.Month-1, 1);
        }

        private void SynchClick(object sender, EventArgs e)
        {
            var serv = DependencyService.Get<BtnClickService>();
            serv.SynchClick();
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
            var serv = DependencyService.Get<BtnClickService>();
            BindingContext = serv.CalculateSalary(dtBegin.Date, dtEnd.Date);
            dataGrid.CollapseAllGroup();
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
    }
}
