using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player player;

    private void InstantiatePlayer()
    {
        Vector3 startPosition = new Vector3();
        startPosition = FindObjectOfType<Maze>().StartingCell.position;
        Instantiate(player, startPosition, Quaternion.identity);
    }
    private void LoadPlayerData()
    {

    }

    private void MakeCameraFollowPlayer()
    {
        var virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        virtualCamera.Follow = FindObjectOfType<Player>().transform;
        virtualCamera.LookAt = FindObjectOfType<Player>().transform;
    }

    public void LoadPlayer()
    {
        if (!PlayerIsSpawned())
        {
            InstantiatePlayer();
            MakeCameraFollowPlayer();
            LoadPlayerData();
        }
    }

    private bool PlayerIsSpawned()
    {
        return FindObjectOfType<Player>() != null;
    }

}
