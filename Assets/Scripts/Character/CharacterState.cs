using System;
using System.Collections.Generic;
using System.Linq;
using Models.Character;
using Models.Network;
using UnityEngine;

namespace Character
{
    public class CharacterState
    {
        public CharacterState(Camera camera, Rigidbody rigidbody, Transform headCameraPoint)
        {
            Camera = camera;
            Rigidbody = rigidbody;
            HeadCameraPoint = headCameraPoint;
            
            Id = Guid.NewGuid();
            SyncData = new SyncElement<CharacterSyncElement>
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
        
        // Transforms
        public Transform Transform { get; set; }
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
        public SyncElement<CharacterSyncElement> SyncData { get; set; }
        public string PrefabName { get; set; }

        public CharacterSyncElement UpdateState => new CharacterSyncElement(Transform.position, Transform.rotation, PrefabName);
    }
}