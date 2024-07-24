using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unparent : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.transform.parent = null;
    }
}
