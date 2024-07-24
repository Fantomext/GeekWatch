using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emission : MonoBehaviour
{
    [SerializeField] private GameObject _outline;
    public bool canEmit = true;

    public void HightLight()
    {
        if(canEmit)
            _outline.SetActive(true);
    }

    public void Extinction()
    {
        _outline.SetActive(false);
    }
}
