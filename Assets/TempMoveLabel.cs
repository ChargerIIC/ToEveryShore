using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempMoveLabel : MonoBehaviour {

    public GameObject MonitoredObject;
    private FigureInputManager inputManager;

	// Use this for initialization
	void Start () {
        inputManager = MonitoredObject.GetComponent<FigureInputManager>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Move: " + inputManager.moveToSpend / Globals.WorldToGameFactor;

    }
}
