using ScrabbleScorer.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ScrabbleScorer.Views
{
    public partial class GameDetailPage : ContentPage
    {
        public GameDetailPage()
        {
            InitializeComponent();
            BindingContext = new GameDetailViewModel();
        }
    }
}