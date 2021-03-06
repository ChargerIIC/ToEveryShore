﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour
{
    public Text CurrentPhaseText;

    public Button Button1;
    public Button Button2;
    public Button Button3;

    public FullFigure SelectedFigure;
    public GameObject MovementRingPrefab;
    public GameObject RangeRingPrefab;
    public GameObject WaypointPrefab;

    private string currentPlayer = "";

	// Use this for initialization
	void Start ()
	{
        Button1.gameObject.SetActive(false);
	    Button2.gameObject.SetActive(false);
	    Button3.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void UpdateCommandTextBar()
    {
        CurrentPhaseText.text = currentPlayer;
    }

    private void setupShootingButton()
    {
        var moveController = SelectedFigure.gameObject.GetComponent<MoveInputController>();
        if (moveController != null)
            GameObject.Destroy(moveController);

        var shootController = SelectedFigure.gameObject.AddComponent<ShootInputController>();
        shootController.RingPrefab = RangeRingPrefab;

        //TODO: Button for each weapon
        Button1.gameObject.SetActive(false);
        Button1.GetComponentInChildren<Text>().text = "";

        Button2.gameObject.SetActive(true);
        Button2.GetComponentInChildren<Text>().text = "Open Fire";
        Button2.onClick.RemoveAllListeners();
        Button2.onClick.AddListener(selectedFigureOpenFire); //Why the hell doesn't unity accept the normal += sytax ?

        Button3.gameObject.SetActive(true);
        Button3.GetComponentInChildren<Text>().text = "Cancel";
        Button3.onClick.RemoveAllListeners();
        Button3.onClick.AddListener(resetMenu);
    }

    private void selectedFigureOpenFire()
    {
        var inputManager = SelectedFigure.GetComponent<ShootInputController>();
        inputManager.OpenFire();
    }


    private void setupMoveButton()
    {
        var shootController = SelectedFigure.gameObject.GetComponent<ShootInputController>();
        if (shootController != null)
            GameObject.Destroy(shootController);

        var moveController = SelectedFigure.gameObject.AddComponent<MoveInputController>();
        moveController.RingPreFab = this.MovementRingPrefab;
        moveController.WayPointPrefab = this.WaypointPrefab;

        Button1.gameObject.SetActive(true);
        Button1.GetComponentInChildren<Text>().text = "Move";
        Button1.onClick.RemoveAllListeners();
        Button1.onClick.AddListener(selectedFigureMove); //Why the hell doesn't unity accept the normal += sytax ?

        Button2.gameObject.SetActive(true);
        Button2.GetComponentInChildren<Text>().text = "Reset";
        Button2.onClick.RemoveAllListeners();

        Button3.gameObject.SetActive(true);
        Button3.GetComponentInChildren<Text>().text = "Cancel";
        Button3.onClick.RemoveAllListeners();
        Button3.onClick.AddListener(resetMenu);
    }

    private void selectedFigureMove()
    {
        var inputManager = SelectedFigure.GetComponent<MoveInputController>();
        inputManager.MoveToWaypoints();
    }

    public void UpdateFigure(FullFigure figure)
    {
        SelectedFigure = figure;
        resetMenu();
    }

    void resetMenu()
    {

        //TODO: Update for figure's available actions
        Button1.gameObject.SetActive(true);
        Button1.GetComponentInChildren<Text>().text = "Move";
        Button1.onClick.AddListener(setupMoveButton);

        Button2.gameObject.SetActive(true);
        Button2.GetComponentInChildren<Text>().text = "Shoot";
        Button2.onClick.AddListener(setupShootingButton);

        Button3.gameObject.SetActive(true);
        Button3.GetComponentInChildren<Text>().text = "Ability";
        Button3.onClick.AddListener(setupAbilityButton);
    }

    private void setupAbilityButton()
    {
        //throw new NotImplementedException();
    }

    public void UpdatePlayer(PlayerId player)
    {
        currentPlayer = player.ToString();
        UpdateCommandTextBar();
    }
}
