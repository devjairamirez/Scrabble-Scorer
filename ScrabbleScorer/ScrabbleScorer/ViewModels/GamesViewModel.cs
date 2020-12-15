using ScrabbleScorer.Models;
using ScrabbleScorer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScrabbleScorer.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        public ObservableCollection<Game> Games { get; }
        public Command LoadGamesCommand { get; }
        public Command<Game> GameTapped { get; }

        public GamesViewModel()
        {
            Title = "Game History";
            Games = new ObservableCollection<Game>();
            LoadGamesCommand = new Command(async () => await ExecuteLoadGamesCommand());

            GameTapped = new Command<Game>(OnGameSelected);
        }

        async Task ExecuteLoadGamesCommand()
        {
            IsBusy = true;

            try
            {
                Games.Clear();
                var games = await GameDataStore.GetLatestDescAsync();
                foreach (var game in games)
                {
                    Games.Add(game);
                }
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

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async void OnGameSelected(Game game)
        {
            if (game == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(GameDetailPage)}?{nameof(GameDetailViewModel.GameId)}={game.Id.ToString()}");
        }
    }
}