using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A house consists of some rooms. To easily mangage the generation of rooms
[Serializable]
public class House : MonoBehaviour
{
    List<Room> roomsData = new List<Room>();//data
    private List<GameObject> rooms=  new List<GameObject>();//obj

    void Start()
    {
        SpawnRooms();
    }
    public void GenerateRoomsData()
    {
        GenerateDoorsPosition();
    }

    private void GenerateDoorsPosition()
    {
        foreach (Room room in roomsData)
        {
            room.CreateDoor();
        }
    }

    public void AddRoom(Room room)
    {
        roomsData.Add(room);
    }

    private void SpawnRooms()
    {
        for (int i = 0; i < roomsData.Count; i++)
        {
            GameObject roomObj = new GameObject();
            roomObj.name = $"Room[i]";
            roomObj.isStatic = true;
            rooms.Add(roomObj);

            var roomScript = roomObj.AddComponent<Room>();
            roomScript.Cells = roomsData[i].Cells;

            roomObj.transform.parent = this.transform;
        }
    }

    public List<Room> RoomsData { get => roomsData; set => roomsData = value; }


}