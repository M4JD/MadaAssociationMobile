using ConnectCareMobile.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.ViewModels.Base
{
    public class TabItem : ObservableObject
    {
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

        private string _Icon;
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; RaisePropertyChanged(); }
        }
        private CommandAsync<TabItem> _TabCommand = null;
        public CommandAsync<TabItem> TabCommand
        {
            get { return _TabCommand; }
            set { _TabCommand = value; RaisePropertyChanged(); }

        }
        public bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; RaisePropertyChanged(); }
        }
    }
}
