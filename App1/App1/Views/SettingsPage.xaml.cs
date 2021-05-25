using App1.Models;
using App1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel _settingsViewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = _settingsViewModel = new SettingsViewModel();
        }

        private void OnPickerSelectionChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Settings.Themes theme = (Settings.Themes)picker.SelectedItem;

            _settingsViewModel.SelectTheme(theme);
        }
    }
}