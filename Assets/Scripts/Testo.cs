using System;
using System.Collections.Generic;
using Helpers.Static;
using Models.Network;
using Newtonsoft.Json;
using UnityEngine;

public class Testo : MonoBehaviour
{
    public GameObject Environment;
    
    private void Start()
    {
        var objects = GetObjectsInLayer(Environment, 7);

        var convertedList = new List<EnvObjectModel>();
        foreach (var obj in objects)
        {
            var newEl = new EnvObjectModel()
            {
                Id = Guid.NewGuid(),
                Name = obj.name,
                CustomVector = obj.transform.position.AdaptToCustomVector(),
                CustomQuaternion = obj.transform.rotation.AdaptToCustomQuaternion(),
                CustomScale = obj.transform.localScale.AdaptToCustomVector()
            };
            
            convertedList.Add(newEl);
        }
        
        var json = JsonConvert.SerializeObject(convertedList);
    }
    
    private static List<GameObject> GetObjectsInLayer(GameObject root, int layer)
    {
        var objects = new List<GameObject>();
        foreach (var child in root.transform)
        {
            if (((Transform)child).gameObject.layer == layer)
            {
                objects.Add(((Transform)child).gameObject);
            }
        }

        return objects;
    }
}