using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Helpers;
using Mapster;
using Models.Character;
using Models.Network;
using Newtonsoft.Json;
using Services.Network.Interfaces;
using Shared;

namespace Services.Network.Sync
{
    public class CharactersSyncService : ISyncabe<CharacterSync>
    {
        private readonly CharacterState _characterState;
        private readonly List<SyncElement<CharacterSync>> _characters = new();
        private readonly UdpClient _updSender = new();
        
        private readonly PositioningHelper _positioningHelper = new();

        #region Constructors
        public CharactersSyncService(CharacterState characterState)
        {
            _characterState = characterState;

            if (!_characterState.CharacterSpawned)
            {
                _characters.Add(_characterState.SyncData);
                UpdateElement(_characterState.Id, _characterState.UpdateState);
            }
        }
        public CharactersSyncService() { }
        #endregion
        
        /// <summary>
        /// Subscribe to characters changes by specificPort
        /// </summary>
        public void SubscribeToChangesFromServer()
        {
            RegisterNewSyncUser();
            
            var receiveCharactersThread = new Thread(SubscribeToChanges);
            receiveCharactersThread.Start();
        }

        /// <summary>
        /// Update element's sync part and send new info to server
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="newData"></param>
        public void UpdateElement(Guid characterId, CharacterSync newData)
        {
            var character = _characters.FirstOrDefault(character => character.Id == characterId);
            character.SyncData.Adapt(newData);
            
            SendElementDataToServer(character);
        }

        public void SyncElement(CharacterState state)
        {
            var characterSyncData = state.SyncData;
            characterSyncData.SyncData = new CharacterSync(state.CharacterTransform.position, state.CharacterTransform.rotation, string.Empty);
            
            UpdateElement(state.Id, characterSyncData.SyncData);
        }
        
        /// <summary>
        /// Notify server about new user
        /// </summary>
        private void RegisterNewSyncUser()
        {
            var data = Encoding.Unicode.GetBytes(_characterState.Id.ToString());
            _updSender.Send(data, data.Length, Constants.ServerIpAddress, (int)Ports.Users);
        }
        
        /// <summary>
        /// Send character's new data to server
        /// </summary>
        /// <param name="updatedElementData"></param>
        private void SendElementDataToServer(SyncElement<CharacterSync> updatedElementData)
        {
            var stringElementData = JsonConvert.SerializeObject(updatedElementData);
            var dataInBytes = Encoding.Unicode.GetBytes(stringElementData);
            
            _updSender.Send(dataInBytes, dataInBytes.Length, Constants.ServerIpAddress, (int)Ports.ServerCharacters);
        }
        
        /// <summary>
        /// Subscribe to characters changes 
        /// </summary>
        private void SubscribeToChanges()
        {
            var receiver = new UdpClient((int)Ports.UserCharacters);
            IPEndPoint? remoteIp = null;
            while (true)
            {
                var dataInBytes = receiver.Receive(ref remoteIp);
                var convertedData = Encoding.Unicode.GetString(dataInBytes);

                if (!string.IsNullOrEmpty(convertedData))
                {
                    var parsedMessage = JsonConvert.DeserializeObject<SyncElement<CharacterSync>>(convertedData);
                    var character = _characters.FirstOrDefault(w => w.Id == parsedMessage.Id);
                    if (character != null)
                    {
                        _positioningHelper.Move(character.SyncData.VectorPosition, parsedMessage.SyncData.VectorPosition, character.SyncData.GameObject, 0.1f);
                        character.SyncData = parsedMessage.SyncData;
                    }
                    else
                    {
                        var characterObject = Starter.SpawnPrefabByName(parsedMessage?.SyncData.PrefabName, parsedMessage.SyncData.VectorPosition, parsedMessage.SyncData.QuaternionRotation);
                        var newCharacter = new SyncElement<CharacterSync>
                        {
                            Id = parsedMessage.Id,
                            SyncData = parsedMessage.SyncData
                        };
                        newCharacter.SyncData.GameObject = characterObject;
                        
                        _characters.Add(newCharacter);
                    }
                }
            }
        }
    }
}