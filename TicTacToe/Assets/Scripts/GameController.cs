﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
    public Image icon;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
}

public class GameController : MonoBehaviour {

    public Text[] buttonList;
    public GameObject gameOverPanel;
    public GameObject startInfo;
    public GameObject restartButton;
    public GameObject choosePlayerX;
    public GameObject choosePlayerO;
    public GameObject menuButton;
    public Text gameOverText;

    public GameObject winPanel;
    public GameObject mainMenuPanel;

    private bool winPanelToggle = false;
    private bool mainMenuToggle = false;
    private bool isPlaying = false;
    
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayercolor;

    private string playerSide;
    private int moveCount;

    private void Awake()
    {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = true;
        Screen.orientation = ScreenOrientation.Portrait;

        SetGameControllerReferenceOnButtons();        
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        menuButton.SetActive(true);
        moveCount = 0;

        winPanel.SetActive(winPanelToggle);
        mainMenuPanel.SetActive(mainMenuToggle);
    }

    private void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }   
    
    private void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        oldPlayer.panel.color = inactivePlayercolor.panelColor;
    }

    private void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayercolor.panelColor;
        playerO.panel.color = inactivePlayercolor.panelColor;
    }

    private void SetPlayerColorsActive()
    {
        playerX.panel.color = activePlayerColor.panelColor;
        playerO.panel.color = activePlayerColor.panelColor;
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void ToggleWinPanel ()
    {
        winPanelToggle = winPanelToggle ? false : true;
        winPanel.SetActive(winPanelToggle);
    }

    public void ToggleMainMenuPanel ()
    {
        mainMenuToggle = mainMenuToggle ? false : true;
        mainMenuPanel.SetActive(mainMenuToggle);

        if (mainMenuToggle)
        {
            SetBoardInteractable(false);
            SetPlayerButtons(false);
        }
        else
        {
            if (isPlaying)
            {
                SetBoardInteractable(true);
                SetPlayerButtons(true);
            }
        }
    }

    private void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";

        if(playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }

        StartGame();
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;        
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        isPlaying = true;
        SetPlayerButtons(false);       
        startInfo.SetActive(false);
        menuButton.SetActive(true);
        choosePlayerX.SetActive(false);
        choosePlayerO.SetActive(false);
    }

    public void EndTurn()
    {
        moveCount++;

        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (moveCount >= 9)
        {
            GameOver("draw");
        }
        else
        {
            ChangeSides();
        } 
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        isPlaying = false;
        
        if (winningPlayer == "draw")
        {
            SetGameOverText("It's a draw!");
            SetPlayerColorsInactive();
        }
        else if (winningPlayer == "X")
        {
            SetGameOverText(winningPlayer + " Wins!");
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
            SetPlayerColors(playerO, playerX);
        }

        restartButton.SetActive(true);
        menuButton.SetActive(true);
        ToggleWinPanel();
    }

    public void RestartGame()
    {        
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        menuButton.SetActive(true);
        startInfo.SetActive(true);
        SetPlayerButtons(true);
        choosePlayerX.SetActive(true);
        choosePlayerO.SetActive(true);
        ToggleWinPanel();
        SetPlayerColorsActive();

        for (int i = 0; i < buttonList.Length; i++)
        {            
            buttonList[i].text = "";
            buttonList[i].GetComponentInParent<GridSpace>().O.SetActive(false);
            buttonList[i].GetComponentInParent<GridSpace>().X.SetActive(false);

        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}

