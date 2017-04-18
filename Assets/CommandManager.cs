using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour
{
    public Text CurrentPhaseText;
    public Button Button1;

    public GameObject SelectedFigure;

    private string phaseText = "";
    private string currentPlayer = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateCommandTextBar()
    {
        CurrentPhaseText.text = currentPlayer + "-" + phaseText + " phase";
    }

    private void setupShootingButton(Button button)
    {
        button.GetComponent<Text>().text = "Open Fire";
        button.onClick += SelectedFigure.GetComponent<FigureInputManager>().OpenFire();
    }

    public void UpdatePhase(TurnPhase currentPhase)
    {
        phaseText = currentPhase.ToString();
        setupShootingButton(Button1);
        UpdateCommandTextBar();
    }

    public void UpdatePlayer(PlayerId player)
    {
        currentPlayer = player.ToString();
        UpdateCommandTextBar();
    }
}
