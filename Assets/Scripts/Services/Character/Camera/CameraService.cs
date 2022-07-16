using CustomExceptions.Camera;
using Models.Character;
using Services.Character.Camera.Interfaces;
using Shared;

namespace Services.Character.Camera
{
    public class CameraService
    {
        private CharacterState State;

        private ICameraMovement _cameraMovement = new FirstPersonCameraMovement(); // TODO: Change default

        public CameraService(CharacterState state)
        {
            State = state;
        }

        public void ChangeCameraType(Vizio vizio, CameraPositionType positionType)
        {
            if (vizio == null)
                throw new NullCameraException("Null camera on changing camera type.");
            
            if (State.CameraPositionType == positionType || !vizio.IsAlive)
                return;

            SelectCameraMovementType(positionType);
            
            if (positionType == CameraPositionType.Free)
            {
                // TODO: Make camera be free
                State.IsCameraPositionChanging = false;
                return;
            }

            if (vizio.IsMovable && (!State.IsCameraPositionChanging || State.ChangingCameraPositionToType != positionType))
            {
                // TODO: Move Camera to transform from state
                State.IsCameraPositionChanging = true;
                State.ChangingCameraPositionToType = positionType;
            }
        }

        public void MoveCamera()
        {
            _cameraMovement.MoveCamera(State);
        }

        private void SelectCameraMovementType(CameraPositionType positionType)
        {
            switch (positionType)
            {
                case CameraPositionType.FirstPersonView:
                    _cameraMovement = new FirstPersonCameraMovement();
                    break;
                case CameraPositionType.ThirdPersonView:
                    _cameraMovement = new ThirdPersonCameraMovement();
                    break;
                case CameraPositionType.Free:
                    _cameraMovement = new FreeCameraMovement();
                    break;
            }
        }
    }
}