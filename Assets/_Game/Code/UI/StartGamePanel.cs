using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Leaderboard;
using Tools;
using UnityEngine.SceneManagement;

public class StartGamePanel : MonoBehaviour
{
    [SerializeField] LeaderboardManager leaderboard;
    [SerializeField] private TMP_InputField _playerUsernameInput;
    [SerializeField] GameObject portal;
    Animator _portalAnim;

    private string _usernameKey = "Username";


    private void Start()
    {
        _portalAnim = portal.GetComponent<Animator>();
        if (PlayerPrefs.HasKey(_usernameKey))
            _playerUsernameInput.text = PlayerPrefs.GetString(_usernameKey);
    }

    public void StartGame()
    {
        leaderboard.SetPlayerUsername(_playerUsernameInput.text);
       
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        portal.SetActive(true);
        _portalAnim.SetTrigger("in");
        yield return new WaitForSeconds(1);

        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        asyncScene.allowSceneActivation = false;
        while (!asyncScene.isDone)
        {
            asyncScene.allowSceneActivation = true;
            yield return null;
        }
    }
}
