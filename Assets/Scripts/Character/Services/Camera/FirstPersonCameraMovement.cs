using Character.Services.Camera.Interfaces;
using UnityEngine;

namespace Character.Services.Camera
{
    public class FirstPersonCameraMovement : ICameraMovement
    {
        Vector2 _rotation = Vector2.zero;
        
        public void MoveCamera(CharacterState characterState)
        {
            var inputX = Input.GetAxis("Mouse X");
            var inputY = Input.GetAxis("Mouse Y");

            _rotation.x += inputX * 1;
            _rotation.y += inputY * 1;
            _rotation.y = Mathf.Clamp(_rotation.y, -82, 82);
            var xQuat = Quaternion.AngleAxis(_rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(_rotation.y, Vector3.left);

            characterState.HeadCameraPoint.transform.localRotation = xQuat * yQuat;
        }
    }
}