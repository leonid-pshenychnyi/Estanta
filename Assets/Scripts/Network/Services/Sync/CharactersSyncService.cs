using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Character;
using Mapster;
using Models.Character;
using Models.Network;
using Network.Services.Interfaces;
using Newtonsoft.Json;
using Shared;

namespace Network.Services.Sync
{
    public class CharactersSyncService : ISyncabe
    {
        private readonly CharacterState CharacterState;
        
        static readonly UdpClient UpdSender = new();
        public readonly List<SyncElement<CharacterSyncElement>> Characters = new();

        #region Constructors
        public CharactersSyncService(CharacterState characterState)
        {
            CharacterState = characterState;

            if (!CharacterState.CharacterSpawned)
            {
                Characters.Add(CharacterState.SyncData);
                UpdateElement(CharacterState.Id, CharacterState.UpdateState);
            }
        }
        public CharactersSyncService() { }
        #endregion
        
        public void SubscribeToChangesFromServer()
        {
            var receiveCharactersThread = new Thread(() => SubscribeToChanges((int)Ports.UserCharacters));
            receiveCharactersThread.Start();
        }

        public void RegisterNewSyncUser()
        {
            var data = Encoding.Unicode.GetBytes(CharacterState.Id.ToString());
            UpdSender.Send(data, data.Length, Constants.ServerIpAddress, (int)Ports.Users);
        }

        public void UpdateElement(Guid characterId, CharacterSyncElement newData)
        {
            var character = Characters.FirstOrDefault(character => character.Id == characterId);
            character.SyncData.Adapt(newData);
            
            SendElementDataToServer(character);
        }
        
        private void SendElementDataToServer(SyncElement<CharacterSyncElement> updatedElementData)
        {
            var stringElementData = JsonConvert.SerializeObject(updatedElementData);
            var dataInBytes = Encoding.Unicode.GetBytes(stringElementData);
            
            UpdSender.Send(dataInBytes, dataInBytes.Length, Constants.ServerIpAddress, (int)Ports.ServerCharacters);
        }
        
        private void SubscribeToChanges(int port)
        {
            var receiver = new UdpClient(port);
            IPEndPoint? remoteIp = null;
            while (true)
            {
                var dataInBytes = receiver.Receive(ref remoteIp);
                var convertedData = Encoding.Unicode.GetString(dataInBytes);

                if (!string.IsNullOrEmpty(convertedData))
                {
                    var parsedMessage = JsonConvert.DeserializeObject<SyncElement<CharacterSyncElement>>(convertedData);
                    var character = Characters.FirstOrDefault(w => w.Id == parsedMessage.Id);
                    if (character != null)
                    {
                        character.SyncData = parsedMessage.SyncData;
                        
                        character.GameObject.transform.position = parsedMessage.SyncData.VectorPosition;
                        character.GameObject.transform.rotation = parsedMessage.SyncData.QuaternionRotation;
                    }
                    else
                    {
                        var newCharacter = new SyncElement
                        {
                            Id = parsedMessage.Id,
                            SyncData = parsedMessage.SyncData
                        };
                        Characters.Add(newCharacter);
                        Starter.SpawnPrefabByName(parsedMessage.SyncData.PrefabName, parsedMessage.SyncData.VectorPosition, parsedMessage.SyncData.QuaternionRotation);
                    }
                }
            }
        }
    }
}