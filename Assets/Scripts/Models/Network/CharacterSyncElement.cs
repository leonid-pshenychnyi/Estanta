using UnityEngine;

namespace Models.Network
{
    public class CharacterSyncElement : SyncData
    {
        public CharacterSyncElement(Vector3 position, Quaternion rotation, string prefabName) : base(position, rotation, prefabName) { }
    }
}