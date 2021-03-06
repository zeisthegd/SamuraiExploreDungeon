using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Process the data before instantiate the cells
[Serializable]
public class Maze : MonoBehaviour
{
    [SerializeField] int roomCreated = 0;
    [SerializeField] int minAcceptableRooms = 0;

    private Cell[,] map;
    private List<GameObject> corridorCells = new List<GameObject>();

    Vector2 startCellPosition;
    [SerializeField] Transform startingCell;

    [SerializeField] GenerationSettings settings;
    [SerializeField] DungeonTheme theme;
    [SerializeField] House house;


    [ContextMenu("Create New Maze")]
    public void CreateNewDungeon()
    {
        GetSettingsUtil.GetDungThemeAndGenSettings(ref theme, ref settings);
        InitCells();
        GenerateRoomDatas();
        GenerateHouse();
        GenerateCorridors();
        SearchRemainingLeftoverCells();
        SpawnCorridorCells();
        house.SpawnRooms();
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

    private void GenerateRoomDatas()
    {
        minAcceptableRooms = settings.MinRooms + 1;
        do
        {
            minAcceptableRooms--;
            ClearTriedAttempts();
            BruteForceSomeRooms();
        }
        while (roomCreated < minAcceptableRooms);
    }

    #region Room Generation
    public void BruteForceSomeRooms()
    {
        house = transform.Find("House").gameObject.GetComponent<House>();
        int randomRoomCount = MathUtility.GetRandomNumber(minAcceptableRooms, settings.MaxRooms + 1);
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
        startCellPosition = startCell.Position;
        RunMazeAlgorithm(startCell);
    }

    public void SearchRemainingLeftoverCells()
    {
        for (int i = 0; i < 20; i++)
        {
            List<Cell> corCells = GetUnsearchedCells();
            Cell startCell = corCells[MathUtility.GetRandomPositiveNumber(corCells.Count)];
            RunMazeAlgorithm(startCell);
        }
    }

    private List<Cell> GetUnsearchedCells()
    {
        List<Cell> cells = new List<Cell>();
        foreach (Cell cell in map)
        {
            if (cell.IsRoomCell == false && !cell.IsSearched())
                cells.Add(cell);
        }
        return cells;
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

    #region Maze Building
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

                corridorCells.Add(cellInstance);
            }
        }
        startingCell = transform.Find($"Cell[{startCellPosition.x},{startCellPosition.y}]");

    }

    IEnumerator DestroyAllCells()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        if (cells.Length != 0)
            foreach (Cell cell in cells)
                Destroy(cell.gameObject);
        yield return new WaitForSeconds(5);
    }

    #endregion
    private void ClearTriedAttempts()
    {
        InitCells();
    }

    private void GenerateHouse()
    {
        house.GenerateRoomsInterior();
    }


    public Cell[,] Map { get => map; }
    public Transform StartingCell { get => startingCell; }
}


