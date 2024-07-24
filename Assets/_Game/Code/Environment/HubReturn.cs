using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubReturn : MonoBehaviour
{
    LevelTimer timer;

    private void Start()
    {
        timer = LevelTimer.Instance;
        timer.OnTimeOut += ReturnHub;
    }

    private void OnDisable()
    {
        timer.OnTimeOut -= ReturnHub;
    }

    public void ReturnHub()
    {
        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("GeekRoom", LoadSceneMode.Single);
        asyncScene.allowSceneActivation = false;
        while (!asyncScene.isDone)
        {
            Debug.Log("Done");
            yield return new WaitForSeconds(2);
            asyncScene.allowSceneActivation = true;
        }
    }

}
