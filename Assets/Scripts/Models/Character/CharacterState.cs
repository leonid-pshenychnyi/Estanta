using System;
using System.Collections.Generic;
using System.Linq;
using Models.Network;
using Shared;
using UnityEngine;

namespace Models.Character
{
    public class CharacterState
    {
        public CharacterState(Camera camera, Rigidbody rigidbody, Transform headCameraPoint, Transform transform)
        {
            Camera = camera;
            Rigidbody = rigidbody;
            HeadCameraPoint = headCameraPoint;
            CharacterTransform = transform;
            
            Id = Guid.NewGuid();
            SyncData = new SyncElement<CharacterSync>
            {
                Id = Id
            };
            
            Vizios = new List<Vizio>
            {
                new()
                {
                    Id = 1,
                    Transform = Camera.transform,
                    IsAlive = true,
                    IsMovable = true
                }
            };

            PrefabName = "TestoCharacter";
        }

        // Identifiers
        public Guid Id { get; set; }
        public GameObject GameObject { get; set; }
        
        // Transforms
        public Transform CharacterTransform { get; set; }
        public Transform HeadCameraPoint { get; set; }
        
        // Objects
        public Camera Camera { get; set; }
        public Vizio SelectedVizio => selectedVizio ?? Vizios.FirstOrDefault();
        private Vizio selectedVizio { get; set; }
        public List<Vizio> Vizios { get; set; }
        
        // Statuses
        public bool CharacterSpawned { get; set; }
        public CameraPositionType CameraPositionType { get; set; }
        public CameraPositionType ChangingCameraPositionToType { get; set; }
        public bool IsCameraPositionChanging { get; set; }
        
        // Data
        public Rigidbody Rigidbody { get; set; }
        public SyncElement<CharacterSync> SyncData { get; set; }
        public string PrefabName { get; set; }

        public CharacterSync UpdateState => new CharacterSync(CharacterTransform.position, CharacterTransform.rotation, PrefabName);
    }
}