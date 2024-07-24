using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles : StaticInstance<Subtitles>
{
    [SerializeField]
    GameObject subsPanel;
    [SerializeField]
    TMP_Text subsText;

    string nextText;

    private void Start()
    {
        subsText.text = "";
        subsPanel.SetActive(false);
    }

    public void ShowSubs(Phrase_SO phrase)
    {
        print("Show subs");
        subsPanel.SetActive(true);
        subsText.text = phrase.subs;
    }

    public void HideSubs()
    {
        print("Hide subs");
        subsPanel.SetActive(false);
    }
}
