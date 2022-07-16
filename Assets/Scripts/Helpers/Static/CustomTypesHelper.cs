using Models.Network;
using UnityEngine;

namespace Helpers.Static
{
    public static class CustomTypesHelper
    {
        public static Vector3 AdaptToVector(this CustomVector3 customVector)
        {
            return new Vector3()
            {
                x = customVector.x,
                y = customVector.y,
                z = customVector.z
            };
        }
        
        public static CustomVector3 AdaptToCustomVector(this Vector3 customVector)
        {
            return new CustomVector3()
            {
                x = customVector.x,
                y = customVector.y,
                z = customVector.z
            };
        }
        
        public static Quaternion AdaptToQuaternion(this CustomQuaternion customQuaternion)
        {
            return new Quaternion
            {
                x = customQuaternion.x,
                y = customQuaternion.y,
                z = customQuaternion.z,
                w = customQuaternion.w
            };
        }
        
        public static CustomQuaternion AdaptToCustomQuaternion(this Quaternion customQuaternion)
        {
            return new CustomQuaternion
            {
                x = customQuaternion.x,
                y = customQuaternion.y,
                z = customQuaternion.z,
                w = customQuaternion.w
            };
        }
    }
}