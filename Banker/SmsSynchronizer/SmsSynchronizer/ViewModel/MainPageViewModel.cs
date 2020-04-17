using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
using SmsSynchronizer.Utils.DB;
using SmsSynchronizer.Utils.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmsSynchronizer.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private double salary;
        private double expense;
        private List<SMSModel> smss = new List<SMSModel>();
        private List<SMSModel> smssForSynch = new List<SMSModel>();
        private DateTime dtBeg;
        private DateTime dtEnd;
        private string btnSynchtnizeText;

        public MainPageViewModel()
        {
            BtnClick = new Command(Execute, CanExecute);
            SchemaModels = DBHelper.Instance().SettingsSchemaDB.GetUsersSchemas();
            dtBeg = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtEnd = DateTime.Now;
        }

        public ICommand BtnClick { get; }

        public DateTime DtBeg
        {
            get { return dtBeg; }
            set
            {
                dtBeg = value;
                OnPropertyChanged(nameof(DtBeg));
                RefreshPage();
            }
        }

        public List<SettingsSchemaModel> SchemaModels { get; set; }

        public DateTime DtEnd
        {
            get { return dtEnd; }
            set
            {
                dtEnd = value;
                OnPropertyChanged(nameof(DtEnd));
                RefreshPage();
            }
        }

        public double Salary
        {
            get { return salary; }
            set { salary = value; OnPropertyChanged(nameof(Salary)); }
        }

        public double Expense
        {
            get { return expense; }
            set { expense = value; OnPropertyChanged(nameof(Expense)); }
        }

        public List<SMSModel> SMSs
        {
            get { return smss; }
            set { smss = value; OnPropertyChanged(nameof(SMSs)); }
        }

        public List<SMSModel> SmssForSynch
        {
            get { return smssForSynch; }
            set { smssForSynch = value; OnPropertyChanged(nameof(SmssForSynch)); }
        }

        public string BtnSynchtnizeText
        {
            get { return btnSynchtnizeText; }
            set
            {
                btnSynchtnizeText = value;
                OnPropertyChanged(nameof(BtnSynchtnizeText));
                ((Command)BtnClick).ChangeCanExecute();
            }
        }

        public void RefreshPage()
        {
            SchemaModels = DBHelper.Instance().SettingsSchemaDB.GetUsersSchemas();
            SMSs.Clear();

            List<SMSModel> tempSMSs = new List<SMSModel>();
            foreach (var item in SchemaModels)
            {
                var temp = item.SMSs
                    .Where(t => t.Date >= DtBeg && t.Date <= DtEnd)
                    .Select(t => { t.Checked = true; return t; })
                    .ToList();

                if (!item.ShowAllMessages)
                    temp.RemoveAll((t) => { return t.Type == SmsType.Another; });

                tempSMSs.AddRange(temp);
            }

            SMSs = tempSMSs;
            CalculateSalaryAndExpense();
            CheckNotSynchronizedSMS();
        }

        public void CalculateSalaryAndExpense()
        {
            double tempSalary = 0;
            double tempExpense = 0;

            foreach (var item in smss.Where(t => t.Checked))
            {
                switch (item.Type)
                {
                    case SmsType.Profit:
                        tempSalary += item.Amount;
                        break;
                    case SmsType.Expense:
                        tempExpense += item.Amount;
                        break;
                    case SmsType.Another:
                        break;
                    default:
                        break;
                }
            }

            Salary = tempSalary;
            Expense = tempExpense;
        }

        private void CheckNotSynchronizedSMS()
        {
            var serv = View.Navigation.DependencyServices;

            SmssForSynch.Clear();

            List<SMSModel> temp = new List<SMSModel>();

            foreach (var item in SchemaModels.Where(t => t.UserSchema = true && t.Use == true))
            {               
                var sms = DBHelper.Instance().SMSDB.GetMaxSMS(item.Id);
                int code = sms == null ? 0 : sms.SMSId;
                var notSynchSms = serv.GetNotSynchSMS(item, code);

                if (notSynchSms != null)
                    temp.AddRange(notSynchSms.Select(t => { t.SettingsSchemaId = item.Id; return t; }));           
            }

            SmssForSynch = temp;
            BtnSynchtnizeText = $"{AppResources.MainPageViewModelSynchronizeBtnName}({SmssForSynch.Count})";
        }

        public bool CanExecute(object parameter)
        {
            return SmssForSynch.Count > 0;
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            foreach (var item in SmssForSynch)
            {
                DBHelper.Instance().SMSDB.AddSMS(item);
            }

            RefreshPage();
        }
    }
}
