using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


    public float maxSpeed = 3f;
    private bool facingRight = true;
    private Rigidbody2D rBody;
    public float jumpForce = 700f;
	bool onGround = true;

	// Use this for initialization
	void Start ()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Movement Horizontal
        float move = Input.GetAxis("Horizontal");

        rBody.velocity = new Vector2(move * maxSpeed, rBody.velocity.y);

        if (move > 0 && !facingRight)
        {
            FlipDir();
        }
        else if (move < 0 && facingRight)
        {
            FlipDir();
        }


		//GetControls
		if (Input.GetKeyDown(GetComponent<Controls>().GetKey("jump")))
		{
			if (onGround) 
			{
				rBody.AddForce (new Vector2 (0, jumpForce));
				onGround = false;
			}
		}
		if(Input.GetKeyDown(GetComponent<Controls>().GetKey("interact")))
		{

		}
		if(Input.GetKey(GetComponent<Controls>().GetKey("sprint")))
		{
			maxSpeed = 10;
		}
		else if(Input.GetKeyUp(GetComponent<Controls>().GetKey("sprint")))
		{
			maxSpeed = 3;
		}
    }
    
    void Update()
    {
        
    }
    
    void FlipDir()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	void OnCollisionEnter2D(Collision2D target)
	{
		if (target.collider.tag == "Ground" && target.relativeVelocity.y < -0.01f)
		{
			//print (target.relativeVelocity);
			onGround = true;
		}
	}
}