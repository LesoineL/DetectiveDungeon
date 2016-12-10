using UnityEngine;
using System.Collections;

public class Generator
{
    int randomNum;
    float min;
    float max;

    public Generator(float rangeMin,float rangeMax)
    {
        min = rangeMin;
        max = rangeMax;
    }
    public Generator()
    {
        min = -1.0f;
        max = -1.0f;
    }
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public float GenerateNumber()
    {
        if (min == max)
        {
            return 0;
        }
        else
        {
            return Random.Range(min, max);
        }
    }

    public float GenerateFromRange(float rangeMin, float rangeMax)
    {
        return Random.Range(rangeMin, rangeMax);
    }

    public int GenerateFromRange(int rangeMin, int rangeMax)
    {
        return Random.Range(rangeMin, rangeMax);
    }
}
