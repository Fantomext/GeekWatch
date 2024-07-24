using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandManager : MonoBehaviour
{
    [SerializeField] private ItemStand[] _stands;
    [SerializeField] private PlayerUI _playerUI;
    private int _countStand;
    private int _currentComplete;

    private void Start()
    {
        _countStand = _stands.Length;
        ItemLoader.Instance.SpawnItem();
    }


    public void CompleteOneStand()
    {
        _currentComplete++;
        if (_currentComplete == _countStand)
        {
            AllStandsComplete();
        }
    }


    public void AllStandsComplete()
    {
        _playerUI.OpenEnd();
        // все стенды заполнены
    }
}
