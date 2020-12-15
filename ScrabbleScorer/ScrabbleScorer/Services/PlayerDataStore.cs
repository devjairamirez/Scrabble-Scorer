using ScrabbleScorer.Helper;
using ScrabbleScorer.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrabbleScorer.Services
{
    public class PlayerDataStore : IPlayerDataStore<Player>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public PlayerDataStore()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Player).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Player)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public Task<List<Player>> GetAsync()
        {
            return Database.Table<Player>().ToListAsync();
        }

        public Task<List<Player>> GetHighestDescAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Player>("SELECT * FROM Player ORDER BY FinalScore DESC LIMIT 10");
        }

        public Task<Player> GetAsync(int id)
        {
            return Database.Table<Player>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Player>> GetByGameAsync(int id)
        {
            return Database.Table<Player>().Where(i => i.GameId == id).ToListAsync();
        }

        public Task<int> SaveAsync(Player player)
        {
            if (player.Id != 0)
            {
                return Database.UpdateAsync(player);
            }
            else
            {
                return Database.InsertAsync(player);
            }
        }

        public Task<int> DeleteAsync(Player player)
        {
            return Database.DeleteAsync(player);
        }
    }
}