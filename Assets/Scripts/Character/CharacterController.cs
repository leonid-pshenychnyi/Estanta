using System.Collections.Generic;
using System.Linq;
using Character.Services.Camera;
using Models.Character;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] Camera Camera;
        [SerializeField] Transform HeadPoint;

        private CameraService _cameraService;
        
        private CharacterState State = new();

        private void Awake()
        {
            State.Camera = Camera;
            State.Rigidbody = GetComponent<Rigidbody>();
            //State.Model

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
    }
}
