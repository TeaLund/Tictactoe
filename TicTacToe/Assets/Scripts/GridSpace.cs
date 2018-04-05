using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

    public Button button;
    public GameObject O;
    public GameObject X;
    public Text text;

    private GameController gameController;

    public void SetSpace()
    {
        if (gameController.GetPlayerSide() == "X")
        {
            X.SetActive(true);
        }
        else if (gameController.GetPlayerSide() == "O")
        {
            O.SetActive(true);
        }

        text.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }
}
