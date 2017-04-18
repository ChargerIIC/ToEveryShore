using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour
{
    public Text CurrentPhaseText;

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

    public void UpdatePhase(TurnPhase currentPhase)
    {
        phaseText = currentPhase.ToString();
        UpdateCommandTextBar();
    }

    public void UpdatePlayer(PlayerId player)
    {
        currentPlayer = player.ToString();
        UpdateCommandTextBar();
    }
}
