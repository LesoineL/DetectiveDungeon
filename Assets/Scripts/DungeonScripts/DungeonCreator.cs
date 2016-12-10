using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonCreator : MonoBehaviour 
{
    Generator numGen = new Generator();

    public int mapHeight;
    public int mapWidth;

    public GameObject[] tileSet;

    public int minRooms;
    public int maxRooms;
    int roomNum;

    public int minRoomWidth;
    public int maxRoomWidth;

    public int minRoomHeight;
    public int maxRoomHeight;

    int[,] tileArray;
    List<Room> rooms = new List<Room>();
    List<Corridor> corrs = new List<Corridor>();

    enum Tiles
    {
        Floor, Wall
    }

	// Use this for initialization
	void Start () 
    {
        tileArray  = new int[mapWidth,mapHeight];
        roomNum = numGen.GenerateFromRange(minRooms, maxRooms);

        //Generates the rooms
        for (int i = 0; i < roomNum; i++)
        {
            rooms.Add(new Room(mapWidth, mapHeight, minRoomWidth, maxRoomWidth, minRoomHeight, maxRoomHeight));
        }

        ConnectRooms();

        //Places the rooms
        for (int count = 0; count < rooms.Count; count++)
        {
            for (int x = rooms[count].minXPos; x <= rooms[count].maxXPos; x++)
            {
                for (int y = rooms[count].minYPos; y <= rooms[count].maxYPos; y++)
                {
                    Instantiate(tileSet[0], new Vector3((float)x, (float)y, 0.0f), Quaternion.identity);
                }
            }
        }

        //Places the corridors
        for (int count = 0; count < corrs.Count; count++)
        {
            if (corrs[count].EndX > -1 && corrs[count].EndY > -1)
            {
                if (corrs[count].StartX <= corrs[count].EndX)
                {
                    for (int x = corrs[count].StartX; x <= corrs[count].EndX; x++)
                    {
                        if (corrs[count].StartY < corrs[count].EndY)
                        {
                            for (int y = corrs[count].StartY; y <= corrs[count].EndY; y++)
                            {
                                Instantiate(tileSet[0], new Vector3((float)x, (float)y, 0.0f), Quaternion.identity);
                            }
                        }
                        else
                        {
                            for (int y = corrs[count].StartY; y >= corrs[count].EndY; y--)
                            {
                                Instantiate(tileSet[0], new Vector3((float)x, (float)y, 0.0f), Quaternion.identity);
                            }
                        }
                    }
                }
                else
                {
                    for (int x = corrs[count].StartX; x >= corrs[count].EndX; x--)
                    {
                        if (corrs[count].StartY < corrs[count].EndY)
                        {
                            for (int y = corrs[count].StartY; y <= corrs[count].EndY; y++)
                            {
                                Instantiate(tileSet[0], new Vector3((float)x, (float)y, 0.0f), Quaternion.identity);
                            }
                        }
                        else
                        {
                            for (int y = corrs[count].StartY; y >= corrs[count].EndY; y--)
                            {
                                Instantiate(tileSet[0], new Vector3((float)x, (float)y, 0.0f), Quaternion.identity);
                            }
                        }
                    }
                }
            }
        }

        foreach (Corridor corr in corrs)
        {
            Debug.Log(corr);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    //Creates the rooms
    public void ConnectRooms()
    {
        Corridor tempCorr;

        for (int i = 0; i < rooms.Count; i++)
        {
            for (int count = 0; count < rooms.Count; count++)
            {
                if (i != count)
                {
                    tempCorr = new Corridor(rooms[i], rooms[count]);
                    AddCorridors(tempCorr);
                }
            }
        }


        /*
        //Creates the initial room connections
        for(int i = 0; i < rooms.Count; i++)
        {
            for (int count = 0; count < rooms.Count; count++)
            {
                tempCorr = new Corridor(rooms[i],rooms[count]);
                corrs.Add(tempCorr);
                if (null != tempCorr.ConnectingCorridorNext)
                {
                    corrs.Add(tempCorr.ConnectingCorridorNext);
                }
            }
        }
        */
    }

    void AddCorridors(Corridor currentCorr)
    {
        Corridor nextCorr;

        if (currentCorr.ConnectingCorridorNext != null)
        {
            nextCorr = currentCorr.ConnectingCorridorNext;

            AddCorridors(nextCorr);
        }

        corrs.Add(currentCorr);
    }
}
