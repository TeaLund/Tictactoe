using System.Collections;
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
    public Color textColor;
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
    
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayercolor;

    private string playerSide;
    private int moveCount;

    void Awake()
    {
        SetGameControllerReferenceOnButtons();        
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        menuButton.SetActive(true);
        moveCount = 0;        
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }   
    
    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayercolor.panelColor;
        oldPlayer.text.color = inactivePlayercolor.textColor;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayercolor.panelColor;
        playerX.text.color = inactivePlayercolor.textColor;
        playerO.panel.color = inactivePlayercolor.panelColor;
        playerO.text.color = inactivePlayercolor.textColor;
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    void ChangeSides()
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
        SetPlayerButtons(false);       
        startInfo.SetActive(false);
        menuButton.SetActive(false);
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
        
        if (winningPlayer == "draw")
        {
            SetGameOverText("It's a draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
            SetPlayerColorsInactive();
        }
        restartButton.SetActive(true);
        menuButton.SetActive(true);
    }

    public void RestartGame()
    {        
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        menuButton.SetActive(false);
        startInfo.SetActive(true);
        SetPlayerButtons(true);
        choosePlayerX.SetActive(true);
        choosePlayerO.SetActive(true);
        SetPlayerColorsInactive();

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

