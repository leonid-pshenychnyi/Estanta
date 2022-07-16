using System;
using Models.Character;
using Models.Network;

namespace Services.Network.Interfaces
{
    public interface ISyncabe<T>
    {
        void SubscribeToChangesFromServer();
        void UpdateElement(Guid elementId, T newData);
        void SyncElement(CharacterState state);
    }
}