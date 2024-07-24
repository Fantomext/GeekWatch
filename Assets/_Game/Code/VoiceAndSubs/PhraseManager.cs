using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PhraseManager : MonoBehaviour
{
    [SerializeField] List<Phrase_SO> phrases;
    [SerializeField] AudioSource source;

    bool isCurrentPlay;
    bool inGame;

    void Awake()
    {
        isCurrentPlay = false;
        inGame = true;
    }

    private void OnDisable()
    {
        isCurrentPlay = false;
        inGame = false;
    }

    void Test()
    {
        SayPhrase("Test1");
        SayPhrase("Test2");
    }

    private void Update()
    {
        //print(source.isPlaying);
        //if (Input.anyKeyDown) Test();
    }



    public void SayPhrase(string name)
    {
        print("Say " + name);
        Phrase_SO sayPhrase = GetPhrase(name);
        SayPhrase(sayPhrase);
    }

    async public void SayPhrase(Phrase_SO sayPhrase)
    {
        while (isCurrentPlay)
            await WaitTilPlayClip();

        isCurrentPlay = true;

        if (inGame)
        {
            source.clip = sayPhrase.clip;
            source.Play();
            if(Subtitles.Instance)
                Subtitles.Instance.ShowSubs(sayPhrase);

            WaitEndOfClip();
        }
    }

    Phrase_SO GetPhrase(string name)
    {
        foreach(Phrase_SO phrase in phrases)
        {
            if (phrase.name == name)
                return phrase;
        }
        return null;
    }

    async Task WaitTilPlayClip()
    {
        if (isCurrentPlay && inGame)
        {
            print("Already playing voice");
            await Task.Delay(100);
        }
    }

    async void WaitEndOfClip()
    {
        while (source.isPlaying)
        {
            print("Wait playing voice");
            await Task.Delay(200);
        }

        print("End of playing voice");
        isCurrentPlay = false;
        
        await Task.Delay(500);
        if (!isCurrentPlay && Subtitles.Instance)
            Subtitles.Instance.HideSubs();
    }
}
