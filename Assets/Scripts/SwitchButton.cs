using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchButton : MonoBehaviour {

    private Button button;
    public GameObject character;
    //public KeyCode key;
    public string key;
    private KeyCode keyNew;
    private Text txt;

	// Use this for initialization
	void Start ()
    {
        this.GetComponentInChildren<Text>().text = key;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
