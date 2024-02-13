using Microsoft.JSInterop;
using System.Text.Json;
using System.Text;

namespace WhiteHat.Share.Services.CoreServices
{
    public interface ILocalStorageProvider
    {
        ValueTask<T> GetAsync<T>(string key);
        ValueTask SetAsync<T>(string key, T value);
        ValueTask DeleteAsync(string key);
        ValueTask DeleteAllAsync();
    }

    public record LocalStorageProvider(
        IJSRuntime JSRuntime,
        ILogger<LocalStorageProvider> Logger) : ILocalStorageProvider
    {
        public async ValueTask<T> GetAsync<T>(string key)
        {
            Logger.LogTrace("localStorage.getItem('{0}')", key);

            var json = await JSRuntime.InvokeAsync<string>(
                identifier: "localStorage.getItem",
                args: key
                );

            return json switch
            {
                null => default,
                T result => result,
                _ => JsonSerializer.Deserialize<T>(Encoding.Unicode.GetString(Convert.FromBase64String(json)))
            };
        }

        public ValueTask SetAsync<T>(string key, T value)
        {
            return JSRuntime.InvokeVoidAsync(
                identifier: "localStorage.setItem",
                args: new[] {
                    key,
                    Convert.ToBase64String(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(value)))
                }
            );
        }

        public ValueTask DeleteAsync(string key)
        {
            return JSRuntime.InvokeVoidAsync(
                identifier: "localStorage.removeItem",
                args: key
                );
        }
        public ValueTask DeleteAllAsync()
        {
            return JSRuntime.InvokeVoidAsync(
                identifier: "localStorage.clear"
                );
        }
    }

    public static class LocalStorageServiceExtensions
    {
        public static IServiceCollection AddLocalStorage(this IServiceCollection services)
            => services.AddScoped<ILocalStorageProvider, LocalStorageProvider>();
    }
}

