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
    public partial class CategoriesPage1 : ContentPage
    {
        private CategoriesViewModel _viewModel;

        public CategoriesPage1()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CategoriesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        async void OnItemSelected(Category category)
        {
            if (category == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(NewCategoryPage)}?{nameof(NewCategoryViewModel.ItemId)}={category.Id}");
        }
    }
}