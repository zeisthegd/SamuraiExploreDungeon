using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Theme", menuName = "Dungeon/Generation/Theme")]

public class DungeonTheme : ScriptableObject
{
    public GameObject Cell;
    [Header("Monsters")]
    public List<GameObject> LesserMonsters;
    public List<GameObject> GreaterMonsters;
    public GameObject Boss;

    [Header("Spawners")]
    public GameObject MonsterSpawner;
    public GameObject ItemSpawner;

}
