using MadaAssociationMobile.Global;
using MadaAssociationMobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadaAssociationMobile.Controls
{
    public class Selectable : ObservableObject, ISelectable
    {
        Func<Task> CheckButtonState = null;
        public Selectable() { }
        public Selectable(Func<Task> checkButtonState)
        {
            CheckButtonState = checkButtonState;
        }
        private string _Value = "";
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                CheckButtonState?.Invoke();
            }
        }
        public string ImageName { get; set; } = "";
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
        private string _ErrorText = "";
        public string ErrorText
        {
            get { return _ErrorText; }
            set
            {
                _ErrorText = value;
                RaisePropertyChanged();
            }
        }
        public bool IsSelected { get; set; } = false;
        public bool Enabled { get; set; } = false;
        public string DisplayValue { get; set; } = "";
        public string FullDescription { get; set; } = "";
        public string DisplayValueLinkURL { get; set; } = "";
        public string DisplayValueLinkName { get; set; } = "";
    }
}
