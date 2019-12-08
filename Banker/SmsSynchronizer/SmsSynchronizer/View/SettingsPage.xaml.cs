using SmsSynchronizer.Model;
using SmsSynchronizer.Utils.DB;
using System;
using System.Collections.Generic;
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
        private SettingsSchemaDB settingsSchemaDB;
        private SettingsSchemaModel settingsSchema;

        public SettingsPage()
        {
            InitializeComponent();
            settingsSchemaDB = new SettingsSchemaDB();
            settingsSchema = settingsSchemaDB.GetDefaultScheme();
            BindingContext = settingsSchema;

            var keyProfitWord_tap = new TapGestureRecognizer() { NumberOfTapsRequired = 1};
            keyProfitWord_tap.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new KeyProfitWordPage((SettingsSchemaModel)BindingContext));
            };

            var keyBankNameRow_tap = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
            keyBankNameRow_tap.Tapped += async (s, e) =>
            {
                if (!settingsSchema.UserSchema)
                    return;

                string result = await DisplayPromptAsync("Edit Bank name", null);

                if (!string.IsNullOrEmpty(result))
                    settingsSchema.BankName = result;
            };

            var keyPatternForAmountRow_tap = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
            keyPatternForAmountRow_tap.Tapped += async (s, e) =>
            {
                if (!settingsSchema.UserSchema)
                    return;

                string result = await DisplayPromptAsync("Edit pattern for amount", null);

                if (!string.IsNullOrEmpty(result))
                    settingsSchema.PatternForAmount = result;
            };

            KeyProfitWordRow.GestureRecognizers.Add(keyProfitWord_tap);
            BankNameRow.GestureRecognizers.Add(keyBankNameRow_tap);
            PatternForAmountRow.GestureRecognizers.Add(keyPatternForAmountRow_tap);
        }
    }
}