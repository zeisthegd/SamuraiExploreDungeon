using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Dungeon/Generation/Settings")]
public class GenerationSettings : ScriptableObject
{
    public int Size;
    public int MaxRoomTries;
    public int MinRooms;
    public int MaxRooms;
    public Vector2 MinRoomSize;
    public Vector2 MaxRoomSize;
    public float CellPositionOffset;

}