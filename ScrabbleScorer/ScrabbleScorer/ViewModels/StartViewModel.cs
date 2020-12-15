using ScrabbleScorer.Helper;
using ScrabbleScorer.Models;
using ScrabbleScorer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScrabbleScorer.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        private string name;
        private string player;
        public ObservableCollection<Player> Players { get; }
        public Command AddPlayerCommand { get; }
        public Command StartCommand { get; }
        public Command LoadPlayersCommand { get; }

        public StartViewModel()
        {
            AddPlayerCommand = new Command(OnAddPlayer);
            LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());
            StartCommand = new Command(OnStart, ValidateStart);
            this.PropertyChanged +=
                (_, __) => StartCommand.ChangeCanExecute();
            Players = new ObservableCollection<Player>();
            SessionData.NewGame = new Game();
            SessionData.NewGame.Players = new List<Player>();
        }
        private bool ValidateStart()
        {
            return !String.IsNullOrWhiteSpace(name)
                && SessionData.NewGame.Players.Count > 0;
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Player
        {
            get => player;
            set => SetProperty(ref player, value);
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
        }

        private void OnAddPlayer()
        {
            SessionData.NewGame.Players.Add(new Player
            {
                Name = player,
                Scores = new List<int>()
            });

            Player = String.Empty;
            if (LoadPlayersCommand.CanExecute(null))
                LoadPlayersCommand.Execute(null);
        }

        private async void OnStart()
        {
            SessionData.NewGame.Name = name;
            SessionData.NewGame.StartDateTime = DateTime.Now;

            Name = String.Empty;
            Players.Clear();
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(CurrentGamePage)}");
        }
    }
}
