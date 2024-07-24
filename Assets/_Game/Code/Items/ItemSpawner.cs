using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    List<Item> items;

    void Start()
    {
        if(items.Count > 0)
        {
            SpawnItem(GetRandomIndex());
        }
    }
        
    void SpawnItem(int index)
    {
        Instantiate(items[index], transform.position, transform.rotation);
    }

    int GetRandomIndex()
    {
        return Random.Range(0, items.Count);
    }
}
