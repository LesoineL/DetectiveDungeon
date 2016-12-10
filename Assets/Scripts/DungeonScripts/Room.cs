using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room
{
    public int roomWidth;
    public int roomHeight;

    List<Room> connectingRooms = new List<Room>();

    //far left x point
    public int minXPos;

    //far right x point
    public int maxXPos;

    //topmost y point
    public int minYPos;

    //bottommost y point
    public int maxYPos;

    Generator numGen = new Generator();

    public Room(int mapWidth, int mapHeight, int minRoomWidth, int maxRoomWidth, int minRoomHeight, int maxRoomHeight)
    {
        roomWidth = numGen.GenerateFromRange(minRoomWidth, maxRoomWidth);
        roomHeight = numGen.GenerateFromRange(minRoomHeight, maxRoomHeight);

        minXPos = numGen.GenerateFromRange(0, mapWidth);
        minYPos = numGen.GenerateFromRange(0, mapHeight);

        maxXPos = minXPos + roomWidth;
        maxYPos = minYPos + roomHeight;

        Debug.Log("Width:  " + roomWidth + "  Height:  " + roomHeight + "  minX:  " + minXPos + "  maxX:  " + maxXPos + "  minY:  " + minYPos + "  maxY:  " + maxYPos);
    }

    public List<Room> ConnectingRooms
    {
        get { return connectingRooms; }
        set{ connectingRooms = value; }
    }
        
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
