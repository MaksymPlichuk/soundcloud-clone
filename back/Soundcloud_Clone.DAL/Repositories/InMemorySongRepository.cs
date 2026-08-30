using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soundcloud_Clone.API.Models;

namespace Soundcloud_Clone.API.Repositories
{
    public class InMemorySongRepository : ISongRepository
    {
        private readonly ConcurrentDictionary<int, Song> _store = new();
        private int _nextId = 1;

        public Task<Song> CreateAsync(Song song)
        {
            var id = System.Threading.Interlocked.Increment(ref _nextId);
            song.Id = id;
            _store[song.Id] = song;
            return Task.FromResult(song);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(_store.TryRemove(id, out _));
        }

        public Task<IEnumerable<Song>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<Song?> GetByIdAsync(int id)
        {
            _store.TryGetValue(id, out var song);
            return Task.FromResult(song);
        }

        public Task<bool> UpdateAsync(Song song)
        {
            if (!_store.ContainsKey(song.Id))
                return Task.FromResult(false);

            _store[song.Id] = song;
            return Task.FromResult(true);
        }
    }
}
