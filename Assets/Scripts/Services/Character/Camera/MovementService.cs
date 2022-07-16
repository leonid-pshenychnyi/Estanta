using Models.Character;
using UnityEngine;

namespace Services.Character.Camera
{
    public class MovementService
    {
        private readonly CharacterState State;
        
        public MovementService(CharacterState state)
        {
            State = state;
        }
        
        public bool MoveWithResult()
        {
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");
            
            var moveDirection = new Vector3 (moveHorizontal, 0.0f, moveVertical);
            State.Rigidbody.AddForce(moveDirection * 10);

            return State.Rigidbody.velocity != Vector3.zero;
        }
    }
}