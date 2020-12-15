using ScrabbleScorer.Helper;
using ScrabbleScorer.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrabbleScorer.Services
{
    public class GameDataStore : IGameDataStore<Game>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public GameDataStore()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Game).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Game)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public Task<List<Game>> GetAsync()
        {
            return Database.Table<Game>().ToListAsync();
        }

        public Task<List<Game>> GetLatestDescAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Game>("SELECT * FROM Game ORDER BY Id DESC LIMIT 10");
        }

        public Task<Game> GetAsync(int id)
        {
            return Database.Table<Game>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(Game game)
        {
            if (game.Id != 0)
            {
                return Database.UpdateAsync(game);
            }
            else
            {
                return Database.InsertAsync(game);
            }
        }

        public Task<int> DeleteAsync(Game game)
        {
            return Database.DeleteAsync(game);
        }
    }
}