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
        button.GetComponentInChildren<Text>().text = "Open Fire";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(selectedFigureOpenFire); //Why the hell doesn't unity accept the normal += sytax ?
    }

    private void selectedFigureOpenFire()
    {
        var inputManager = SelectedFigure.GetComponent<ShootInputController>();
        inputManager.OpenFire();
    }

    private void setupMoveButton(Button button)
    {
        button.GetComponentInChildren<Text>().text = "Move";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(selectedFigureMove); //Why the hell doesn't unity accept the normal += sytax ?
    }

    private void selectedFigureMove()
    {
        var inputManager = SelectedFigure.GetComponent<MoveInputController>();
        inputManager.MoveToWaypoints();
    }

    public void UpdatePhase(TurnPhase currentPhase)
    {
        phaseText = currentPhase.ToString();
        if (currentPhase == TurnPhase.Shooting)
        {
            setupShootingButton(Button1);
        }
        else if (currentPhase == TurnPhase.Movement)
        {
            setupMoveButton(Button1);
        }
        UpdateCommandTextBar();
    }

    public void UpdatePlayer(PlayerId player)
    {
        currentPlayer = player.ToString();
        UpdateCommandTextBar();
    }
}
