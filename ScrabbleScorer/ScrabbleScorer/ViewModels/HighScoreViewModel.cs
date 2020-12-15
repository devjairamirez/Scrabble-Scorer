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
    public class HighScoreViewModel : BaseViewModel
    {
        public ObservableCollection<Player> Players { get; }
        public Command LoadPlayersCommand { get; }

        public HighScoreViewModel()
        {
            Title = "High Score";
            Players = new ObservableCollection<Player>();
            LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());
        }

        async Task ExecuteLoadPlayersCommand()
        {
            IsBusy = true;

            try
            {
                Players.Clear();
                var players = await PlayerDataStore.GetHighestDescAsync();
                foreach (var player in players)
                {
                    Players.Add(player);
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
    }
}