using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteItem : MonoBehaviour
{
    [SerializeField] GameObject watch;
    [SerializeField] Emission emission;
    [SerializeField] Animator handAnim;

    void Start()
    {
        if (PlayerPrefs.HasKey("watch"))
        {
            if (PlayerPrefs.GetInt("watch") == 1)
                Interact();
        }
    }

    public void Interact()
    {
        watch.SetActive(false);
        emission.canEmit = false;

        PlayerPrefs.SetInt("watch", 1);
        handAnim.SetBool("Watch", true);
    }
}
