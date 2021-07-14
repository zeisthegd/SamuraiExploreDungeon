using System;
using System.Collections;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Dungeon")]
    [SerializeField] bool toGenerate = false;
    [SerializeField] Maze maze;
    [SerializeField] DungeonTheme theme;
    [SerializeField] GenerationSettings settings;
    [SerializeField] WaitForSeconds waitTime = new WaitForSeconds (1f);
    [Header("Event Channels")]
    [SerializeField] VoidEventChannelSO OnDungeonGenerated;

    Maze mainMaze;

    void Start()
    {
        StartCoroutine(GenerateDungeon());
    }

    [ContextMenu("Generate Dungeon")]
    IEnumerator GenerateDungeon()
    {
        if (toGenerate)
        {
            StartCoroutine(DeleteAllMaze());
            mainMaze = Instantiate(maze, transform.position, Quaternion.identity, this.transform);
            mainMaze.CreateNewDungeon();
            yield return new WaitForSeconds(10);
            OnDungeonGenerated.RaiseEvent();
        }     
    }

    IEnumerator DeleteAllMaze()
    {
        var mazes = FindObjectsOfType<Maze>();
        if (mazes.Length > 0)
        {
            foreach (Maze maze in mazes)
                Destroy(maze.gameObject);
        }
        yield return waitTime;
    }

    public GenerationSettings Settings { get => settings; set => settings = value; }
    public DungeonTheme Theme { get => theme; set => theme = value; }
}