using App1.Models;
using App1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        private NewItemViewModel _viewModel;
        IEnumerable<Category> categories;

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewItemViewModel();

            LoadCategories();
        }

        async Task LoadCategories()
        {

            try
            {
                categories = await _viewModel.DataStoreCategories.GetItemsAsync(true);

                List<string> categoriesTitle = new List<string>();

                foreach (var category in categories)
                {
                    categoriesTitle.Add(category.Title);
                }

                PickerCategory.ItemsSource = categoriesTitle;

                if (categoriesTitle.Count > 0)
                    PickerCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DatePikerDate_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            _viewModel.Date = e.NewDate.ToShortDateString();
        }
    }
}