using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] UIManager uIManager;
    [SerializeField] GameState currentState;

    GameObject playerPref;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum GameState
    {
        StandBy,
        Loading,
        Playing,
        Paused,
        LevelChange,
        Shopping
    }
}
