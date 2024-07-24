using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    public int _itemIndex = -1;
    public int _itemId = -1;
    [SerializeField] private ItemsSO _items;
    public static ItemLoader Instance;
    public float SavedTimer { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnItem()
    {
        if (_itemId > -1)
        {
            Grabble grabble = FindAnyObjectByType<Grabble>();
            grabble.SetItem(_items.GetById(_itemId));
        }
        
    }

    public void SetItem(int id)
    {
        _itemId = id;
    }

    public int ReturnIndex()
    {
        return _itemIndex;
    }
}
