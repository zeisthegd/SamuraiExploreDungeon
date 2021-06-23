using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Theme", menuName = "Dungeon/Generation/Theme")]

public class DungeonTheme : ScriptableObject
{
    public GameObject Cell;
    public GameObject[] LesserMonsters;
    public GameObject[] GreaterMonsters;
    public GameObject Boss;
}
