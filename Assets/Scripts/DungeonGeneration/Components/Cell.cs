using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cell : MonoBehaviour
{
    public static string[] Directions = new string[] { "North", "East", "South", "West" };
    [SerializeField] private bool isDoor;
    [SerializeField] private bool isRoomCell;
    [SerializeField] private bool[] availWalls;//NESW
    [SerializeField] GameObject door;

    private bool isVisited;
    private bool[] availDirections;//NESW
    private bool closeToRoom;
    private bool triedCreateHere;
    private Vector2 position;

    public Cell()
    {
        position = new Vector2(-1, -1);
    }

    public Cell(Vector2 position)
    {
        this.position = position;
        isRoomCell = false;
        isDoor = false;
        triedCreateHere = false;
        closeToRoom = false;
        isVisited = false;
        availDirections = new bool[4] { true, true, true, true };
        availWalls = new bool[4] { true, true, true, true };

    }

    void Start()
    {
        BuildCell();
    }

    public void BuildCell()
    {
        for (int i = 0; i < 4; i++)
            BuildWall(i);

    }

    #region Cell Constructing

    private void BuildWall(int direction)
    {
        Destroy(direction, availWalls[direction]);
        SpawnDoor(direction);
    }


    public void Destroy(int direction, bool wallDirIsAvailable)
    {
        if (!wallDirIsAvailable)
        {
            var celDir = GetDirection(direction);
            Destroy(celDir.gameObject);
        }
    }

    private void DestroyWall(int direction)
    {
        var cellDir = GetDirection(direction);
        var wallObj = cellDir.Find("Wall");
        Destroy(wallObj.gameObject);
    }

    private void SpawnDoor(int direction)
    {
        if (isDoor && availWalls[direction] == true)
        {
            DestroyWall(direction);
            var doorDir = transform.Find(Directions[direction]);
            var newDoor = Instantiate(door, doorDir);   
        }
    }

    #endregion


    #region Data Processing
    public void UpdateAvailableWalls(Cell[,] map)
    {
        int mapLength = (int)Math.Sqrt(map.Length);
        if (position.x > 0 && map[(int)position.x - 1, (int)position.y].isRoomCell == true)
        {
            availWalls[0] = false;
        }

        if (position.x < mapLength - 1 && map[(int)position.x + 1, (int)position.y].isRoomCell == true)
        {
            availWalls[2] = false;
        }
        if (position.y < mapLength - 1 && map[(int)position.x, (int)position.y + 1].isRoomCell == true)
        {
            availWalls[1] = false;
        }

        if (position.y > 0 && map[(int)position.x, (int)position.y - 1].isRoomCell == true)
        {
            availWalls[3] = false;
        }
    }

    public void CopyCellValue(Cell data)
    {
        Cell cellscrpit = GetComponent<Cell>();
        cellscrpit.IsRoomCell = data.IsRoomCell;
        cellscrpit.AvailDirections = data.AvailDirections;
        cellscrpit.AvailWalls = data.AvailWalls;
        cellscrpit.IsDoor = data.IsDoor;
    }

    #endregion


    private Transform GetDirection(int direction)
    {
        return transform.Find(Directions[direction]);
    }

    public void Spawn(GameObject something, Transform parent)
    {
        Instantiate(something, transform.position, Quaternion.identity, parent);
    }

    public bool IsSearched()
    {
        foreach (bool direction in AvailWalls)
        {
            if (direction == false)
                return true;
        }
        return false;
    }

    public Vector2 Position { get => position; set => position = value; }
    public bool IsVisited { get => isVisited; set => isVisited = value; }
    public bool[] AvailDirections { get => availDirections; set => availDirections = value; }
    public bool CloseToRoom { get => closeToRoom; set => closeToRoom = value; }
    public bool IsDoor { get => isDoor; set => isDoor = value; }
    public bool IsRoomCell { get => isRoomCell; set => isRoomCell = value; }
    public bool[] AvailWalls { get => availWalls; set => availWalls = value; }
    public bool TriedCreateHere { get => triedCreateHere; set => triedCreateHere = value; }
}