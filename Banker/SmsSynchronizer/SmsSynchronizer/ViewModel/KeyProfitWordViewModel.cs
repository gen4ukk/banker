using SmsSynchronizer.Model;
using SmsSynchronizer.Utils.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmsSynchronizer.ViewModel
{
    public class KeyProfitWordViewModel : BaseViewModel
    {
        private ObservableCollection<KeyProfitWordModel> items;
        private int schemaId;
        private ContentPage page;

        public KeyProfitWordViewModel(SettingsSchemaModel schema, ContentPage _page)
        {
            Items = new ObservableCollection<KeyProfitWordModel>(schema.KeyProfitWords);
            IsUserSchema = schema.UserSchema;
            schemaId = schema.Id;
            page =_page;
            BtnAddClick = new Command(AddClick, CanAddClick);
        }

        public ICommand BtnAddClick { get; }

        public bool IsUserSchema { get; set; }

        public ObservableCollection<KeyProfitWordModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private async void AddClick(object parametr)
        {
            string result = await page.DisplayPromptAsync("Profit word", null);

            if (!string.IsNullOrEmpty(result))
            {
                var word = new KeyProfitWordModel() { Name = result, SettingsSchemaId = schemaId };
                DBHelper.Instance().KeyProfitWordDB.AddKeyProfitWord(word);
                Items.Add(word);
            }
        }

        private bool CanAddClick(object parametr)
        {
            return IsUserSchema;
        }

        public async Task DeleteWord(KeyProfitWordModel key)
        {
            if (!CanAddClick(null))
                return;

            var action = await page.DisplayAlert("Delete", "Are you sure you want to delete?", "Yes", "No");

            if (action)
            {
                DBHelper.Instance().KeyProfitWordDB.DeleteKeyProfitWord(key.Id);
                Items.Remove(key);
            }
        }
    }
}
