using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A house consists of some rooms. To easily mangage the generation of rooms
[Serializable]
public class House : MonoBehaviour
{
    [SerializeField] private List<Room> roomsData = new List<Room>();//data
    [SerializeField] private List<GameObject> rooms = new List<GameObject>();//obj

    [SerializeField] GameObject room;


    public void SpawnRooms()
    {
        for (int i = 0; i < roomsData.Count; i++)
        {
            CreateRoomObj(i);
        }
    }
    public void GenerateRoomsInterior()
    {
        GenerateDoorsPosition();
        //Generate pillars and stuff
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
    private void CreateRoomObj(int i)
    {
        var roomObj = Instantiate(room,transform.position,Quaternion.identity,this.transform);
        roomObj.name = $"Room {i}";
        roomObj.isStatic = true;
        rooms.Add(roomObj);
        var roomScript = roomObj.GetComponent<Room>();
        Room.Copy(roomsData[i], roomScript);

    }

    public List<Room> RoomsData { get => roomsData; set => roomsData = value; }


}