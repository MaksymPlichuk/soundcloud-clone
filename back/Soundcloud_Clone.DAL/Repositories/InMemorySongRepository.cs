using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soundcloud_Clone.DAL.Enitites;

namespace Soundcloud_Clone.DAL.Repositories
{
    public class InMemorySongRepository : ISongRepository
    {
        private readonly ConcurrentDictionary<int, SongEntity> _store = new();
        private int _nextId = 1;

        public Task<SongEntity> CreateAsync(SongEntity song)
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

        public Task<IEnumerable<SongEntity>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<SongEntity?> GetByIdAsync(int id)
        {
            _store.TryGetValue(id, out var song);
            return Task.FromResult(song);
        }

        public Task<bool> UpdateAsync(SongEntity song)
        {
            if (!_store.ContainsKey(song.Id))
                return Task.FromResult(false);

            _store[song.Id] = song;
            return Task.FromResult(true);
        }
    }
}
