using UnityEngine;

namespace Models.Character
{
    /// <summary>
    /// Model name for player's camera
    /// P.S. name is Vision from esperanto
    /// </summary>
    public class Vizio
    {
        public ushort Id { get; set; }
        
        public Transform Transform { get; set; }
        public bool IsMovable { get; set; }
        public bool IsAlive { get; set; }
    }
}