namespace Network.Services.Interfaces
{
    public interface ISyncable<T>
    {
        void Subscribe();
        void UpdateElement(T element);
    }
}