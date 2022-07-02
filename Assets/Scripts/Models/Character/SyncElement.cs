using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Models.Character
{
    public class SyncElement
    {
        public Guid Id { get; set; }
        public ElementData ElementData { get; set; }
        
        [JsonIgnore]
        public GameObject GameObject { get; set; }
    }

    public class ElementData
    {
        public ElementData(Vector3 position, Quaternion rotation, string prefabName)
        {
            Position = new CustomVector3()
            {
                x = position.x,
                y = position.x,
                z = position.z
            };
            Rotation = new CustomQuaternion()
            {
                x = rotation.x,
                y = rotation.y,
                z = rotation.z,
                w = rotation.w
            };
            PrefabName = prefabName;
        }

        public Vector3 VectorPosition => new(Position.x, Position.y, Position.z);
        public Quaternion QuaternionRotation => new(Rotation.x, Rotation.y, Rotation.z, Rotation.w);

        public string Name { get; set; }
        
        // Transform
        public CustomVector3 Position { get; set; }
        public CustomQuaternion Rotation { get; set; }
        public string PrefabName { get; set; }
    }

    public class CustomVector3
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    public class CustomQuaternion
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }
    }
}