using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrabbleScorer.Services
{
    public interface IGameDataStore<T>
    {
        Task<List<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<List<T>> GetLatestDescAsync();
        Task<int> SaveAsync(T game);
        Task<int> DeleteAsync(T game);
    }
}
