using System;
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

    public GameObject CameraObject;
    public GameObject RingPrefab;
    public GameObject WaypointPrefab;

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
                var moveInput = SelectedFriendlyObject.AddComponent<MoveInputController>();
                moveInput.CameraRig = CameraObject;
                moveInput.RingPreFab = RingPrefab;
                moveInput.WayPointPrefab = WaypointPrefab;
                break;
        }
    }

    public void ResolveAttack(Weapon weapon, GameObject target = null)
    {
        if (target == null)
            target = SelectedEnemyObject;

        Debug.Log("Firing " + weapon.Name + " at " + target.name);
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
