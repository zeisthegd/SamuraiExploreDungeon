using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Process the data before instantiate the cells
[Serializable]
public class Maze : MonoBehaviour
{
    int roomCreated = 0;

    private Cell[,] map;
    private List<GameObject> corridorCells;

    GenerationSettings settings;
    DungeonTheme theme;
    House house;

    void Awake()
    {
        GetThemeAndSettings();
        InitCells();
        InitRooms();
        SpawnHouse();
        GenerateCorridors();
    }

    void Start()
    {
        SpawnCorridorCells();
    }

    private void InitCells()
    {
        map = new Cell[settings.Size, settings.Size];
        for (int i = 0; i < settings.Size; i++)
        {
            for (int j = 0; j < settings.Size; j++)
            {
                Vector2 position = new Vector2(i, j);
                map[i, j] = new Cell(position);
            }
        }
    }

    private void InitRooms()
    {
        BruteForceSomeRooms();
        if (roomCreated < settings.MinRooms)
        {
            ClearTriedAttempts();
            InitRooms();
        }
    }

    #region Room Generation
    public void BruteForceSomeRooms()
    {
        house = new House();
        int randomRoomCount = MathUtility.GetRandomNumber(settings.MinRooms, settings.MaxRooms + 1);
        for (int i = 0; i < settings.MaxRoomTries || roomCreated < randomRoomCount; i++)
        {
            Room room = new Room(this, settings);
            if (room.TryCreateRoom())
            {
                house.AddRoom(room);
                roomCreated++;
            }
        }
    }

    public bool RoomIsNotOverlapping(int startX, int startY, int bottomX, int rightY)
    {
        for (int i = startX; i <= bottomX; i++)
        {
            for (int j = startY; j <= rightY; j++)
            {
                if (map[i, j].IsRoomCell)
                    return false;
            }
        }
        return true;
    }



    public void UpdateRoomCellsWalls(int startX, int startY, int bottomX, int rightY)
    {
        for (int i = startX; i <= bottomX; i++)
        {
            for (int j = startY; j <= rightY; j++)
            {
                map[i, j].UpdateAvailableWalls(map);
            }
        }
    }

    public bool RoomIsNotAdjacentToAnother(int startX, int startY, int bottomX, int rightY)
    {
        return (startX > 0 && startY > 0 && bottomX < settings.Size - 1 && rightY < settings.Size - 1)
                //CheckTopLeftCorner
                && map[startX - 1, startY - 1].CloseToRoom == false
                && map[startX, startY - 1].CloseToRoom == false
                && map[startX - 1, startY].CloseToRoom == false
                //CheckBottomLeftCorner
                && map[bottomX + 1, startY].CloseToRoom == false
                && map[bottomX, startY - 1].CloseToRoom == false
                && map[bottomX + 1, startY - 1].CloseToRoom == false
                //CheckTopRightCorner
                && map[startX, rightY + 1].CloseToRoom == false
                && map[startX - 1, rightY].CloseToRoom == false
                && map[startX - 1, rightY + 1].CloseToRoom == false
                //CheckTopRightCorner
                && map[bottomX, rightY + 1].CloseToRoom == false
                && map[bottomX + 1, rightY].CloseToRoom == false
                && map[bottomX + 1, rightY + 1].CloseToRoom == false;
    }

    #endregion

    #region Maze Generation

    public void GenerateCorridors()
    {
        List<Cell> corCells = GetNonRoomCells();
        Cell startCell = corCells[MathUtility.GetRandomPositiveNumber(corCells.Count)];
        RunMazeAlgorithm(startCell);
    }

    private void RunMazeAlgorithm(Cell cell)
    {
        cell.IsVisited = true;
        List<Cell> neighbors = GetNeighborCellsOf(cell);
        for (int i = 0; i < neighbors.Count; i++)
        {
            Cell nextCellInPath = neighbors[MathUtility.GetRandomPositiveNumber(neighbors.Count)];
            DeleteWallInBetween(cell, nextCellInPath);
            RunMazeAlgorithm(nextCellInPath);
            neighbors.Remove(nextCellInPath);
        }
    }

    public List<Cell> GetNonRoomCells()
    {
        List<Cell> cells = new List<Cell>();
        foreach (Cell cell in map)
        {
            if (cell.IsRoomCell == false)
                cells.Add(cell);
        }
        return cells;
    }

    private List<Cell> GetNeighborCellsOf(Cell cell)
    {
        List<Cell> cells = new List<Cell>();
        for (int i = 0; i < 4; i++)
        {
            Cell neighbor = GetCellAtDirection(cell, i);
            if (!Equals(neighbor, null))
            {
                if (!neighbor.IsVisited && !neighbor.IsRoomCell)
                    cells.Add(neighbor);
                else if (neighbor.IsDoor)
                    DeleteWallInBetween(cell, neighbor);
            }
        }
        return cells;
    }

    private Cell GetCellAtDirection(Cell cell, int direction)
    {
        Vector2 cellPos = cell.Position;
        int size = settings.Size;

        if (cell.Position.x > 0 && direction == 0)
            return map[(int)cellPos.x - 1, (int)cellPos.y];
        else if (cell.Position.x < size - 1 && direction == 1)
            return map[(int)cellPos.x + 1, (int)cellPos.y];
        else if (cell.Position.y < size - 1 && direction == 2)
            return map[(int)cellPos.x, (int)cellPos.y + 1];
        else if (cell.Position.y > 0 && direction == 3)
            return map[(int)cellPos.x, (int)cellPos.y - 1];

        return null;
    }

    private void DeleteWallInBetween(Cell a, Cell b)
    {
        bool[] wallsToDel = new bool[] { false, false, false, false };

        wallsToDel[0] = !(a.Position.x > b.Position.x);//N to lose | a is below b
        wallsToDel[1] = !(a.Position.y < b.Position.y);//E to lose | a is on the left of b     
        wallsToDel[2] = !(a.Position.x < b.Position.x);//S to lose | a is above b
        wallsToDel[3] = !(a.Position.y > b.Position.y);//W to lose | a is on the right of b

        for (int i = 0; i < 4; i++)
        {
            if (wallsToDel[i] == false)
            {
                int oppDir = (i % 2 == 0) ? ((i == 0) ? 2 : 0) : ((4 - i) % 4);
                a.AvailWalls[i] = false || a.IsDoor;
                b.AvailWalls[oppDir] = false || b.IsDoor;
                return;
            }
        }
    }

    public void SetDoorCell(int x, int y)
    {
        map[x, y].IsDoor = true;
        List<Cell> neighbors = GetNeighborCellsOf(map[x, y]);
        int i = 0;
        foreach (Cell cell in neighbors)
        {
            DeleteWallInBetween(map[x, y], neighbors[i]);
            i++;
        }
    }

    #endregion

    #region Instantiate
    private void SpawnCorridorCells()
    {
        foreach (Cell cell in map)
        {
            if (!cell.IsRoomCell)
            {
                Vector2 cPos = new Vector2(cell.Position.x, cell.Position.y);
                Vector3 spawnPos = new Vector3((cPos.x * settings.CellPositionOffset), 0, (cPos.y * settings.CellPositionOffset));
                var cellInstance = Instantiate(theme.Cell, spawnPos, Quaternion.identity, this.transform);

                cellInstance.GetComponent<Cell>().CopyCellValue(map[(int)cPos.x, (int)cPos.y]);
                cellInstance.name = $"Cell[{cPos.x},{cPos.y}]";
            }
        }
    }

    private void GetThemeAndSettings()
    {
        settings = FindObjectOfType<DungeonGenerator>().Settings;
        theme = FindObjectOfType<DungeonGenerator>().Theme;
    }

    #endregion
    private void ClearTriedAttempts()
    {
        InitCells();
    }

    private void SpawnHouse()
    {
        if (!GameObject.Find("House"))
        {
            GameObject houseObj = new GameObject();
            houseObj.isStatic = true;
            var houseSrcipt = houseObj.AddComponent<House>();
            houseSrcipt.RoomsData = this.house.RoomsData;
            houseObj.name = "House";

            houseObj.transform.parent = this.transform;
            houseSrcipt.GenerateRoomsData();
        }
    }


    public Cell[,] Map { get => map; }
    public GenerationSettings Settings { get => settings; set => settings = value; }
}


