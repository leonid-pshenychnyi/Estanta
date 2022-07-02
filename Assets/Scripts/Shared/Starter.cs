
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
            string[] foldersToSearch = {"Assets/Prefabs"};
            List<GameObject> allPrefabs = GetAssets<GameObject>(foldersToSearch, "t:prefab");

            return allPrefabs.FirstOrDefault(w => w.name == name);
        }
     
        private static List<T> GetAssets<T>(string[] _foldersToSearch, string _filter) where T : UnityEngine.Object
        {
            var guids = AssetDatabase.FindAssets(_filter, _foldersToSearch);
            var a = new List<T>();
            foreach (var t in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(t);
                a.Add(AssetDatabase.LoadAssetAtPath<T>(path));
            }
            return a;
        }
    }
}
