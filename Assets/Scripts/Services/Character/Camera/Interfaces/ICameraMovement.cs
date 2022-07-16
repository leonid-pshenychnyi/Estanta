using Models.Character;

namespace Services.Character.Camera.Interfaces
{
    public interface ICameraMovement
    {
        void MoveCamera(CharacterState characterState);
    }
}