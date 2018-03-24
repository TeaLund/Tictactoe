using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Button startGameButton;
    public Button optionButton;
    public Button quitGameButton;
    public Button cancelButton;
    public Button confirmQuitButton;
    public GameObject quitPanel;
    public Button backButton;
    public GameObject optionsPanel;

    private bool quitPanelToggle = false;
    private bool optionsPanelToggle = false;

    private void Awake()
    {
        quitPanel.SetActive(quitPanelToggle);
        optionsPanel.SetActive(optionsPanelToggle);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void toggleOptionsPanel ()
    {
        optionsPanelToggle = optionsPanelToggle ? false : true;

        optionsPanel.SetActive(optionsPanelToggle);
    }

    public void toggleQuitPanel ()
    {
        quitPanelToggle = quitPanelToggle ? false : true;

        quitPanel.SetActive(quitPanelToggle);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
