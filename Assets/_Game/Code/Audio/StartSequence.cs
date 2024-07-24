using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartSequence : MonoBehaviour
{
    [SerializeField] PhraseManager manager;

    bool inGame;

    void Start()
    {
        inGame = true;
        manager = GetComponent<PhraseManager>();

        if (!PlayerPrefs.HasKey("watch"))
            StartCoroutine(StartWaiter());
    }

    void OnDisable()
    {
        inGame = false;
    }

    [ContextMenu("warp")]
    public void StopALl()
    {
        StopAllCoroutines();
    }

    IEnumerator StartWaiter()
    {
        print("Start seq");
        manager.SayPhrase("Start 1");
        yield return new WaitForSeconds(2);
        manager.SayPhrase("Start 2");
        yield return new WaitForSeconds(2);
        manager.SayPhrase("Start 3");

        while (!PlayerPrefs.HasKey("watch"))
            yield return new WaitForSeconds(0.2f);


        manager.SayPhrase("Start 4");
        yield return new WaitForSeconds(1);
        manager.SayPhrase("Start 5");
        yield return new WaitForSeconds(1);
        manager.SayPhrase("Start 6");
        manager.SayPhrase("Start 7");
    }
}
