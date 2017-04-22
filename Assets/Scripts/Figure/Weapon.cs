using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A DTO Class that manages the attributes of a weapon
/// </summary>
[Serializable]
public class Weapon
{
    public string Name;
    public int RateOfFire;
    public int Penetration;
    public int Damage;
    public int EffectiveRange;
    public int LongRange;
    //TODO: Attributes
}
