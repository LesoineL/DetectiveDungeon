using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Corridor
{
    //Number generator
    Generator numGen = new Generator();

    //List of connecting corridors
    Corridor connectingCorridorNext;

    //Direction uses int of 0-3
    //left, up, right, down
    int direction;

    //Gets the case to generate a point from
    int caseResult;

    //length of corridor in tiles
    int length;

    //Ints for the positions
    int startX;
    int endX;
    int startY;
    int endY;

    //Weld point placed along the y-axis (Connects two x coordinates)
    int xPosWeld = -1;

    //Weld point placed along the x-axis (Connects two y coordinates)
    int yPosWeld = -1;


    /* * * * * * **
     *Constructors* 
     * * * * * * **/

    //Connects two rooms with corridors
    public Corridor(Room startRoom, Room endRoom)
    {
        //Tries a single corridor connection
        CalcConnection(startRoom, endRoom);

        //Check to see if the rooms overlap
        if(length > 0)
        {
            
        }

        //Could not connect with one corridor
        if (xPosWeld <= -1 && yPosWeld <= -1)
        {
            int corrNum = numGen.GenerateFromRange(2,5);

            GenerateMultiCorr(startRoom, endRoom, corrNum);

            startRoom.ConnectingRooms.Add(endRoom);
            endRoom.ConnectingRooms.Add(startRoom);
        }
        /*
        //The two rooms are connected
        if (xPosWeld > -1 || yPosWeld > -1)
        {
            startRoom.ConnectingRooms.Add(endRoom);
            endRoom.ConnectingRooms.Add(startRoom);
        }
        */
    }

    //Connects a corridor to an end room
    public Corridor(Corridor secondLastCorr, Room endRoom)
    {
        FinishCorrConnections(secondLastCorr, endRoom);
    }

    //Generates the next Corridor in a chain of Corridors
    public Corridor(Corridor prevCorridor, Room endRoom, int numCorrsLeft)
    {

        int corrCount = numCorrsLeft;

        if (corrCount == 2)
        {
            GenerateLastCorrs(prevCorridor, endRoom);
        }
        else
        {
            //Generates next Corridor in a set
            GenerateMultiCorr(prevCorridor, endRoom, numCorrsLeft);
            /*/Creates the next Corridor in the set
            corrCount--;
            Corridor nextCorr = new Corridor(this, endRoom, corrCount);
            connectingCorridorNext = nextCorr;*/
        }
    }

    //Generates a corridor from a start room
    public Corridor(Room startRoom)
    {

    }

    //Generates a corridor from an existing corridor
    public Corridor(Corridor connectingCorridor)
    {
        direction = numGen.GenerateFromRange(0, 3);
    }

    /* * * * * * **
     *-Properties-* 
     * * * * * * **/

    public Corridor ConnectingCorridorNext
    {
        get { return connectingCorridorNext; }
        set { connectingCorridorNext = value; }
    }
    
    public int Direction
    {
        get { return direction; }
    }

    public int StartX
    {
        get { return startX; }
    }

    public int StartY
    {
        get { return startY; }
    }
    public int EndX
    {
        get { return endX; }
    }

    public int EndY
    {
        get { return endY; }
    }

    /* * * * * * * *
     *---Methods---* 
     * * * * * * * */

    //Caclulates the direction the corridor needs to be in and a point of connection
    public void CalcConnection(Room room1, Room room2)
    {
        //Room1 is right of Room2
        if (room1.minXPos > room2.maxXPos)
        {
            //Room1 starts before Room2 on the Y
            if (room1.minYPos <= room2.minYPos)
            {
                //Room1 ends after Room2 starts and before Room2 ends on the Y  
                if (room1.maxYPos >= room2.minYPos && room1.maxYPos <= room2.maxYPos)
                {
                    direction = 3;
                    caseResult = 0;

                    yPosWeld = GenRandomPoint(room1.minYPos, room1.maxYPos, room2.minYPos, room2.maxYPos, caseResult);
                    startY = yPosWeld;
                    endY = yPosWeld;

                    startX = room2.maxXPos;
                    endX = room1.minXPos;

                    length = endX - startX;

                    Debug.Log("WeldY:  " + yPosWeld + "  StartX:  " + startX + "  EndX:  " + endX + "  Length:  " + length);
                }
                //Room1 ends after Room2 starts and after Room2 ends on the Y
                else if (room1.maxYPos >= room2.minYPos && room1.maxYPos >= room2.maxYPos)
                {
                    direction = 3;
                    caseResult = 1;

                    yPosWeld = GenRandomPoint(room1.minYPos, room1.maxYPos, room2.minYPos, room2.maxYPos, caseResult);
                    startY = yPosWeld;
                    endY = yPosWeld;

                    startX = room2.maxXPos;
                    endX = room1.minXPos;

                    length = endX - startX;

                    Debug.Log("WeldY:  " + yPosWeld + "  StartX:  " + startX + "  EndX:  " + endX + "  Length:  " + length);
                }
            }
        }
        //Room1 is left of Room2
        else if (room1.maxXPos < room2.minXPos)
        {
            //Room1 starts before Room2 on the Y
            if (room1.minYPos <= room2.minYPos)
            {
                //Room1 ends after Room2 starts and before Room2 ends on the Y  
                if (room1.maxYPos >= room2.minYPos && room1.maxYPos <= room2.maxYPos)
                {
                    direction = 1;
                    caseResult = 2;

                    yPosWeld = GenRandomPoint(room1.minYPos, room1.maxYPos, room2.minYPos, room2.maxYPos, caseResult);
                    startY = yPosWeld;
                    endY = yPosWeld;

                    startX = room1.maxXPos;
                    endX = room2.minXPos;

                    length = endX - startX;

                    Debug.Log("WeldY:  " + yPosWeld + "  StartX:  " + startX + "  EndX:  " + endX + "  Length:  " + length);
                }
                //Room1 ends after Room2 starts and after Room2 ends on the Y
                else if (room1.maxYPos >= room2.minYPos && room1.maxYPos >= room2.maxYPos)
                {
                    direction = 1;
                    caseResult = 3;

                    yPosWeld = GenRandomPoint(room1.minYPos, room1.maxYPos, room2.minYPos, room2.maxYPos, caseResult);
                    startY = yPosWeld;
                    endY = yPosWeld;

                    startX = room1.maxXPos;
                    endX = room2.minXPos;

                    length = endX - startX;

                    Debug.Log("WeldY:  " + yPosWeld + "  StartX:  " + startX + "  EndX:  " + endX + "  Length:  " + length);
                }
            }
        }
        //Room1 is above Room2
        else if (room1.minYPos > room2.maxYPos || room1.maxYPos < room2.minYPos)
        {
            //Room1 starts before Room2 on the X
            if (room1.minXPos <= room2.minXPos)
            {
                //Room1 ends after Room2 starts but before Room2 ends on the X
                if (room1.maxXPos >= room2.minXPos && room1.maxXPos <= room2.maxXPos)
                {
                    direction = 2;
                    caseResult = 4;

                    xPosWeld = GenRandomPoint(room1.minXPos, room1.maxXPos, room2.minXPos, room2.maxXPos, caseResult);
                    startX = xPosWeld;
                    endX = xPosWeld;

                    startY = room2.maxYPos;
                    endY = room1.minYPos;

                    length = endY - startY;

                    Debug.Log("WeldX:  " + xPosWeld + "  StartY:  " + startY + "  EndY:  " + endY + "  Length:  " + length);
                }
                //Room1 ends after Room2 starts and ends on the X
                else if(room1.maxXPos >= room2.minXPos && room1.maxXPos >= room2.maxXPos)
                {
                    direction = 2;
                    caseResult = 5;

                    xPosWeld = GenRandomPoint(room1.minXPos, room1.maxXPos, room2.minXPos, room2.maxXPos, caseResult);
                    startX = xPosWeld;
                    endX = xPosWeld;

                    startY = room2.maxYPos;
                    endY = room1.minYPos;

                    length = endY - startY;

                    Debug.Log("WeldX:  " + xPosWeld + "  StartY:  " + startY + "  EndY:  " + endY + "  Length:  " + length);
                }
            }
        }
        /*Room1 is below Room2
        else if (room1.maxYPos < room2.minYPos)
        {

        }*/

        //Cannot connect rooms with a single corridor
        if (xPosWeld == -1 && yPosWeld == -1)
        {
            
            /*
            if (room1.ConnectingRooms.Count < 2 || room2.ConnectingRooms.Count < 2)
            {
                //room1 is to the right of room2
                if (room1.minXPos > room2.maxXPos)
                {
                    //room1 is above room2
                    if (room1.minYPos > room2.maxYPos)
                    {
                        direction = numGen.GenerateFromRange(2, 3);
                        //Corridor will go up
                        if (direction == 2)
                        {
                            xPosWeld = numGen.GenerateFromRange(room1.minXPos, room1.maxXPos);
                            startX = xPosWeld;
                            endX = xPosWeld;

                            startY = room1.maxYPos;

                            endY = numGen.GenerateFromRange(room2.minYPos, room2.maxYPos);
                            length = endY - startY;
                            Debug.Log("WeldX:  " + xPosWeld + "  StartY:  " + startY + "  EndY:  " + endY + "  Length:  " + length);
                        }
                    //Corridor will go right
                    else
                        {
                            yPosWeld = numGen.GenerateFromRange(room1.minYPos, room1.maxYPos);
                            startY = yPosWeld;
                            endY = yPosWeld;

                            startX = room1.minXPos;

                            endX = numGen.GenerateFromRange(room2.minXPos, room2.maxXPos);
                            length = endX - startX;
                            Debug.Log("WeldY:  " + yPosWeld + "  StartX:  " + startX + "  EndX:  " + endX + "  Length:  " + length);
                        }
                    }
                    //room1 is below room2
                    else if (room1.minYPos < room2.minYPos)
                      {
                        direction = numGen.GenerateFromRange(3, 4);
                        //Corridor will go right
                        if (direction == 3)
                        {
                            yPosWeld = numGen.GenerateFromRange(room1.minYPos, room1.maxYPos);
                            startY = yPosWeld;
                            endY = yPosWeld;

                            startX = room1.minXPos;

                            endX = numGen.GenerateFromRange(room2.minXPos, room2.maxXPos);
                            length = endX - startX;
                            Debug.Log("WeldY:  " + yPosWeld + "  StartX:  " + startX + "  EndX:  " + endX + "  Length:  " + length);
                        }
                    //Corridor will go down
                    else
                        {
                            xPosWeld = numGen.GenerateFromRange(room1.minXPos, room1.maxXPos);
                            startX = xPosWeld;
                            endX = xPosWeld;

                            startY = room1.minYPos;

                            endY = numGen.GenerateFromRange(room2.minYPos, room2.maxYPos);
                            length = endY - startY;
                            Debug.Log("WeldX:  " + xPosWeld + "  StartY:  " + startY + "  EndY:  " + endY + "  Length:  " + length);
                        }
                    }
                    Corridor firstHalf = this;
                    Corridor secondHalf = new Corridor(firstHalf,room2);
                    connectingCorridorNext = secondHalf;
                }

            }
            else
            {
                Debug.Log("Error generating connection point.  Room has too many connections.");
                Debug.Break();
            }

            /*
            Debug.Log("Error generating connection point.  Weld not within map boundries.");
            Debug.Break();

            */
        }

        /*
        //room1 is left of room2
        if (room1.maxXPos <= room2.minXPos && !(room1.minYPos > room2.maxYPos) && !(room2.minYPos > room1.minYPos))
        {
            direction = 1;

            //xPosWeld = GenRandomPoint(room1.minXPos, room1.maxXPos, room2.minXPos, room2.maxXPos);
            yPosWeld = GenRandomPoint(room1.minYPos, room1.maxYPos, room2.minYPos,room2.maxYPos);
            startY = yPosWeld;
            endY = yPosWeld;

            startX = room1.maxXPos;
            endX = room2.minXPos;

            length = endX - startX;

            Debug.Log("WeldY:  " + yPosWeld + "  StartX:  " + startX + "  EndX:  " + endX + "  Length:  " + length);
        }

        //room1 is right of room2
        else if (room1.minXPos >= room2.maxXPos && !(room1.minYPos > room2.maxYPos) && !(room2.minYPos > room1.minYPos))
        {
            direction = 3;

            //xPosWeld = GenRandomPoint(room1.minXPos, room1.maxXPos, room2.minXPos, room2.maxXPos);
            yPosWeld = GenRandomPoint(room1.minYPos, room1.maxYPos, room2.minYPos,room2.maxYPos);
            startY = yPosWeld;
            endY = yPosWeld;

            startX = room2.maxXPos;
            endX = room1.minXPos;

            length = endX - startX;

            Debug.Log("WeldY:  " + yPosWeld + "  StartX:  " + startX + "  EndX:  " + endX + "  Length:  " + length);
        }

        //room1 is below room2
        else if (room1.maxYPos <= room2.minYPos && !(room1.minXPos > room2.maxXPos) && !(room2.minXPos > room1.minXPos))
        {
            direction = 0;

            //yPosWeld = GenRandomPoint(room1.minYPos, room1.maxYPos, room2.minYPos, room2.maxYPos);
            xPosWeld = GenRandomPoint(room1.minXPos, room1.maxXPos, room2.minXPos, room2.maxXPos);
            startX = xPosWeld;
            endX = xPosWeld;

            startY = room1.maxYPos;
            endY = room2.minXPos;

            length = endY - startY;

            Debug.Log("WeldX:  " + xPosWeld + "  StartY:  " + startY + "  EndY:  " + endY + "  Length:  " + length);
        }

        //room1 is above room2
        else if (room1.minYPos >= room2.maxYPos && !(room1.minXPos > room2.maxXPos) && !(room2.minXPos > room1.minXPos))
        {
            direction = 2;
            //yPosWeld = GenRandomPoint(room1.minYPos, room1.maxYPos, room2.minYPos, room2.maxYPos);
            xPosWeld = GenRandomPoint(room1.minXPos, room1.maxXPos, room2.minXPos, room2.maxXPos);
            startX = xPosWeld;
            endX = xPosWeld;

            startY = room2.maxYPos;
            endY = room1.minYPos;

            length = endY - startY;

            Debug.Log("WeldX:  " + xPosWeld + "  StartY:  " + startY + "  EndY:  " + endY + "  Length:  " + length);
        }

        if (xPosWeld == -1 && yPosWeld == -1)
        {
            Debug.Log("Error generating connection point.  Weld not within map boundries.");
            Debug.Break();
        }
        */
        /*
        else
        {
            //Assigns the value of the length
            switch (direction)
            {
                case 0:
                    length = room2.maxYPos - room1.minYPos;
                    break;
                case 1:
                    length = room2.minXPos - room1.maxXPos;
                    break;
                case 2:
                    length = room1.maxYPos - room2.minYPos;
                    break;
                case 3:
                    length = room1.minXPos - room2.maxXPos;
                    break;
                default:
                    length = -1;
                    break;
            }
        }
        */
    }

    //Generates a point to use as a connection between two rooms that is within both of their ranges
    public int GenRandomPoint(int min1, int max1, int min2, int max2, int caseR)
    {
        int point = -1;

        /*
        //Case 1
        //second room is smaller than the first room and within it's min and max range
        if (min1 >= min2 && max1 >= max2)
        {
            point = numGen.GenerateFromRange(min2, max2);
        }
        //Case 2
        //first room is smaller than the second room and within it's min and max range
        else if (min2 >= min1 && max2 >= max1)
        {
            point = numGen.GenerateFromRange(min1, max1);
        }
        //Case 3
        //First room starts before the second room but ends after the second room starts and before the second room ends
        else if (min1 < min2)
        {
            if (max1 >= min2 && max1 <= max2)
            {
                point = numGen.GenerateFromRange(min2, max1);
            }
        }
        //Case 4
        //Second room starts before the first room but ends after the first room starts and before the first room ends
        else if (min2 <= min1)
        {
            if (max2 >= min1 && max2 <= max1)
            {
                point = numGen.GenerateFromRange(min1, max2);
            }
        }
        */

        switch (caseR)
        {
            //Right of Room2 and ends before Room2 ends on Y. Starts before Room2
            case 0:
                point = numGen.GenerateFromRange(min2, max1);
                return point;
            
            //Right of Room2 and ends after Room2 ends on Y.  Starts before Room2
            case 1:
                point = numGen.GenerateFromRange(min2, max2);
                return point;
            
            //Left of Room2 and ends before Room2 ends on Y. Starts before Room2
            case 2:
                point = numGen.GenerateFromRange(min2, max1);
                return point;
            
            //Left of Room2 and ends after Room2 ends on Y.  Starts before Room2
            case 3:
                point = numGen.GenerateFromRange(min2, max2);
                return point;

            //Above Room2 and ends before Room2 ends on X. Starts before Room2
            case 4:
                point = numGen.GenerateFromRange(min2, max1);
                return point;

            //Above of Room2 and ends after Room2 ends on X.  Starts before Room2
            case 5:
                point = numGen.GenerateFromRange(min2, max2);
                return point;
            default:
                return -1;
        }  
    }

    void GenerateMultiCorr(Room startRoom, Room endRoom, int corrCount)
    {
        int numCorrs = corrCount;
        int side = -1;

        if (numCorrs > 2)
        {
            //Choose a random direction
            direction = numGen.GenerateFromRange(0, 3);

            //Create the first corridor of random length from the direciton
            switch (direction)
            {
                //left
                case 0:
                    //Weld on Y
                    yPosWeld = numGen.GenerateFromRange(startRoom.minYPos, startRoom.maxYPos);
                    startY = yPosWeld;
                    endY = yPosWeld;

                    //choose a length for the corridor to be
                    length = numGen.GenerateFromRange(2, 10);

                    //Set the X coordinates
                    startX = startRoom.minXPos;
                    endX = startX - length;
                    break;
                //up
                case 1:
                    //Weld on X
                    xPosWeld = numGen.GenerateFromRange(startRoom.minXPos, startRoom.maxXPos);
                    startX = xPosWeld;
                    endX = xPosWeld;

                    //choose length for the corridor
                    length = numGen.GenerateFromRange(2, 10);

                    //Set Y coordinates
                    startY = startRoom.maxYPos;
                    endY = startY + length;
                    break;
               //right
                case 2:
                    //Weld on Y
                    yPosWeld = numGen.GenerateFromRange(startRoom.minYPos, startRoom.maxYPos);
                    startY = yPosWeld;
                    endY = yPosWeld;

                    //choose a length for the corridor to be
                    length = numGen.GenerateFromRange(2, 10);

                    //Set the X coordinates
                    startX = startRoom.maxXPos;
                    endX = startX + length;
                    break;
                //down
                case 3:
                    //Weld on X
                    xPosWeld = numGen.GenerateFromRange(startRoom.minXPos, startRoom.maxXPos);
                    startX = xPosWeld;
                    endX = xPosWeld;

                    //choose length for the corridor
                    length = numGen.GenerateFromRange(2, 10);

                    //Set Y coordinates
                    startY = startRoom.minYPos;
                    endY = startY - length;
                    break;
            }

            numCorrs--;
            Corridor next = new Corridor(this, endRoom, numCorrs);
            connectingCorridorNext = next;
        }
        else
        {
            //Room1 is right of Room2
            if (startRoom.minXPos > endRoom.maxXPos)
            {
                //Room1 is above Room2
                if (startRoom.minYPos > endRoom.maxYPos)
                {
                    side = numGen.GenerateFromRange(1, 2);
                }
                //Room1 is below Room2
                else if (startRoom.maxYPos < endRoom.minYPos)
                {
                    side = numGen.GenerateFromRange(2, 3);
                }
            }
            //Room1 is left of Room2
            else
            {
                //Room1 is above Room2
                if (startRoom.minYPos > endRoom.maxYPos)
                {
                    side = numGen.GenerateFromRange(0, 1);
                }
                //Room1 is below Room2
                else if (startRoom.maxYPos < endRoom.minYPos)
                {
                    side = numGen.GenerateFromRange(0, 3);

                    if (side == 1)
                    {
                        side = 0;
                    }
                    else if (side == 2)
                    {
                        side = 3;
                    }
                }
            }

            switch (side)
            {
                //Corridor goes left
                case 0:
                    yPosWeld = numGen.GenerateFromRange(startRoom.minYPos, startRoom.maxYPos);
                    startY = yPosWeld;
                    endY = yPosWeld;
                    startX = startRoom.minXPos;
                    endX = startX;

                    GenerateLastCorrs(this, endRoom);
                    break;
                
                //Corridor goes up
                case 1:
                    xPosWeld = numGen.GenerateFromRange(startRoom.minXPos, startRoom.maxXPos);
                    startX = xPosWeld;
                    endX = xPosWeld;
                    startY = startRoom.maxYPos;
                    endY = startY;

                    GenerateLastCorrs(this, endRoom);
                    break;
                
                //Corridor goes right
                case 2:
                    yPosWeld = numGen.GenerateFromRange(startRoom.minYPos, startRoom.maxYPos);
                    startY = yPosWeld;
                    endY = yPosWeld;
                    startX = startRoom.maxXPos;
                    endX = startX;

                    GenerateLastCorrs(this, endRoom);
                    break;
                
                //Corridor goes down
                case 3:
                    xPosWeld = numGen.GenerateFromRange(startRoom.minXPos, startRoom.maxXPos);
                    startX = xPosWeld;
                    endX = xPosWeld;
                    startY = startRoom.minYPos;
                    endY = startY;

                    GenerateLastCorrs(this, endRoom);
                    break;
                default:
                    break;
            }
        }
    }

    //Generates the remaining corridors from the initial multiCorr gen
    void GenerateMultiCorr(Corridor prevCorr, Room endRoom, int corrCount)
    {
        //Get the numCorrs
        int numCorrs = corrCount;

        //Set up the initial points
        startX = prevCorr.EndX;
        startY = prevCorr.EndY;

        //Choose a random direction that does not retrace on the previous corridor
        //Moved right last
        if (prevCorr.EndX > prevCorr.StartX)
        {
            direction = numGen.GenerateFromRange(1, 3);
        }
        //Moved left last
        else if (prevCorr.EndX < prevCorr.StartX)
        {
            direction = numGen.GenerateFromRange(0, 3);
            if (direction == 2)
            {
                int rand = numGen.GenerateFromRange(0, 1);
                if (rand == 0)
                {
                    direction = 1;
                }
                else
                {
                    direction = 3;
                }
            }
        }
        //Move up last
        else if (prevCorr.EndY > prevCorr.StartY)
        {
            direction = numGen.GenerateFromRange(0, 2);
        }
        //Moved down last
        else if(prevCorr.EndY < prevCorr.StartY)
        {
            direction = numGen.GenerateFromRange(0, 3);
            if (direction == 1)
            {
                int rand = numGen.GenerateFromRange(0, 1);
                if (rand == 0)
                {
                    direction = 0;
                }
                else
                {
                    direction = 2;
                }
            }
        }
        else
        {
            Debug.Log("Error with prevCorr coords.");
        }

        //Switch based off direction
        switch (direction)
        {
            //left
            case 0:
                yPosWeld = startY;
                endY = yPosWeld;

                //choose length for the corridor
                length = numGen.GenerateFromRange(2, 10);

                //set X coordinate
                endX = startX - length;
                break;
            //up
            case 1:
                xPosWeld = startX;
                endX = xPosWeld;

                //choose length for the corridor
                length = numGen.GenerateFromRange(2, 10);

                //set Y coordinate
                endY = startY + length;
                break;
            //right
            case 2:
                yPosWeld = startY;
                endY = yPosWeld;

                //choose length for the corridor
                length = numGen.GenerateFromRange(2, 10);

                //set X coordinate
                endX = startX + length;
                break;
            //down
            case 3:
                xPosWeld = startX;
                endX = xPosWeld;

                //choose length for the corridor
                length = numGen.GenerateFromRange(2, 10);

                //set Y coordinate
                endY = startY - length;
                break;
            default:
                Debug.Log("Error occurred generating corridor with " + corrCount + " corridors left.");
                break;
        }

        numCorrs--;
        Corridor next = new Corridor(this, endRoom, numCorrs);
        connectingCorridorNext = next;
    }

    //Creates the second to last corridor in a chain
    void GenerateLastCorrs(Corridor prevCorr, Room endRoom)
    {
        /*
        startX = prevCorr.endX;
        startY = prevCorr.endY;
        endX = prevCorr.endX;
        endY = prevCorr.endY;

        //Checks to see if it needs to move in a specific direction
        //Starts to the left of the Room
        if (startX < endRoom.minXPos || startX > endRoom.maxXPos)
        {
            //above or below
            if (startY > endRoom.maxYPos || startY < endRoom.minYPos)
            {
                //Corridor will go up or down
                xPosWeld = startX;

                endY = numGen.GenerateFromRange(endRoom.minYPos, endRoom.maxYPos);

                //Creates the last corridor
                Corridor final = new Corridor(this, endRoom);
                connectingCorridorNext = final;
            }
            //Y starts within range of the room:  left
            else
            {
                FinishCorrConnections(this, endRoom);
            }
        }
        //X is within range of the room
        //Starts above or below the Room
        else if (startY < endRoom.minYPos || startY > endRoom.maxYPos)
        {
            FinishCorrConnections(this, endRoom);
        }

        //Both are within range of the Room, nothing needed
        */

        startX = prevCorr.EndX;
        startY = prevCorr.EndY;
        endX = prevCorr.EndX;
        endY = prevCorr.EndY;

        //Default direction value in case there is no needed direction
        direction = -1;

        //Starts to the left
        if (startX < endRoom.maxXPos)
        {
            //Also starts above the room
            if (startY > endRoom.maxYPos)
            {
                //Check to see if the previous corridor moved left or up
                if (prevCorr.Direction != 0 && prevCorr.Direction != 1)
                {
                    //If not then it is safe to move in the opposite directions
                    direction = numGen.GenerateFromRange(2, 3);
                }
                //Otherwise readjust
                else
                {
                    if (prevCorr.Direction == 0)
                    {
                        //Down instead of right
                        direction = 3;
                    }
                    else
                    {
                        //Right instead of down
                        direction = 2;
                    }
                }
            }
            //Also starts below the room
            else if (startY < endRoom.minYPos)
            {
                //Check to see if the previous corridor moved left or down
                if (prevCorr.Direction != 0 && prevCorr.Direction != 3)
                {
                    //If not then it is safe to move in the opposite directions
                    direction = numGen.GenerateFromRange(1, 2);
                }
                //Otherwise readjust
                else
                {
                    if (prevCorr.Direction == 0)
                    {
                        //Up instead of right
                        direction = 1;
                    }
                    else
                    {
                        //Right instead of up
                        direction = 2;
                    }
                }
            }
            //Y starts within range of the room:  left
            else
            {
                FinishCorrConnections(this, endRoom);
            }
        }
        //Starts to the right
        else if (startX > endRoom.maxXPos)
        {
            //Also starts above the room
            if (startY > endRoom.maxYPos)
            {
                //Check to see if the previous corridor moved right or up
                if (prevCorr.Direction != 1 && prevCorr.Direction != 2)
                {
                    //If not then it is safe to move in the opposite directions
                    direction = numGen.GenerateFromRange(0, 3);

                    if (direction == 1)
                    {
                        direction = 0;
                    }
                    else if (direction == 2)
                    {
                        direction = 3;
                    }
                }
                //Otherwise readjust
                else
                {
                    if (prevCorr.Direction == 2)
                    {
                        //Down instead of left
                        direction = 3;
                    }
                    else
                    {
                        //Left instead of down
                        direction = 0;
                    }
                }
            }
            //Also starts below the room
            else if (startY < endRoom.minYPos)
            {
                //Check to see if the previous corridor moved right or down
                if (prevCorr.Direction != 2 && prevCorr.Direction != 3)
                {
                    //If not then it is safe to move in the opposite directions
                    direction = numGen.GenerateFromRange(0, 1);
                }
                else
                {
                    if (prevCorr.Direction == 2)
                    {
                        //Up instead of left
                        direction = 1;
                    }
                    else
                    {
                        //Left instead of up
                        direction = 0;
                    }
                }
            }
            //Y starts within range of the room:  right
            else
            {
                FinishCorrConnections(this, endRoom);
            }
        }
        //X is within range of the room
        //Starts above or below the Room
        else if(startY < endRoom.minYPos || startY > endRoom.maxYPos)
        {
            FinishCorrConnections(this, endRoom);
        }

        //Variable for the final corridor if needed
        Corridor final;

        switch (direction)
        {
            //No need to generate this corridor
            case -1:
                break;
            //left
            case 0:
                yPosWeld = startY;
                endY = yPosWeld;

                //set X coordinate
                endX = numGen.GenerateFromRange(endRoom.minXPos, endRoom.maxXPos);

                //Creates the last corridor
                final = new Corridor(this, endRoom);
                connectingCorridorNext = final;
                break;
            //up
            case 1:
                xPosWeld = startX;
                endX = xPosWeld;

                //set Y coordinate
                endY = numGen.GenerateFromRange(endRoom.minYPos, endRoom.maxYPos);

                //Creates the last corridor
                final = new Corridor(this, endRoom);
                connectingCorridorNext = final;
                break;
            //right
            case 2:
                yPosWeld = startY;
                endY = yPosWeld;

                //set X coordinate
                endX = numGen.GenerateFromRange(endRoom.minXPos, endRoom.maxXPos);

                //Creates the last corridor
                final = new Corridor(this, endRoom);
                connectingCorridorNext = final;
                break;
            //down
            case 3:
                xPosWeld = startX;
                endX = xPosWeld;

                //set Y coordinate
                endY = numGen.GenerateFromRange(endRoom.minYPos, endRoom.maxYPos);

                //Creates the last corridor
                final = new Corridor(this, endRoom);
                connectingCorridorNext = final;
                break;
        }
    }

    //Creates the last corridor in a chain
    void FinishCorrConnections(Corridor prevCorr, Room endRoom)
    {
        startX = prevCorr.EndX;
        startY = prevCorr.EndY;

        //Y is off
        if (startX <= endRoom.maxXPos && startX >= endRoom.minXPos)
        {
            endX = startX;

            //Above room
            if (startY >= endRoom.maxYPos)
            {
                endY = endRoom.maxYPos;
            }
            //Below Room
            else
            {
                endY = endRoom.minYPos;
            }
        }
        //X is off
        else
        {
            endY = startY;

            //Right of Room
            if (startX >= endRoom.maxXPos)
            {
                endX = endRoom.maxXPos;
            }
            //Left of Room
            else
            {
                endX = endRoom.minXPos;
            }
        }

        Debug.Log("Added Last corridor");
        Debug.Log(this);
    }

    public override string ToString()
    {
        return "StartY:  " + startY + "  EndY:  " + endY + "  StartX:  " + startX + "  EndX:  " + endX + "  Length:  " + length + " Next Corridor:  " + connectingCorridorNext;
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