using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class Room : MonoBehaviour
{
    Vector2 topLeft, bottomRight;
    List<Cell> cells = new List<Cell>();
    List<GameObject> cellObjs;
    DungeonTheme theme;
    GenerationSettings settings;
    Maze maze;

    void Start()
    {
        GetThemeAndSettings();
        SpawnCells();
    }

    private void SpawnCells()
    {
        cellObjs = new List<GameObject>();
        foreach (Cell cell in cells)
        {
            Vector2 cPos = new Vector2(cell.Position.x, cell.Position.y);
            Vector3 spawnPos = new Vector3((cPos.x * settings.CellPositionOffset), 0, (cPos.y * settings.CellPositionOffset));
            var cellObj = Instantiate(theme.Cell, spawnPos, Quaternion.identity, this.transform);

            cellObj.GetComponent<Cell>().CopyCellValue(cell);
            cellObj.name = $"Cell[{cPos.x},{cPos.y}]";

            cellObjs.Add(cellObj);
        }
    }

    private void GetThemeAndSettings()
    {
        theme = FindObjectOfType<DungeonGenerator>().Theme;
        settings = FindObjectOfType<DungeonGenerator>().Settings;

    }

    public Room(Maze maze, GenerationSettings settings)
    {
        this.maze = maze;
        this.settings = settings;
    }

    public bool TryCreateRoom()
    {
        System.Random random = new System.Random();
        int roomStartPosX = random.Next(0, settings.Size);
        int roomStartPosY = random.Next(0, settings.Size);

        if (maze.Map[roomStartPosX, roomStartPosY].TriedCreateHere == false
            && maze.Map[roomStartPosX, roomStartPosY].IsRoomCell == false)
        {
            for (int i = 0; i < settings.MaxRoomTries; i++)
            {
                int rHeight = random.Next((int)settings.MinRoomSize.x, (int)settings.MaxRoomSize.x);//vertical length
                int rWidth = random.Next((int)settings.MinRoomSize.y, (int)settings.MaxRoomSize.y);//horizontal length

                int bottomX = roomStartPosX + rHeight;
                int rightY = roomStartPosY + rWidth;

                if (IsValidRoom(roomStartPosX, roomStartPosY, bottomX, rightY))
                {
                    SetRoomCells(roomStartPosX, roomStartPosY, bottomX, rightY);
                    maze.UpdateRoomCellsWalls(roomStartPosX, roomStartPosY, bottomX, rightY);
                    maze.Map[roomStartPosX, roomStartPosY].TriedCreateHere = true;

                    topLeft = new Vector2(roomStartPosX, roomStartPosY);
                    bottomRight = new Vector2(bottomX, rightY);
                    return true;
                }
            }
            maze.Map[roomStartPosX, roomStartPosY].TriedCreateHere = true;
        }

        return false;
    }

    public void CreateDoor()
    {
        CreateDoorAtRow((int)topLeft.x);
        CreateDoorAtRow((int)bottomRight.x);
        CreateDoorAtColumn((int)topLeft.y);
        CreateDoorAtColumn((int)bottomRight.y);
    }

    public void SetRoomCells(int startX, int startY, int bottomX, int rightY)
    {
        for (int i = startX; i <= bottomX; i++)
        {
            for (int j = startY; j <= rightY; j++)
            {
                maze.Map[i, j].IsRoomCell = true;
                cells.Add(maze.Map[i, j]);

                if (i > 0)
                    maze.Map[i - 1, j].CloseToRoom = true;
                if (i < settings.Size - 1)
                    maze.Map[i + 1, j].CloseToRoom = true;
                if (j < settings.Size - 1)
                    maze.Map[i, j + 1].CloseToRoom = true;
                if (j > 0)
                    maze.Map[i, j - 1].CloseToRoom = true;
            }
        }
    }

    private bool IsValidRoom(int roomStartPosX, int roomStartPosY, int bottomX, int rightY)
    {
        return RoomIsInMaze(bottomX, rightY)
                        && maze.RoomIsNotOverlapping(roomStartPosX, roomStartPosY, bottomX, rightY)
                            && maze.RoomIsNotAdjacentToAnother(roomStartPosX, roomStartPosY, bottomX, rightY);
    }

    private void CreateDoorAtRow(int x)
    {
        int ranDoorPos = MathUtility.GetRandomNumber((int)topLeft.y + 1, (int)bottomRight.y - 1);
        maze.SetDoorCell(x, ranDoorPos);
    }
    private void CreateDoorAtColumn(int y)
    {
        int ranDoorPos = MathUtility.GetRandomNumber((int)topLeft.x + 1, (int)bottomRight.x - 1);
        maze.SetDoorCell(ranDoorPos, y);

    }

    private bool RoomIsInMaze(int x, int z)
    {
        bool isInMaze = (x < settings.Size && z < settings.Size
            && x >= 0 && z >= 0);
        return isInMaze;
    }

    public List<Cell> Cells { get => cells; set => cells = value; }
}