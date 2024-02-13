using WhiteHat.Share.ApiHttpService;

namespace WhiteHat.Share.Services.CoreServices
{
    public interface ISessionState<T> : IState<T>
    {
    }

    public record SessionState<T>(ILocalStorageProvider LocalStorage) : StateRecord<T>, ISessionState<T>
    {
        public override async ValueTask<T> Get(string StorageKey)
        {
            return await LocalStorage.GetAsync<T>(StorageKey);
        }

        public override async ValueTask Set(T value, string StorageKey)
        {
            await LocalStorage.SetAsync(StorageKey, value);

            await base.Set(value, StorageKey);
        }

        public override async ValueTask Delete(string StorageKey)
        {
            await LocalStorage.DeleteAsync(StorageKey);

            await base.Delete(StorageKey);
        }

        public override async ValueTask DeleteAll()
        {
            await LocalStorage.DeleteAllAsync();

            await base.DeleteAll();
        }
    }

}