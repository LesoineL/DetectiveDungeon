using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls : MonoBehaviour {

	//Temporary Class -- At some point should move control script to a game manager so that it affects all possible player characters
	//Canvas to gain access to the menu
    public Canvas menu;
	//Dictionary to hold all the controls with their corresponding keys
    private Dictionary<string, KeyCode> controlSet = new Dictionary<string, KeyCode>();

    // Use this for initialization
    void Start ()
    {
        controlSet.Add("jump", KeyCode.Space);
        controlSet.Add("sprint", KeyCode.LeftShift);
        controlSet.Add("interact", KeyCode.E);
    }

	//Allows a control to have a different input key -- Add extra functionality later -- ex. jump1, jump2
    public void ChangeKey(string command, KeyCode key)
    {
        if(controlSet.ContainsKey(command))
        {
            controlSet[command] = key;
        }
    }
    
	//Allows another component to recieve information about a key
    public KeyCode GetKey(string command)
    {
        try
        {
            return controlSet[command];
        }
        catch(KeyNotFoundException kNFE)
        {
            return KeyCode.Escape;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menu.enabled == true)
            {
                menu.enabled = false;
            }
            else
            {
                menu.enabled = true;
            }
        }
	}
}
