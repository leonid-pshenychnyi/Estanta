using System;
using Mapster;
using Newtonsoft.Json;
using UnityEngine;

namespace Models.Network
{
    public class SyncElement<T>
    {
        public Guid Id { get; set; }
        public T SyncData { get; set; }
    }

    public class SyncData
    {
        public SyncData(Vector3 position, Quaternion rotation, string prefabName)
        {
            Position = new CustomVector3
            {
                x = position.x,
                y = position.y,
                z = position.z
            };

            Rotation = new CustomQuaternion
            {
                x = rotation.x,
                y = rotation.y,
                z = rotation.z,
                w = rotation.w
            };

            PrefabName = prefabName;
        }
        
        // Custom transforms
        public CustomVector3 Position { get; set; }
        public CustomQuaternion Rotation { get; set; }
        public string PrefabName { get; set; }

        [JsonIgnore]
        public GameObject GameObject { get; set; }
        
        // True transforms
        [JsonIgnore]
        public Vector3 VectorPosition => new(Position.x, Position.y, Position.z);
        [JsonIgnore]
        public Quaternion QuaternionRotation => new(Rotation.x, Rotation.y, Rotation.z, Rotation.w);
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