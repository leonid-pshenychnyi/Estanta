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
        
        private CharacterState State = new();

        private void Awake()
        {
            State.Camera = Camera;
            State.Rigidbody = GetComponent<Rigidbody>();
            State.SyncData = new SyncElement
            {
                Id = CharactersSyncService.SessionId,
                ElementData = new ElementData(Character.position, Character.rotation, "testoCharacter"),
                GameObject = gameObject
            };
            CharactersSyncService.Characters.Add(State.SyncData);
            CharactersSyncService.UpdateElement(State.SyncData);

            State.Vizios = new List<Vizio>();
            State.HeadCameraPoint = HeadPoint;
            State.Vizios.Add(new Vizio()
            {
                Id = 1,
                Transform = Camera.transform,
                IsAlive = true,
                IsMovable = true
            });
            State.SelectedVizio = State.Vizios.FirstOrDefault();

            _cameraService = new CameraService(State);
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
                State.SyncData.ElementData = new ElementData(Character.position, Character.rotation, "testoCharacter");
                CharactersSyncService.UpdateElement(State.SyncData);
            }
        }
    }
}
