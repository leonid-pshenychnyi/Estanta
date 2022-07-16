using Helpers;
using Models.Character;
using Models.Network;
using Services.Character.Camera;
using Services.Network.Interfaces;
using Services.Network.Sync;
using UnityEngine;

namespace Controllers
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] Camera Camera;
        [SerializeField] Transform HeadPoint;
        [SerializeField] Transform CharacterModel;
        [SerializeField] Transform Character;

        private CameraService _cameraService;
        private MovementService _movementService;
        private ISyncabe<CharacterSync> _charactersSyncService;
        
        private readonly PositioningHelper _positioningHelper = new();

        private CharacterState CharacterState;

        private void Awake()
        {
            // Initialize character's state
            CharacterState = new CharacterState(Camera, GetComponent<Rigidbody>(), HeadPoint, Character);

            _cameraService = new CameraService(CharacterState);
            _movementService = new MovementService(CharacterState);
            _charactersSyncService = new CharactersSyncService(CharacterState);
            _charactersSyncService.SubscribeToChangesFromServer();
        }

        private void FixedUpdate()
        {
            // Move all elements
            _positioningHelper.FixedMoveElements();
            
            // Move and sync character
            var moved = _movementService.MoveWithResult();
            if (moved)
            {
                _charactersSyncService.SyncElement(CharacterState);
            }

            // Move camera from mouse axis
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
