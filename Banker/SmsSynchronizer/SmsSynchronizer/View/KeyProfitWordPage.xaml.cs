using SmsSynchronizer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmsSynchronizer.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KeyProfitWordPage : ContentPage
    {
        public ObservableCollection<KeyProfitWordModel> Items { get; set; }
        private bool isUserSchema;

        private SettingsSchemaModel Schema;

        public KeyProfitWordPage(SettingsSchemaModel schema) : this()
        {
            Schema = schema;
            isUserSchema = Schema.UserSchema;
            Items = new ObservableCollection<KeyProfitWordModel>(Schema.KeyProfitWords);
            MyListView.ItemsSource = Items;
            BtnAddWord.IsEnabled = isUserSchema;
        }

        public KeyProfitWordPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null || !isUserSchema)
                return;

            var action = await DisplayAlert("Delete", "Do you want delete this item", "Yes", "No");

            if (action)
            {
                Items.Remove((KeyProfitWordModel)e.Item);
            }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Profit word", null);

            if (!string.IsNullOrEmpty(result))
            {
                Items.Add(new KeyProfitWordModel() { Name = result, SettingsSchemaId = Schema .Id});
            }
        }
    }
}
