using ConnectCareMobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConnectCareMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomPicker : ContentView
    {
        public CustomPicker()
        {
            InitializeComponent();
        }
        //DROPDOWN IMAGE BINDABLE PROPERTY
        public static readonly BindableProperty DropdownImageProperty =
            BindableProperty.Create("DropdownImage", typeof(string), typeof(CustomPicker), "arrowdown.png", propertyChanged: OnDropdownImageChanged, defaultBindingMode: BindingMode.TwoWay);
        public string DropdownImage
        {
            get { return (string)GetValue(DropdownImageProperty); }
            set { SetValue(DropdownImageProperty, value); }
        }
        private static void OnDropdownImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
                (bindable as CustomPicker).DropdownImage = (string)newValue;
        }
        //ListItems BINDABLE PROPERTY
        public static readonly BindableProperty ListItemsProperty =
            BindableProperty.Create("ListItems", typeof(List<Selectable>), typeof(CustomPicker), null, propertyChanged: OnListItemsChanged, defaultBindingMode: BindingMode.TwoWay);
        public List<Selectable> ListItems
        {
            get { return (List<Selectable>)GetValue(ListItemsProperty); }
            set { SetValue(ListItemsProperty, value); }
        }
        private static void OnListItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
                (bindable as CustomPicker).ListItems = (List<Selectable>)newValue;
        }
        //SelectetItem BINDABLE PROPERTY
        public static readonly BindableProperty SelectetItemProperty =
            BindableProperty.Create("SelectetItem", typeof(Selectable), typeof(CustomPicker), null, propertyChanged: OnSelectetItemChanged, defaultBindingMode: BindingMode.TwoWay);
        public Selectable SelectetItem
        {
            get { return (Selectable)GetValue(SelectetItemProperty); }
            set { SetValue(SelectetItemProperty, value); }
        }
        private static void OnSelectetItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue)
                (bindable as CustomPicker).SelectetItem = (Selectable)newValue;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DropdownImage = "arrowup.png";
            CPicker.Focus();
        }
        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                Picker picker = (Picker)sender;
                Selectable SelectedItem = (Selectable)picker.SelectedItem;
                SelectedItemDisplayValue.Text = SelectedItem.DisplayValue;
            }
            catch (Exception ex) { }
        }
        private void CPicker_Focused(object sender, FocusEventArgs e)
        {
            DropdownImage = "arrowup.png";
        }
        private void CPicker_Unfocused(object sender, FocusEventArgs e)
        {
            DropdownImage = "arrowdown.png";
        }
    }
}