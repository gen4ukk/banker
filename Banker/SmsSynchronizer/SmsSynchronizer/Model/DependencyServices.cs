using SmsSynchronizer.Model;
using SmsSynchronizer.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmsSynchronizer.Services
{
    public interface BtnClickService
    {
        List<SMSModel> GetNotSynchSMS(SettingsSchemaModel model, int code);

        MainPageViewModel CalculateSalary(DateTime dtBegin, DateTime dtEnd);

        List<AddressesModel> GetAllSMSAddresses();
    }

    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
