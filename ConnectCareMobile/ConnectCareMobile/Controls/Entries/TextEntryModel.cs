using ConnectCareMobile.Global;
using System;
using System.Threading.Tasks;

namespace ConnectCareMobile.Controls.Entries
{
    public class TextEntryModel : ObservableObject
    {
        private string _ErrorText = string.Empty;
        private Func<Task> CheckButtonState;
        public TextEntryModel()
        {
        }

        public TextEntryModel(Func<Task> checkButtonState)
        {
            CheckButtonState = checkButtonState;
        }
        public string ErrorText
        {
            get { return _ErrorText; }
            set
            {
                _ErrorText = value;
                RaisePropertyChanged();
            }
        }
        private string _Placeholder = string.Empty;
        public string Placeholder
        {
            get { return _Placeholder; }
            set
            {
                _Placeholder = value;
                RaisePropertyChanged();
            }
        }
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                RaisePropertyChanged();
            }
        }
        private string _Text = string.Empty;
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                CheckButtonState?.Invoke();
                RaisePropertyChanged();
            }
        }

        private bool _IsError = false;
        public bool IsError
        {
            get { return _IsError; }
            set
            {
                _IsError = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsPassword = false;
        public bool IsPassword
        {
            get { return _IsPassword; }
            set
            {
                _IsPassword = value;
                RaisePropertyChanged();
            }
        }

    }
}
