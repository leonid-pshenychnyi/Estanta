using UnityEngine;

namespace Models.Network
{
    public class CharacterSync : SyncData
    {
        public CharacterSync(Vector3 position, Quaternion rotation, string prefabName) : base(position, rotation, prefabName) { }
    }
}