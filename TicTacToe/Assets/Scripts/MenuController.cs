using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Button startGameButton;
    public Button optionsButton;
    public Button quitGameButton;
    public Button cancelButton;
    public Button confirmQuitButton;
    public GameObject quitPanel;
    public Button backButton;
    public GameObject optionsPanel;
    //public GameObject backgroundLandscape;
    //public GameObject backgroundPortrait;

    private bool quitPanelToggle = false;
    private bool optionsPanelToggle = false;

    private void Awake()
    {
        //Disables the option and quit panels.
        quitPanel.SetActive(quitPanelToggle);
        optionsPanel.SetActive(optionsPanelToggle);
    }

    private void Start()
    {
        //Enables the screen to rotate to the values set to true.
        //Auto-rotates the screen towards the enabled orientations.
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    private void Update()
    {
        if (optionsPanelToggle || quitPanelToggle)
        {
            DisableMenuButtons();
        }
        else
        {
            EnableMenuButtons();
        }

        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            //SetOrientationPortrait();
        }
        else
        {
            //SetOrientationLandscape();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleOptionsPanel ()
    {
        optionsPanelToggle = optionsPanelToggle ? false : true;

        optionsPanel.SetActive(optionsPanelToggle);
    }

    public void ToggleQuitPanel ()
    {
        quitPanelToggle = quitPanelToggle ? false : true;

        quitPanel.SetActive(quitPanelToggle);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DisableMenuButtons ()
    {
        quitGameButton.interactable = false;
        optionsButton.interactable = false;
        startGameButton.interactable = false;
    }

    private void EnableMenuButtons ()
    {
        quitGameButton.interactable = true;
        optionsButton.interactable = true;
        startGameButton.interactable = true;
    }

    /*private void SetOrientationPortrait()
    {
        backgroundLandscape.SetActive(false);
        backgroundPortrait.SetActive(true);
    }

    private void SetOrientationLandscape ()
    {
        backgroundLandscape.SetActive(true);
        backgroundPortrait.SetActive(false);
    }*/
}
