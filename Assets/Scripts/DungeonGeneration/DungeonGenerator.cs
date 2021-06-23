using System;
using System.Collections;
using UnityEngine;


public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] DungeonTheme theme;
    [SerializeField] GenerationSettings settings;
    [SerializeField] GameObject cellType;
    [SerializeField] bool toGenerate = false;
    [SerializeField] float positionOffset;

    private House house;// Manage Rooms


    void Awake()
    {

    }
    void Start()
    {
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        if (toGenerate)
        {
            SpawnMaze();
        }
    }

    private void SpawnMaze()
    {
        if (!GameObject.Find("Maze"))
        {
            GameObject maze = new GameObject();
            maze.isStatic = true;
            maze.AddComponent<Maze>();
            maze.GetComponent<Maze>().Settings = this.settings;
            maze.name = "Maze";
            maze.transform.parent = this.transform;
        }
    }

    public GenerationSettings Settings { get => settings; set => settings = value; }
    public DungeonTheme Theme { get => theme; set => theme = value; }
}