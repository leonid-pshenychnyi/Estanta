using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Character;
using Models.Character;
using Network.Services.Interfaces;
using Newtonsoft.Json;
using Shared;

namespace Network.Services.Sync
{
    public class CharactersSyncService : ISyncabe
    {
        static UdpClient sender = new();

        public static List<SyncElement> Characters = new();

        public static Guid SessionId = Guid.NewGuid();
        public static void UpdateElement(SyncElement syncElement)
        {
            var character = Characters.FirstOrDefault(character => character.Id == syncElement.Id);
            character.ElementData.Name = syncElement.ElementData.Name;
            character.ElementData.Position = syncElement.ElementData.Position;
            character.ElementData.Rotation = syncElement.ElementData.Rotation;
            
            SyncData(character);
        }
        
        public void Subscribe()
        {
            var receiveCharactersThread = new Thread(() => SubscribeToChanges((int)Ports.UCharacters, Characters));
            receiveCharactersThread.Start();
        }

        public void RegisterNewSyncUser()
        {
            var data = Encoding.Unicode.GetBytes(SessionId.ToString());
            sender.Send(data, data.Length, Constants.ServerIpAddress, (int)Ports.Users);
        }

        private static void SyncData(SyncElement updateElement)
        {
            var notifyElement = JsonConvert.SerializeObject(updateElement);
            var data = Encoding.Unicode.GetBytes(notifyElement);
            
            sender.Send(data, data.Length, Constants.ServerIpAddress, (int)Ports.Characters);
        }
        
        private void SubscribeToChanges(int port, IReadOnlyCollection<SyncElement> list)
        {
            var receiver = new UdpClient(port);
            IPEndPoint? remoteIp = null;
            while (true)
            {
                var data = receiver.Receive(ref remoteIp);
                var convertedData = Encoding.Unicode.GetString(data);

                if (!string.IsNullOrEmpty(convertedData))
                {
                    var parsedMessage = JsonConvert.DeserializeObject<SyncElement>(convertedData);
                    var character = list.FirstOrDefault(w => w.Id == parsedMessage.Id);
                    if (character != null)
                    {
                        character.ElementData = parsedMessage.ElementData;
                        
                        character.GameObject.transform.position = parsedMessage.ElementData.VectorPosition;
                        character.GameObject.transform.rotation = parsedMessage.ElementData.QuaternionRotation;
                    }
                    else
                    {
                        var newCharacter = new SyncElement
                        {
                            Id = parsedMessage.Id,
                            ElementData = parsedMessage.ElementData
                        };
                        Characters.Add(newCharacter);
                        Starter.SpawnPrefabByName(parsedMessage.ElementData.PrefabName, parsedMessage.ElementData.VectorPosition, parsedMessage.ElementData.QuaternionRotation);
                    }
                }
            }
        }
    }
}