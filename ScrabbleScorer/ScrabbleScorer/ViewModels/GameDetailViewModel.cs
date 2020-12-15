using ScrabbleScorer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScrabbleScorer.ViewModels
{
    [QueryProperty(nameof(GameId), nameof(GameId))]
    public class GameDetailViewModel : BaseViewModel
    {
        private string gameId;
        public int Id { get; set; }
        private string name;
        private List<Player> players;
        private DateTime startDateTime;
        private DateTime endDateTime;
        public ObservableCollection<Player> PlayersDisplay { get; }
        public Command LoadPlayersCommand { get; }
        public GameDetailViewModel()
        {
            Title = "Game Details";
            LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());
            PlayersDisplay = new ObservableCollection<Player>();
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public DateTime StartDateTime
        {
            get => startDateTime;
            set => SetProperty(ref startDateTime, value);
        }

        public DateTime EndDateTime
        {
            get => endDateTime;
            set => SetProperty(ref endDateTime, value);
        }

        public List<Player> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        public string GameId
        {
            get
            {
                return gameId;
            }
            set
            {
                gameId = value;
                LoadGameId(value);
            }
        }
        async Task ExecuteLoadPlayersCommand()
        {
            IsBusy = true;
            try
            {
                PlayersDisplay.Clear();
                foreach (var player in Players)
                {
                    PlayersDisplay.Add(player);
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

        public async void LoadGameId(string gameId)
        {
            try
            {
                var game = await GameDataStore.GetAsync(Convert.ToInt32(gameId));
                Id = game.Id;
                Name = game.Name;
                StartDateTime = game.StartDateTime;
                EndDateTime = game.EndDateTime;
                Players = await PlayerDataStore.GetByGameAsync(game.Id);

                if (LoadPlayersCommand.CanExecute(null))
                    LoadPlayersCommand.Execute(null);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
