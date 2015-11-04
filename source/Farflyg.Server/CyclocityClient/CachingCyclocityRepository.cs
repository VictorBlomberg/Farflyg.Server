namespace Farflyg.Server.CyclocityClient
{
    using System;
    using System.Collections.Immutable;
    using System.Runtime.Caching;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CachingCyclocityRepository : ICyclocityRepository
    {
        private const string _BaseKey = "_Farflyg_";
        private const string _GetStationsKey = _BaseKey + nameof(GetStationsDataAsync);

        private readonly ICyclocityRepository _InnerCyclocityRepository;
        private readonly ObjectCache _Cache;
        private readonly CacheItemPolicy _CacheItemPolicy;

        public CachingCyclocityRepository(ICyclocityRepository innerCyclocityRepository, ObjectCache cache, CacheItemPolicy cacheItemPolicy)
        {
            _InnerCyclocityRepository = innerCyclocityRepository;
            _Cache = cache;
            _CacheItemPolicy = cacheItemPolicy;
        }

        public async Task<ImmutableList<CyclocityStationData>> GetStationsDataAsync(
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _AddOrGetExistingAsync(_GetStationsKey, () => _InnerCyclocityRepository.GetStationsDataAsync(cancellationToken));
        }

        private async Task<T> _AddOrGetExistingAsync<T>(string cacheKey, Func<Task<T>> getValueAsync)
            where T : class
        {
            var _newValue = new TaskCompletionSource<T>();
            var _cachedValue = _Cache.AddOrGetExisting(cacheKey, _newValue, _CacheItemPolicy) as TaskCompletionSource<T>;

            if (_cachedValue != null)
            {
                return await _cachedValue.Task;
            }

            T _value;
            try
            {
                _value = await getValueAsync();
            }
            catch (Exception _e)
            {
                _newValue.SetException(_e);
                throw;
            }

            _newValue.SetResult(_value);

            return _value;
        }
    }
}
