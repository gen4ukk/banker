using SmsSynchronizer.Model;
using SmsSynchronizer.Utils.DB;
using SmsSynchronizer.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmsSynchronizer.ViewModel
{
    class SettingsSchemaViewModel : BaseViewModel
    {
        private int id;
        private string schemaName;
        private bool showAllMessages;
        private string bankName;
        private List<KeyProfitWordModel> keyProfitWords;
        private string patternForAmount;
        private bool userSchema;
        private bool use;
        private bool isChanged;

        public SettingsSchemaViewModel(SettingsSchemaModel schemaModel)
        {
            id = schemaModel.Id;
            var schema = DBHelper.Instance().SettingsSchemaDB.GetSettingsSchema(id);
            Addresses = DBHelper.Instance().AddressesDB.GetAddresses();

            schemaName = schema.SchemaName;
            showAllMessages = schema.ShowAllMessages;
            bankName = schema.BankName;
            keyProfitWords = schema.KeyProfitWords;
            patternForAmount = schema.PatternForAmount;
            userSchema = schema.UserSchema;
            use = schema.Use;
           
            BtnSave = new Command(Save, CanSave);
            ClickProfitWords = new Command(ClickProfitWord);
            IsChanged = false;
        }

        public INavigation Navigation { get; set; }
        public ICommand BtnSave { get; }
        public ICommand ClickProfitWords { get; }

        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                isChanged = value;
                ((Command)BtnSave).ChangeCanExecute();
            }
        }

        public List<AddressesModel> Addresses { get; set; }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string SchemaName
        {
            get { return schemaName; }
            set
            {
                if (schemaName!= value)
                {
                    schemaName = value;
                    OnPropertyChanged(nameof(SchemaName));
                    IsChanged = true;
                }
            }
        }

        public bool ShowAllMessages
        {
            get { return showAllMessages; }
            set
            {
                if (showAllMessages != value)
                {
                    showAllMessages = value;
                    OnPropertyChanged(nameof(ShowAllMessages));
                    IsChanged = true;
                }
            }
        }

        public string BankName
        {
            get { return bankName; }
            set
            {
                if (bankName!= value)
                {
                    bankName = value;
                    OnPropertyChanged(nameof(BankName));
                    IsChanged = true;
                }
            }
        }

        public List<KeyProfitWordModel> KeyProfitWords
        {
            get { return keyProfitWords; }
            set
            {
                keyProfitWords = value;
                OnPropertyChanged(nameof(KeyProfitWords));
            }
        }

        public string PatternForAmount
        {
            get { return patternForAmount; }
            set
            {
                if (patternForAmount != value)
                {
                    patternForAmount = value;
                    OnPropertyChanged(nameof(PatternForAmount));
                    IsChanged = true;
                }
            }
        }

        public bool UserSchema
        {
            get { return userSchema; }
            set
            {
                if (userSchema != value)
                {
                    userSchema = value;
                    OnPropertyChanged(nameof(UserSchema));
                    IsChanged = true;
                }
            }
        }

        public bool Use
        {
            get { return use; }
            set
            {
                if (use != value)
                {
                    use = value;
                    OnPropertyChanged(nameof(Use));
                    IsChanged = true;
                }
            }
        }

        private bool CanSave(object param)
        {
            return IsChanged;
        }

        private void Save(object param)
        {
            DBHelper.Instance().SettingsSchemaDB.UpdateSettingsSchema(GetSchema());
            IsChanged = false;
        }

        private void ClickProfitWord(object parametr)
        {
            Navigation.PushAsync(new KeyProfitWordPage(GetSchema()));
        }

        private SettingsSchemaModel GetSchema()
        {
            return new SettingsSchemaModel()
            {
                Id = Id,
                SchemaName = SchemaName,
                ShowAllMessages = ShowAllMessages,
                BankName = BankName,
                KeyProfitWords = KeyProfitWords,
                PatternForAmount = PatternForAmount,
                UserSchema = UserSchema,
                Use = Use
            };
        }
    }
}
