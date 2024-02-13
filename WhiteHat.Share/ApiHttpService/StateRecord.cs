namespace WhiteHat.Share.ApiHttpService
{
    public interface IState<T>
    {
        ValueTask<T> Get(string StorageKey);
        ValueTask Set(T value, string StorageKey);
        ValueTask Delete(string StorageKey);
        ValueTask DeleteAll();

        event Action<T> OnChanged;
    }

    public record StateRecord<T> : IState<T>
    {
        protected T _value;

        public event Action<T> OnChanged;

        public virtual ValueTask Delete(string StorageKey)
        {
            _value = default;

            StateHasChanged();

            return ValueTask.CompletedTask;
        }
        public virtual ValueTask DeleteAll()
        {
            _value = default;

            StateHasChanged();

            return ValueTask.CompletedTask;
        }

        public virtual ValueTask<T> Get(string StorageKey)
        {
            return ValueTask.FromResult(_value);
        }

        public virtual ValueTask Set(T value, string StorageKey)
        {
            _value = value;

            StateHasChanged();

            return ValueTask.CompletedTask;
        }

        public void StateHasChanged()
        {
            OnChanged?.Invoke(_value);
        }
    }
}
