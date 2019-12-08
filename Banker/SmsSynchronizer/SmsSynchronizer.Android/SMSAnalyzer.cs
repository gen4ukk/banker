using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SmsSynchronizer.Model;
using SmsSynchronizer.Services;

namespace SmsSynchronizer.Droid
{
    public class SMSAnalyzer
    {
        public static List<SMSModel> GetSMSbyAddress(string address)
        {
            var selection = "address = ?";
            var selectionArgs = new string[] { address };
            
            return GetSMS(selection, selectionArgs); ;
        }

        public static List<SMSModel> GetSMSaboveCode(string address, int code)
        {
            var selection = "address = ? and _id > ?";
            var selectionArgs = new string[] { address, code.ToString() };

            return GetSMS(selection, selectionArgs); ;
        }

        public static List<SMSModel> GetSMSbyAddress(string address, DateTime dateBegin, DateTime dateEnd)
        {
            var selection = "address = ? and date > ? and date < ? ";
            long unixTimeBegin = (long)(dateBegin - new DateTime(1970, 1, 1)).TotalMilliseconds;
            long unixTimeEnd = (long)(dateEnd - new DateTime(1970, 1, 1)).TotalMilliseconds;
            var selectionArgs = new string[] { address, unixTimeBegin.ToString(), unixTimeEnd.ToString() };

            return GetSMS(selection, selectionArgs); ;
        }

        private static List<SMSModel> GetSMS(string selection, string[] selectionArgs)
        {
            var listOfSMS = new List<SMSModel>();
            try
            {
                var context = Application.Context.ApplicationContext;
                var projection = new string[] { "_id", "address", "body", "date" };
                var sortOrder = "_id ASC";
                var cursor = context.ContentResolver.Query(Telephony.Sms.Inbox.ContentUri, projection, selection, selectionArgs, sortOrder);

                if (cursor.MoveToFirst())
                {
                    do
                    {
                        var sms = new SMSModel();
                        sms.SMSId = cursor.GetInt(cursor.GetColumnIndex("_id"));
                        sms.Address = cursor.GetString(cursor.GetColumnIndex("address"));
                        sms.Body = cursor.GetString(cursor.GetColumnIndex("body"));
                        sms.UnixDate = cursor.GetString(cursor.GetColumnIndex("date"));

                        listOfSMS.Add(sms);

                    } while (cursor.MoveToNext());
                }
            }
            catch (Exception ex)
            {

            }
            return listOfSMS;
        }

        public static List<SMSModel> ParseSMSBody(List<SMSModel> listOfSMS, SettingsSchemaModel model)
        {
            var parsedList = new List<SMSModel>();

            try
            {
                bool financeSMS;

                string profitWordsPattern = string.Empty;

                if (model.KeyProfitWords != null)
                {
                    foreach (var wordItem in model.KeyProfitWords)
                    {
                        profitWordsPattern += wordItem.Name + "|";
                    }
                }

                if (!string.IsNullOrEmpty(profitWordsPattern))
                    profitWordsPattern = profitWordsPattern.Remove(profitWordsPattern.Length - 1);

                foreach (var item in listOfSMS)
                {
                    financeSMS = false;

                    Match matchKeyWords = Regex.Match(item.Body, profitWordsPattern);

                    if (matchKeyWords.Success)
                        financeSMS = true;

                    Match match = Regex.Match(item.Body, model.PatternForAmount);

                    if (match.Success)
                    {
                        item.Profit = financeSMS;
                        item.Amount = Convert.ToDouble(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                        item.Date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(Convert.ToInt64(item.UnixDate));
                        item.Checked = true;

                        parsedList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return parsedList;
        }
    }
}