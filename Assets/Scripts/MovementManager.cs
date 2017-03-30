using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{

    public GameObject WaypointPrefab;

    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //We only handle movement right now, but in the future there will be context sensitive options for targeting enemies
    private void ProcessMouseInput(RaycastHit hitData, int layerHit)
    {

    }
}
