﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that marks any figure that can be selected in the Game UI
/// Inhits from the Third Party Project Highlight
/// </summary>
public class Selectable : HighLight
{

	// Use this for initialization
	void Start ()
	{
	    Initialize();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsSelected
    {
        get { return isHighlighted; }
        set { isHighlighted = value; }
    }
}
