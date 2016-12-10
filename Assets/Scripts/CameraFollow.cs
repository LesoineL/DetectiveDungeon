using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    /*public GameObject Character;
    private float xPos;
    private float yPos;
    private float camX;
    private float camY;
    
	// Use this for initialization
	void Start ()
    {
        
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        xPos = Character.transform.position.x;
        yPos = Character.transform.position.y;
        camX = this.transform.position.x;
        camY = this.transform.position.y;
        
        /*if (this.transform.position.x < Character.transform.position.x)
        {
            this.transform.Translate(0.05F, 0, 0);
        }
        else if(this.transform.position.x > Character.transform.position.x)
        {
            this.transform.Translate(-0.05F, 0, 0);
        }
        
        this.transform.position = new Vector3(Mathf.Lerp(camX, xPos, 0.01F), Mathf.Lerp(camY, yPos, 0.01F), -10);
        camX = camX - 0.1f;
        camY = camY - 0.1f;
        //this.transform.position = new Vector3((float)Mathf.Round(this.transform.position.x), (float)Mathf.Round(this.transform.position.y), -10);
    }
}*/
	//Variable creation
    public float dampTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    public Transform targetCurr;
	public Transform targetPrev;
	Transform targetTemp;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

	void Update()
	{
		
	}
		
    void FixedUpdate()
    {
		CheckActive ();
        if (targetCurr == true)
        {
            Vector3 point = cam.WorldToViewportPoint(targetCurr.position);
            Vector3 delta = targetCurr.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }

	void CheckActive()
	{
		if (targetCurr.GetComponent<ActivePlayer> ().isCurrentPlayer == false)
		{
			print ("char change");
			targetTemp = targetCurr;
			targetCurr = targetPrev;
			targetPrev = targetTemp;
		}
	}
}
