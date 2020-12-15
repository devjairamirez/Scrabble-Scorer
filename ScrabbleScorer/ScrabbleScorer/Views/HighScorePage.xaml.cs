using ScrabbleScorer.Models;
using ScrabbleScorer.ViewModels;
using ScrabbleScorer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScrabbleScorer.Views
{
    public partial class HighScorePage : ContentPage
    {
        HighScoreViewModel _viewModel;

        public HighScorePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HighScoreViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}