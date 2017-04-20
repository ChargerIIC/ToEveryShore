using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{

    public Text NameText;
    public Text VehicleDescText;
    public Text MoveStatText;
    public Text MoveTypeStatText;
    public Text ArmorStatText;
    public Text ToHitStatText;
    public Text StructStatText;
    public Text MoraleStatText;
    public Text SkillStatText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void Update(FullFigure figure)
    {
        NameText.text = "Name: " + figure.CommanderName;
        VehicleDescText.text = "Vehicle: " + figure.VehicleName + " " + figure.Chassis;
        MoveStatText.text = "Move: " + figure.Movement.ToString();
        MoveTypeStatText.text = "Move Type: " + figure.MovementType.ToString();
        ArmorStatText.text = "Armor: " + figure.FrontArmor + "/" + figure.SideArmor + "/" + figure.RearArmor;
        ToHitStatText.text = "ToHit: " + figure.ToHit.ToString();
        StructStatText.text = "Struct: " + figure.Struct.ToString();
        SkillStatText.text = "Skill: " + figure.Skill.ToString();
        MoraleStatText.text = "Morale: " + figure.Morale.ToString();
    }
}
