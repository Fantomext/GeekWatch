using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Data _data;
    [ContextMenu("Save")]
    private void Start()
    {
        Load();
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(_data);
        PlayerPrefs.SetString("Save", json);
        PlayerPrefs.Save();
    }


    [ContextMenu("Load")]
    public void Load()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            string json = PlayerPrefs.GetString("Save");
            _data = JsonUtility.FromJson<Data>(json);
        }
    }

    [Serializable]
    public class Data
    {
        public int score;
        public List<int> stands;
        public int currentItem;
        public List<int> items;
    }
}
