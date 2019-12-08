﻿using SmsSynchronizer.Model;
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

        MainPageModel CalculateSalary(DateTime dtBegin, DateTime dtEnd);
    }

    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
