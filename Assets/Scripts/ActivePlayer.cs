using UnityEngine;
using System.Collections;

public class ActivePlayer : MonoBehaviour {

	public bool isCurrentPlayer;
	public GameObject otherPlayer;

	// Use this for initialization
	void Start () 
	{
	
	}

	void FixedUpdate()
	{
		if (Input.GetKeyDown (KeyCode.P))
		{
			if (isCurrentPlayer) 
			{
				isCurrentPlayer = false;
			} 
			else if(isCurrentPlayer == false)
			{
				isCurrentPlayer = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isCurrentPlayer == false)
		{
			GetComponent<PlayerScript> ().enabled = false;
		} 
		else
		{
			GetComponent<PlayerScript> ().enabled = true;
		}
		//print (this.name + " " + isCurrentPlayer);
	}
}
