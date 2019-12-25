using SmsSynchronizer.Model;
using SmsSynchronizer.Utils.DB;
using SmsSynchronizer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmsSynchronizer.ViewModel
{
    class SettingsListViewModel : BaseViewModel
    {
        private ObservableCollection<SettingsSchemaModel> items;

        public SettingsListViewModel()
        {
            Items = new ObservableCollection<SettingsSchemaModel>(DBHelper.Instance().SettingsSchemaDB.GetUsersSchemas());
            BtnAddClick = new Command(AddClick);
        }

        public INavigation Navigation { get; set; }

        public ObservableCollection<SettingsSchemaModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ICommand BtnAddClick { get; }

        private async void AddClick(object param)
        {
            var schema = DBHelper.Instance().SettingsSchemaDB.GetDefaultScheme();
            schema.Id = 0;
            schema.UserSchema = true;

            DBHelper.Instance().SettingsSchemaDB.AddSettingsSchema(schema);
            await Navigation.PushAsync(new SettingsPage(schema));
            Items.Add(schema);
        }

        public void DeleteSchema(int index)
        {
            var model = Items[index];
            DBHelper.Instance().SettingsSchemaDB.DeleteSettingsSchema(model.Id);
            Items.RemoveAt(index);
        }
    }
}
