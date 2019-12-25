using SmsSynchronizer.Model;
using SmsSynchronizer.Utils.DB;
using SmsSynchronizer.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmsSynchronizer.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private SettingsSchemaModel schema;
        public SettingsPage(SettingsSchemaModel schema) : this()
        {
            this.schema = schema;
            BindPage();
        }

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void BindPage()
        {
            BindingContext = new SettingsSchemaViewModel(schema) { Navigation = Navigation };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindPage();
        }

        private void ComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }
    }
}