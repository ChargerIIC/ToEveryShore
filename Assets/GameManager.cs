using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class maintains the game state. Which player's turn is it, victory conditions, etc.
/// </summary>
public class GameManager : MonoBehaviour
{
    public PlayerId CurrentPlayer;
    public TurnPhase CurrentPhase;

	// Use this for initialization
	void Start () {
		CurrentPlayer = PlayerId.PlayerOne; //Default
	    CurrentPhase = TurnPhase.Movement;
	    UIController.NotifyOfPlayerChange(CurrentPlayer);
	    UpdatePhase();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdatePhase()
    {
        UIController.NotifyOfPhaseChange(CurrentPhase);
    }

    private GuiController uiController;
    protected GuiController UIController
    {
        get { return uiController ?? (uiController = FindObjectOfType<GuiController>()); }
    }

}
