using ScrabbleScorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScrabbleScorer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentGamePage : ContentPage
    {
        CurrentGameViewModel _viewModel;
        public CurrentGamePage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new CurrentGameViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}