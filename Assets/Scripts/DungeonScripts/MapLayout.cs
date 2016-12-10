using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapLayout : MonoBehaviour 
{
    private Generator rangeGenerator;
    private int[,] mapSize;
    private List<Vector2> roomPosList;
    public int maxMapHeight;
    public int maxMapWidth;
    public int numberOfRooms;
    public float minRoomWidth;
    public float maxRoomWidth;
    public float minRoomHeight;
    public float maxRoomHeight;
    public GameObject tile;

	// Use this for initialization
	void Start () 
    {
        rangeGenerator = new Generator();
        if (maxMapHeight < 0 || maxMapWidth < 0)
        {
            Debug.Log("Warning:  maxHeight or maxWidth must be positive values.");
            Debug.Break();
        }
            
        roomPosList = new List<Vector2>();

        for (int count = 0; count < numberOfRooms; count++)
        {
            GenerateAndPlaceRoom(-maxMapWidth / 2f, maxMapWidth / 2f, -maxMapHeight / 2f, maxMapHeight / 2f);
            Debug.Log("HI");
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    void GenerateAndPlaceRoom(float minX, float maxX, float minY, float maxY)
    {
        GameObject newRoom = Instantiate(tile);
        Vector2 position = newRoom.transform.position;
        Vector3 scale = new Vector3(rangeGenerator.GenerateFromRange(minRoomWidth, maxRoomWidth), rangeGenerator.GenerateFromRange(minRoomHeight, maxRoomHeight), 0);

        position.x = rangeGenerator.GenerateFromRange(minX, maxX);
        position.y = rangeGenerator.GenerateFromRange(minY, maxY);


        newRoom.transform.localScale = scale;

        newRoom.transform.position = position;
    }
}
