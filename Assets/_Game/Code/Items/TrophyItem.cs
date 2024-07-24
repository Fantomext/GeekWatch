using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    None, Test1, Test2
}

public class TrophyItem : MonoBehaviour
{
    public ItemTypes type;
    [Space]
    public Transform gripPoint, standPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
