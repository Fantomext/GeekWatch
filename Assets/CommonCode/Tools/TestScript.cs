using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.DeleteKey("watch");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S)) AudioBox.Instance.Play("Test");
        //if (Input.GetKeyDown(KeyCode.M)) AudioBox.Instance.Play("Music");
    }
}
