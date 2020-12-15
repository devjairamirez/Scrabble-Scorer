using ScrabbleScorer.Helper;
using ScrabbleScorer.Models;
using ScrabbleScorer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScrabbleScorer.ViewModels
{
    public class CurrentGameViewModel : BaseViewModel
    {
        private Player _selectedPlayer;

        private string selectedPlayerName;
        private string score;
        public ObservableCollection<Player> Players { get; }
        public Command AddScoreCommand { get; }
        public Command EndCommand { get; }
        public Command LoadPlayersCommand { get; }
        public Command<Player> PlayerTapped { get; }

        public CurrentGameViewModel()
        {
            Title = "Ongoing Game";
            AddScoreCommand = new Command(OnAddScore, ValidateScore);
            LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());
            EndCommand = new Command(OnEnd);
            this.PropertyChanged +=
                (_, __) => AddScoreCommand.ChangeCanExecute();

            PlayerTapped = new Command<Player>(OnPlayerSelected);
            Players = new ObservableCollection<Player>();
        }
        private bool ValidateScore()
        {
            var outScore = 0;
            return !String.IsNullOrWhiteSpace(score) && Int32.TryParse(Score, out outScore);
        }
        public string SelectedPlayerName
        {
            get => selectedPlayerName;
            set => SetProperty(ref selectedPlayerName, value);
        }
        public string Score
        {
            get => score;
            set => SetProperty(ref score, value);
        }
        async Task ExecuteLoadPlayersCommand()
        {
            IsBusy = true;
            try
            {
                Players.Clear();
                var players = SessionData.NewGame.Players;
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
            SelectedPlayer = null;
        }
        public Player SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                SetProperty(ref _selectedPlayer, value);
                OnPlayerSelected(value);
            }
        }

        private void OnAddScore()
        {
            var outScore = 0;
            Int32.TryParse(Score, out outScore);
            SessionData.NewGame.Players.Where(player => player.Name == SelectedPlayerName).FirstOrDefault().Scores.Add(outScore);

            SelectedPlayer = null;
            SelectedPlayerName = String.Empty;
            Score = String.Empty;
            if (LoadPlayersCommand.CanExecute(null))
                LoadPlayersCommand.Execute(null);
        }

        private async void OnEnd()
        {
            SessionData.NewGame.EndDateTime = DateTime.Now;

            var gameId = await GameDataStore.SaveAsync(SessionData.NewGame);

            foreach(var player in SessionData.NewGame.Players)
            {
                player.GameId = gameId;
                player.FinalScore = player.TotalScore;
                await PlayerDataStore.SaveAsync(player);
            }

            SessionData.NewGame = new Game();
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(StartPage)}");
        }

        void OnPlayerSelected(Player player)
        {
            if (player == null)
                return;

            SelectedPlayerName = player.Name;
        }
    }
}
