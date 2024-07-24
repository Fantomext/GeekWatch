using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    //[SerializeField] Animator portalAnim;
    [SerializeField] CameraController cameraControll;
    [Space]
    [SerializeField]
    GameObject notePanel, settings, endPanel;
    [Space]
    [SerializeField]
    TMP_Text item1, item2, item3, time, score;

    GameObject currentPanel;

    public void OpenNote() => OpenPanel(notePanel);
    public void OpenSettings() => OpenPanel(settings);

    public void OpenEnd()
    {
        OpenPanel(endPanel);
        EndGameScore();
    }

    public void EndGameScore()
    {
        int stand1 = PlayerPrefs.GetInt("stand1Score");
        int stand2 = PlayerPrefs.GetInt("stand2Score");
        int stand3 = PlayerPrefs.GetInt("stand3Score");
        int timeScore = (int)ItemLoader.Instance.SavedTimer;

        item1.text = stand1.ToString();
        item2.text = stand2.ToString();
        item3.text = stand3.ToString();

        time.text = timeScore.ToString();

        int finalScore = stand1 + stand2 + stand3 + timeScore;

        score.text = finalScore.ToString();

        PlayerPrefs.SetInt("PlayerScore", finalScore);

        PlayerPrefs.DeleteKey("stand1");
        PlayerPrefs.DeleteKey("stand2");
        PlayerPrefs.DeleteKey("stand3");
        PlayerPrefs.DeleteKey("stand1Score");
        PlayerPrefs.DeleteKey("stand2Score");
        PlayerPrefs.DeleteKey("stand3Score");
        PlayerPrefs.DeleteKey("watch");
    }

    private void Start()
    {
        //portalAnim.SetTrigger("out");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPanel == notePanel)
                ClosePanel();

            if (!currentPanel) OpenPanel(settings);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        cameraControll.SenseZero();

        print("Open " + panel.name);
        Cursor.lockState = CursorLockMode.None;

        currentPanel = panel;
        currentPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        cameraControll.SenseNormal();

        if (currentPanel)
            currentPanel.SetActive(false);

        currentPanel = null;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitToMenu()
    {
        SceneLoader.Load(0);
    }


}
