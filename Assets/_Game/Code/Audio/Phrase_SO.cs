using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Phrase", order = 2, fileName = "Phrase")]
public class Phrase_SO : ScriptableObject
{
    [SerializeField] public AudioClip clip;
    [TextArea]
    [SerializeField] public string subs;

    public void ResetData()
    {
       
    }
}
