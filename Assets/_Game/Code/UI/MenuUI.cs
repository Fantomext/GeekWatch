using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel, enterName, settings, table, authors, exit;

    GameObject currentPanel;

    private void Start()
    {
        ClosePanel();
    }

    public void OpenNamePanel() => OpenPanel(enterName);
    public void OpenSettingsPanel() => OpenPanel(settings);
    public void OpenTablePanel() => OpenPanel(table);
    public void OpenAuthorsPanel() => OpenPanel(authors);
    public void OpenExitPanel() => OpenPanel(exit);

    public void OpenPanel(GameObject panel)
    {
        menuPanel.SetActive(false);

        currentPanel = panel;
        currentPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        if(currentPanel)
            currentPanel.SetActive(false);

        menuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
