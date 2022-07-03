using Network.Services.Sync;
using UnityEngine;

namespace Network
{
    public class NetworkController : MonoBehaviour
    {
        private readonly CharactersSyncService _charactersSyncService = new();
        private readonly EnvironmentSyncService _environmentSyncService = new();
        private readonly NpcSyncService _npcSyncService = new();

        private void Start()
        {
            _charactersSyncService.RegisterNewSyncUser();
            _charactersSyncService.SubscribeToChangesFromServer();
        }
    }
}
