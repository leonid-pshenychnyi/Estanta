using UnityEngine;

namespace Models.Moving
{
    public class MovingElement
    {
        public GameObject GameObject { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public Vector3 StartPosition { get; set; }
        public Vector3 Destination { get; set; }
        public float TimeStarted { get; set; }
        public float Duration { get; set; }
        public float Factor { get; set; }
    }
}