using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadaAssociationMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerEntry : ContentView
    {
        public PickerEntry()
        {
            InitializeComponent();
        }
        private void CustomEntry_Focused(object sender, FocusEventArgs e)
        {
            VisualStateManager.GoToState(EntryOuterFrame, "Focused");
            VisualStateManager.GoToState(EntryInnerFrame, "Focused");
            VisualStateManager.GoToState(TitleLabel, "Focused");
        }

        private void CustomEntry_Unfocused(object sender, FocusEventArgs e)
        {
            VisualStateManager.GoToState(TitleLabel, "Normal");
            VisualStateManager.GoToState(EntryOuterFrame, "Normal");
            VisualStateManager.GoToState(EntryInnerFrame, "Normal");
        }
        //Placeholder Title
        public static BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(PickerEntry), string.Empty);
        public string Title
        {
            get { return ((string)GetValue(TitleProperty)); }
            set { SetValue(TitleProperty, value); }
        }
        //IsError Property
        public static BindableProperty IsErrorProperty = BindableProperty.Create("IsError", typeof(bool), typeof(PickerEntry), false);
        public bool IsError
        {
            get { return ((bool)GetValue(IsErrorProperty)); }
            set { SetValue(IsErrorProperty, value); }
        }
        //ErrorText Property
        public static BindableProperty ErrorTextProperty = BindableProperty.Create("ErrorText", typeof(string), typeof(PickerEntry), string.Empty);
        public string ErrorText
        {
            get { return ((string)GetValue(ErrorTextProperty)); }
            set { SetValue(ErrorTextProperty, value); }
        }
        //ListItems BINDABLE PROPERTY
        public static readonly BindableProperty ListItemsProperty =
            BindableProperty.Create("ListItems", typeof(List<Selectable>), typeof(PickerEntry), null, propertyChanged: OnListItemsChanged, defaultBindingMode: BindingMode.TwoWay);
        public List<Selectable> ListItems
        {
            get { return (List<Selectable>)GetValue(ListItemsProperty); }
            set { SetValue(ListItemsProperty, value); }
        }
        private static void OnListItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
                (bindable as PickerEntry).ListItems = (List<Selectable>)newValue;
        }
        //SelectetItem BINDABLE PROPERTY
        public static readonly BindableProperty SelectetItemProperty =
            BindableProperty.Create("SelectetItem", typeof(Selectable), typeof(PickerEntry), null, propertyChanged: OnSelectetItemChanged, defaultBindingMode: BindingMode.TwoWay);
        public Selectable SelectetItem
        {
            get { return (Selectable)GetValue(SelectetItemProperty); }
            set { SetValue(SelectetItemProperty, value); }
        }
        private static void OnSelectetItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
                (bindable as PickerEntry).SelectetItem = (Selectable)newValue;
        }
    }
}