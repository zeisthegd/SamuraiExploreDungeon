using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] VoidEventChannelSO OnPlayerInstantiated;
    [SerializeField] VoidEventChannelSO OnDungeonGenerated;

    void Start()
    {

    }

    void OnEnable()
    {
        OnDungeonGenerated.OnEventRaised += LoadPlayer;
    }

    void OnDisable()
    {
        OnDungeonGenerated.OnEventRaised -= LoadPlayer;
    }
    public void LoadPlayer()
    {
        if (!PlayerIsSpawned())
        {
            LoadPlayerData();
            InstantiatePlayer();
        }
    }

    private void InstantiatePlayer()
    {
        Vector3 startPosition = new Vector3();
        startPosition = FindObjectOfType<Maze>().StartingCell.position;
        Instantiate(player, startPosition, Quaternion.identity);
        OnPlayerInstantiated.RaiseEvent();
    }
    private void LoadPlayerData()
    {

    }



    private bool PlayerIsSpawned()
    {
        return FindObjectOfType<Player>() != null;
    }


}
