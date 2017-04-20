using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayableFigure : Figure {


    #region Stats


    //This is a hidden stat used to track terrain movement costs
    private float movementCost = 1.0f;
    #endregion Stats

    #region Components

    #endregion Components

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
        //TODO: We need to determine how to modify the navmesh for the custom terrain areas
        //NavMeshHit navMeshHit;
        //var pos = new Vector3(transform.position.x, 0, transform.position.z);
        //if (NavMesh.SamplePosition(pos, out navMeshHit, 1.0f, NavMesh.AllAreas))
        //{
        //    print("Nav Mesh: " + navMeshHit.mask);
        //}
    }
}
