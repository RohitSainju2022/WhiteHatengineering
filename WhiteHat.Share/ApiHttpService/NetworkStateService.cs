using Microsoft.JSInterop;

namespace WhiteHat.Share.ApiHttpService
{
    public interface INetworkStateService
    {
        event Action<bool> StatusChanged;
        bool IsOnline { get; }
    }

    public class NetworkStateService : INetworkStateService
    {
        public event Action<bool> StatusChanged;

        public NetworkStateService(IJSRuntime jsRuntime)
        {
            Initialize(jsRuntime);
        }

        [JSInvokable("Network.OnStatusChanged")]
        public void OnStatusChanged(bool isOnline)
        {
            if (IsOnline != isOnline)
            {
                IsOnline = isOnline;
                StatusChanged?.Invoke(isOnline);
            }
        }

        public bool IsOnline { get; protected set; }

        private async void Initialize(IJSRuntime jsRuntime)
        {
            IsOnline = await jsRuntime.InvokeAsync<bool>("Network.Initialize", DotNetObjectReference.Create(this));
        }
    }
}
