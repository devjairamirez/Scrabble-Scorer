using ScrabbleScorer.ViewModels;
using ScrabbleScorer.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ScrabbleScorer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GameDetailPage), typeof(GameDetailPage));
        }

    }
}
