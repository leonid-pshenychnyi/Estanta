using System.Collections.Generic;
using Models.Moving;
using UnityEngine;

namespace Helpers
{
    public class PositioningHelper
    {
        private readonly List<MovingElement> MovingRbElements = new();
        private readonly List<MovingElement> MovingElement = new();

        /// <summary>
        /// Add element to moving list, moving working ONLY with rigidbody
        /// </summary>
        /// <param name="startPosition">Moving element transform</param>
        /// <param name="destination">Moving element final destination</param>
        /// <param name="rigidbody">Element's rigidbody</param>
        /// <param name="time">Time to move</param>
        public void MoveWithRb(Vector3 startPosition, Vector3 destination, Rigidbody rigidbody, float time)
        {
            var movingElement = new MovingElement
            {
                Rigidbody = rigidbody,
                StartPosition = startPosition,
                Destination = destination,
                TimeStarted = Time.deltaTime,
                Duration = time,
                Factor = Vector3.Distance(startPosition, destination) / time
            };
            MovingRbElements.Add(movingElement);
        }

        public void Move(Vector3 startPosition, Vector3 destination, GameObject characterObject, float time)
        {
            var movingElement = new MovingElement
            {
                GameObject = characterObject,
                StartPosition = startPosition,
                Destination = destination,
                TimeStarted = Time.time,
                Duration = time
            };
            MovingElement.Add(movingElement);
        }

        /// <summary>
        /// Repeatedly move rb elements [Fixed]
        /// </summary>
        public void FixedMoveElements()
        {
            foreach (var movingElement in MovingRbElements)
            {
                MoveRbElement(movingElement);
            }
        }
        
        /// <summary>
        /// Teleport element to position
        /// </summary>
        /// <param name="elementTransform">Teleporting element transform</param>
        /// <param name="destination">Moving element final destination</param>
        public void Teleport(ref Transform elementTransform, Transform destination)
        {
            elementTransform.position = destination.position;
        }

        /// <summary>
        /// Change element with rigidbody velocity changing to destination direction
        /// </summary>
        /// <param name="element">Iterator element</param>
        private void MoveRbElement(MovingElement element)
        {
            if (Time.time < element.TimeStarted + element.Duration)
            {
                element.Rigidbody.velocity = (element.Destination - element.StartPosition) / Vector3.Distance(element.Destination, element.StartPosition) * element.Factor;
            }
            else
            {
                MovingRbElements.Remove(element);
            }
        }

        private void MoveElement(MovingElement element)
        {
            var delta = (Time.time - element.TimeStarted) / element.Duration;
            if (delta <= 1)
            {
                element.GameObject.transform.position = Vector3.Lerp(element.StartPosition, element.Destination, delta);
            }
        }
    }
}