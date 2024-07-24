using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "ScriptableObjects/Items", order = 1)]
public class ItemsSO : ScriptableObject
{ 
    public List<Item> items;

    public Item GetById(int id)
    {

        foreach (Item item in items)
        {
            if (item.ReturnId() == id)
            {
                return item;
            }
        }
        return null;
    }
}
