using SmsSynchronizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace SmsSynchronizer.ViewModel
{
    public class CurrencyExchangeViewModel : BaseViewModel
    {
        private static string financeuaUrl = @"http://resources.finance.ua/ru/public/currency-cash.xml";

        public CurrencyExchangeViewModel()
        {
            WebClient client = new WebClient();

            var data = client.DownloadData(financeuaUrl);

            CurrencyExchangeModel model;
            if (data != null)
            {
                using (TextReader sr = new StringReader(Encoding.UTF8.GetString(data)))
                {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CurrencyExchangeModel));
                    model = (CurrencyExchangeModel)serializer.Deserialize(sr);
                }

                if (model != null)
                {
                    var res = model.organizations
                        .Where(t => t.org_type == 2)
                        .ToList();

                    Grid = new List<CurrencyExchangeGridRow>();
                    foreach (var item in res)
                    {
                        var currency = item.currencies.Where(x => x.id == "USD").FirstOrDefault();

                        if (currency!= null)
                        {
                            Grid.Add(new CurrencyExchangeGridRow() { Name = item.title.value, Buy = currency.br, Sale = currency.ar });
                        }
                    }
                }
            }
        }

        private List<CurrencyExchangeGridRow> grid;

        public List<CurrencyExchangeGridRow> Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                OnPropertyChanged(nameof(Grid));
            }
        }
    }

    public class CurrencyExchangeGridRow
    {
        public string Name { get; set; }
        public decimal Buy { get; set; }
        public decimal Sale { get; set; }
    }
}
