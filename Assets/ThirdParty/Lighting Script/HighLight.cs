/*********************************************************/
/*                                                       */
/* Highlighting system by Javier Nieto                   */
/*                                                       */
/* IMPORTANT!! Remember to apply the highight Shader     */
/* (by default is "Self-Illumin/Diffuse") to any         */
/* object on your scene. Otherwise you will get          */
/* an ugly pink highlight instead if a pretty            */
/* illuminated lighting.                                 */
/*                                                       */
/*********************************************************/

using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class HighLight : MonoBehaviour {

	public Shader highightShader = null;	//Shader used to illuminate object, by default is "Self-Illumin/Diffuse"
	public Transform player;				//Player character. Used to measure distances. Leave null for infinite distance activation
	public float lightDistance=4.0f;		//Minimun distace from player to object needed to switch highlight on
	public float labelDistance=2.0f;		//Minimun distace from player to object needed to show label
	//public String labelToDisplay="LABEL";	//Text to show
    private String labelToDisplay;
	public Color labelColor=Color.white;	//Text color
	public bool outlined=true;				//Set true for display outlined text
	public Color outlineColor=Color.black;	//Outline text color
	public Font textFont = null;			//Text font
	public int fontSize=12;					//Text size
	public AnimationClip animationClip = null;	//Animation to play when highlighted. The clip MUST be on the animation array of th asset

	private List<Shader> originalObjetsShader;
	public bool highlighted=false;
	private GUIStyle style;
	private float dist = 0.0f;
	private Vector3 originalPosition;
	private Quaternion originalRotation;

    private Animation animationComponent;

    public HighLight()
    {
    }

	/* Called when user clicks the object   */
	/* Add you own code to interact with it */
	void OnMouseDown() {
		Debug.Log (gameObject.name + "Clicked.");
	}

	void Start()
	{
	    labelToDisplay = gameObject.name;//TODO: CHange to model name
        animationComponent = gameObject.GetComponent<Animation>();

        if (highightShader==null)
			highightShader=Shader.Find( "Self-Illumin/Diffuse" );

	    style = new GUIStyle
	    {
	        normal = {textColor = labelColor},
	        alignment = TextAnchor.UpperCenter,
	        wordWrap = true,
	        fontSize = fontSize
	    };

	    if(textFont!=null)
			style.font=textFont;
		else
			style.font= (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
	}
	
	//draw text of a specified color, with a specified outline color
	void DrawOutline(Rect position, String text, GUIStyle theStyle, Color outColor, Color inColor){
		var backupStyle = theStyle;
		theStyle.normal.textColor = outColor;
		position.x--;
		GUI.Label(position, text, style);
		position.x +=2;
		GUI.Label(position, text, style);
		position.x--;
		position.y--;
		GUI.Label(position, text, style);
		position.y +=2;
		GUI.Label(position, text, style);
		position.y--;
		theStyle.normal.textColor = inColor;
		GUI.Label(position, text, style);
		theStyle = backupStyle;
	}
	
	void OnGUI () {
		if (player)
			dist = Vector3.Distance (player.position, transform.position);
		
		if (!highlighted || labelToDisplay.Equals ("") || dist>labelDistance)
			return;
		
		float x = Event.current.mousePosition.x;
		float y = Event.current.mousePosition.y;
		if (outlined)
			DrawOutline (new Rect(x-149,y-20,300,60), labelToDisplay, style, outlineColor, labelColor);
		else
			GUI.Label ( new Rect(x-149,y-20,300,60), labelToDisplay, style);
	}
	
	void OnMouseEnter()
	{
		dist = 0.0f;
		
		if (player)
			dist = Vector3.Distance (player.position, transform.position);
		
		if(dist > lightDistance)
			return;

		Component[] renderers;
		renderers = GetComponentsInChildren<Renderer>();
		originalObjetsShader = new List<Shader> ();
		
		foreach (Renderer singleRenderer in renderers)
		foreach (Material singleMaterial in singleRenderer.materials) {
			originalObjetsShader.Add(singleMaterial.shader);
			singleMaterial.shader=highightShader;
		}

		if (animationClip != null) {
			originalPosition=this.transform.position;
			originalRotation=this.transform.rotation;
            animationComponent.Play (animationClip.name);
		}

		highlighted = true;
	}
	
	void OnMouseExit()
	{
		if (!highlighted)
			return;
		
		int i=0;
	    Component[] renderers = GetComponentsInChildren<Renderer>();

	    foreach (Renderer singleRenderer in renderers)
	    {
	        foreach (Material singleMaterial in singleRenderer.materials)
	        {
	            singleMaterial.shader = originalObjetsShader[i++];
	        }
	    }
	    if (animationClip != null) {
            animationComponent.Stop (animationClip.name);
			this.transform.position=originalPosition;
			this.transform.rotation=originalRotation;
		}
		highlighted = false;
	}
}
