using System.Collections.Generic;
using Models.Character;
using UnityEngine;

namespace Character
{
    public class CharacterState
    {
        public GameObject Model { get; set; }
        public Rigidbody Rigidbody { get; set; }
        
        public Camera Camera { get; set; }
        public List<Vizio> Vizios { get; set; }
        public Vizio SelectedVizio { get; set; }
        public CameraPositionType CameraPositionType { get; set; }

        public Transform HeadCameraPoint { get; set; }
        public Transform BackCameraPoint { get; set; }
        public bool IsPositionChanging { get; set; }
        public CameraPositionType ChangingPositionToType { get; set; }
        public SyncElement SyncData { get; set; }
    }
}