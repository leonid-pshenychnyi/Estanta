using UnityEngine;

namespace Shared
{
    public class Starter : MonoBehaviour
    {
        private void Start()
        {
            SpawnPrefabByName("TestoCharacter");
        }

        public static void SpawnPrefabByName(string prefabName)
        {
            var characterPrefab = GetPrefabByName(prefabName);
            Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
        public static void SpawnPrefabByName(string prefabName, Vector3 position, Quaternion rotation)
        {
            var characterPrefab = GetPrefabByName(prefabName);
            Instantiate(characterPrefab, position, rotation);
        }
        
        private static GameObject GetPrefabByName(string name)
        {
            string folderToSearch = "Prefabs";
            var t = (GameObject[])Resources.LoadAll("GameObjects");
            return (GameObject)Resources.Load($"{folderToSearch}/{name}", typeof(GameObject));
        }
    }
}
