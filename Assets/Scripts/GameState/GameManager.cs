﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class maintains the game state. Which player's turn is it, victory conditions, etc.
/// </summary>
public class GameManager : MonoBehaviour
{
    public PlayerId CurrentPlayer;
    public TurnPhase CurrentPhase;

    public GameObject SelectedEnemyObject;

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
        setupSelectedFigureInputControllers();
        UIController.NotifyOfPhaseChange(CurrentPhase);
    }

    private void setupSelectedFigureInputControllers()
    {
        switch (CurrentPhase)
        {
            case TurnPhase.Shooting:
                if (SelectedFriendlyObject == null)
                    break;
                if (SelectedFriendlyObject.GetComponent<MoveInputController>())
                {
                    Destroy(SelectedFriendlyObject.GetComponent<MoveInputController>());
                }
                SelectedFriendlyObject.AddComponent<ShootInputController>();
                break;
            case TurnPhase.Movement:
                if (SelectedFriendlyObject == null)
                    break;
                if (SelectedFriendlyObject.GetComponent<ShootInputController>())
                {
                    Destroy(SelectedFriendlyObject.GetComponent<ShootInputController>());
                }
                SelectedFriendlyObject.AddComponent<MoveInputController>();
                break;
        }
    }

    #region Public Methods

    public void NextPhase()
    {
        switch (CurrentPhase)
        {
            case TurnPhase.Movement:
                CurrentPhase = TurnPhase.Shooting;
                break;
            case TurnPhase.Shooting:
                //TODO: Later this will move to assault and then the other player's turn
                CurrentPhase = TurnPhase.Movement;
                break;
        }

        UpdatePhase();
    }

    public void NotifyOfUnitSelection(GameObject selectedFigure)
    {
        if (selectedFigure.layer == (int) Layers.Friendly)
        {
            if (SelectedFriendlyObject != null)
            {
                SelectedFriendlyObject.GetComponent<Selectable>().IsSelected = false; //deselect previous figure
                if (SelectedFriendlyObject.GetComponent<MoveInputController>())
                {
                    //TODO: Remove Controller Component
                }
            }
            SelectedFriendlyObject = selectedFigure;
            if (CurrentPhase == TurnPhase.Movement)
            {
                SelectedFriendlyObject.AddComponent<MoveInputController>();
            }
            else if (CurrentPhase == TurnPhase.Shooting)
            {
                SelectedFriendlyObject.AddComponent<ShootInputController>();
            }
        }
        else
        {
            SelectedEnemyObject = selectedFigure;
        }
        UIController.NotifyOfUnitChange(selectedFigure);
    }

    #endregion Public Methods

    private GameObject selectedFriendlyFigure;

    public GameObject SelectedFriendlyObject
    {
        get { return selectedFriendlyFigure; }
        set
        {
            selectedFriendlyFigure = value;
            setupSelectedFigureInputControllers();
        }
    }

    private GuiController uiController;
    protected GuiController UIController
    {
        get { return uiController ?? (uiController = FindObjectOfType<GuiController>()); }
    }

}
