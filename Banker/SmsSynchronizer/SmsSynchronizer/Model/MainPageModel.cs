using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmsSynchronizer.Model
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private double salary;
        private double expense;
        private List<SMS> smss = new List<SMS>();


        public double Salary
        {
            get { return salary; }
            set { salary = value; OnPropertyChanged("Salary"); }
        }

        public double Expense
        {
            get { return expense; }
            set { expense = value; OnPropertyChanged("Expense"); }
        }

        public List<SMS> SMSs
        {
            get { return smss; }
            set { smss = value; OnPropertyChanged("SMSs"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
