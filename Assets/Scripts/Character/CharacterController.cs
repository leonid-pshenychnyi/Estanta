using System;
using System.Collections.Generic;
using System.Linq;
using Character.Services.Camera;
using Models.Character;
using Network.Services.Sync;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] Camera Camera;
        [SerializeField] Transform HeadPoint;
        [SerializeField] Transform CharacterModel;
        [SerializeField] Transform Character;

        private CameraService _cameraService;
        private CharactersSyncService _charactersSyncService;
        
        private CharacterState CharacterState;

        private void Awake()
        {
            CharacterState = new CharacterState(Camera, GetComponent<Rigidbody>(), HeadPoint);

            _cameraService = new CameraService(CharacterState);
            _charactersSyncService = new CharactersSyncService(CharacterState);
        }

        private void FixedUpdate()
        {
            _cameraService.MoveCamera();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Character.position = new Vector3(Character.position.x + 1, Character.position.y, Character.position.z);
                ////CharacterState.SyncData.ElementData = new ElementData(Character.position, Character.rotation, "testoCharacter");
                ////CharactersSyncService.UpdateElement(CharacterState.SyncData);
            }
        }
    }
}
