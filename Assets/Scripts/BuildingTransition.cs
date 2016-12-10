using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BuildingTransition : MonoBehaviour {

    public GameObject character;
    public string nextLevel;
    bool colliding = false;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(colliding == true)
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        print("collided");
        if(other.tag == "Player")
        {
            colliding = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            colliding = false;
        }
    }
}
