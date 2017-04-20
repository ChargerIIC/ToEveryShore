using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Per the GDD, this is a stat holding class that represents any figure that has interaction with the rules (tanks, infantry, etc.)
/// </summary>
public class Figure : Model {

    //Movement
    public MoveType MovementType = MoveType.HalfTrack;
    public int Movement = 0;

    //Defensive
    public int ToHit = 1;
    public int Struct = 1;
    public int FrontArmor = 10;
    public int SideArmor = 10;
    public int RearArmor = 10;

    public int Morale = 8;
    public int Skill = 8;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
