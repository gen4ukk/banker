using SmsSynchronizer.Model;
using SmsSynchronizer.ViewModel;
using Syncfusion.ListView.XForms;
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
    public partial class SettingsListViewPage : ContentPage
    {
        public SettingsListViewPage()
        {
            InitializeComponent();
            BindPage();
        }

        private void BindPage()
        {
            BindingContext = new SettingsListViewModel() { Navigation = this.Navigation };
        }

        private void ListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            if (listView.SelectedItem!= null)
            {
                if (listView.SelectedItem is SettingsSchemaModel schema)
                {
                    Navigation.PushAsync(new SettingsPage(schema));
                }
            }
            listView.SelectedItem = null;
        }

        private async void ListView_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            if (e.SwipeOffset > 300)
            {
                var action = await DisplayAlert("Delete", "Are you sure you want to delete?", "Yes", "No");

                if (action)
                {
                    ((SettingsListViewModel)BindingContext).DeleteSchema(e.ItemIndex);
                }
                listView.ResetSwipe();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindPage();
        }
    }
}
