using SmsSynchronizer.Model;
using SmsSynchronizer.ViewModel;
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
        public KeyProfitWordPage(SettingsSchemaModel schema) : this()
        {
            BindingContext = new KeyProfitWordViewModel(schema, this);
        }

        public KeyProfitWordPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            await ((KeyProfitWordViewModel)BindingContext).DeleteWord((KeyProfitWordModel)e.Item);

            ((ListView)sender).SelectedItem = null;
        }
    }
}
