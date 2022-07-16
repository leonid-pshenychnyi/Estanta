using UnityEngine;
using Random = UnityEngine.Random;

namespace Shared
{
    public class Starter : MonoBehaviour
    {
        private void Start()
        {
            SpawnPrefabByName("Character/TestoCharacter");
        }

        /// <summary>
        /// Spawn prefab on zero position
        /// </summary>
        /// <param name="prefabName"></param>
        public static void SpawnPrefabByName(string prefabName)
        {
            var characterPrefab = GetPrefabByName(prefabName);
            
            // Testo
            var randomYPos = Random.Range(0.0f, 20.0f);
            
            Instantiate(characterPrefab, new Vector3(0, randomYPos, 0), Quaternion.identity);
        }
        
        /// <summary>
        /// Spawn prefab on specific position and rotation
        /// </summary>
        /// <param name="prefabName"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public static GameObject SpawnPrefabByName(string prefabName, Vector3 position, Quaternion rotation)
        {
            var characterPrefabObject = GetPrefabByName(prefabName);
            Instantiate(characterPrefabObject, position, rotation);

            return characterPrefabObject;
        }
        
        /// <summary>
        /// Find prefab in 'Prefab' folder by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static GameObject GetPrefabByName(string name)
        {
            var folderToSearch = "Prefabs";
            return (GameObject)Resources.Load($"{folderToSearch}/{name}");
        }
    }
}
