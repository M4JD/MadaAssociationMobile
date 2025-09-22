using ConnectCareMobile.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConnectCareMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatDetailsView : BaseView
    {
        public ChatDetailsView()
        {
            InitializeComponent();
        }
        private void CustomEntry_Focused(object sender, FocusEventArgs e)
        {
            VisualStateManager.GoToState(EntryOuterFrame, "Focused");
            VisualStateManager.GoToState(EntryInnerFrame, "Focused");
            ScrollToEnd();
        }

        private void CustomEntry_Unfocused(object sender, FocusEventArgs e)
        {
            VisualStateManager.GoToState(EntryOuterFrame, "Normal");
            VisualStateManager.GoToState(EntryInnerFrame, "Normal");
        }
        private async void ScrollToEnd()
        {
            await Task.Delay(200);
            var lastChild = msgstack.Children.LastOrDefault();
            if (lastChild != null)
                await scrollView.ScrollToAsync(lastChild, ScrollToPosition.MakeVisible, false);
        }

        private void msgstack_ChildAdded(object sender, ElementEventArgs e)
        {
            ScrollToEnd();
        }
    }
}