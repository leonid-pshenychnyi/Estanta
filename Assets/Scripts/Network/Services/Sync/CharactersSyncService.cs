using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Models.Character;
using Network.Services.Interfaces;
using Newtonsoft.Json;
using Unity.VisualScripting;

namespace Network.Services.Sync
{
    public class CharactersSyncService : ISyncable<SyncCharacter>
    {
        public const int localPort = default; // TODO: change
        public const string remoteAddress = default;
        public const int remotePort = default;
        
        UdpClient sender = new();
        UdpClient receiver = new(localPort);

        public List<SyncCharacter> Characters = new();

        public void Subscribe()
        {
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start();
        }

        public void UpdateElement(SyncCharacter syncCharacter)
        {
            var character = Characters.FirstOrDefault(character => character.Id == syncCharacter.Id);
            character.Name = syncCharacter.Name;
            character.Transform = syncCharacter.Transform;
            character.Synced = false;
            
            SyncData();
        }

        private void SyncData()
        {
            var notSyncedCharacters = Characters.Where(character => !character.Synced);
            if (notSyncedCharacters.Any())
            {
                var message = JsonConvert.SerializeObject(notSyncedCharacters);
                
                byte[] data = Encoding.Unicode.GetBytes(message);
                sender.Send(data, data.Length, remoteAddress, remotePort);
            }
        }
        
        private void ReceiveMessage()
        {
            IPEndPoint remoteIp = null;

            while (true)
            {
                byte[] data = receiver.Receive(ref remoteIp);
                string message = Encoding.Unicode.GetString(data);
                var changedCharacters = JsonConvert.DeserializeObject<List<SyncCharacter>>(message);

                // TODO: Redraw characters
                Characters = changedCharacters;
            }
        }
    }
}