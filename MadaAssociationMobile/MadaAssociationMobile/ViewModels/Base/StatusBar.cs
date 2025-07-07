using MadaAssociationMobile.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MadaAssociationMobile.ViewModels.Base
{
    public class StatusBar : ObservableObject
    {
        public StatusBar()
        {
            StatusBarColor = Color.White;
            DarkStatusBarTint = true;
        }
        private Color _StatusBarColor = Color.White;
        public Color StatusBarColor
        {
            get { return _StatusBarColor; }
            set
            {
                _StatusBarColor = value;
                RaisePropertyChanged();
            }
        }
        private bool _DarkStatusBarTint = true;
        public bool DarkStatusBarTint
        {
            get { return _DarkStatusBarTint; }
            set
            {
                _DarkStatusBarTint = value;
                RaisePropertyChanged();
            }
        }
    }
}
