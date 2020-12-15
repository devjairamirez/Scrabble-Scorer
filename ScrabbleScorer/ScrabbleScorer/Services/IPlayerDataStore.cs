using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrabbleScorer.Services
{
    public interface IPlayerDataStore<T>
    {
        Task<List<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<List<T>> GetByGameAsync(int id);
        Task<List<T>> GetHighestDescAsync();
        Task<int> SaveAsync(T player);
        Task<int> DeleteAsync(T player);
    }
}
