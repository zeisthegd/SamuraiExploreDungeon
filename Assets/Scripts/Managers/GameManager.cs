using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] UIManager uIManager;
    [SerializeField] TimeManager timeManager;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] GameState currentState;
    [SerializeField] GameObject level;

    public event System.Action OnDungeonInstantiated;

    GameObject playerPref;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;

        OnDungeonInstantiated += playerManager.LoadPlayer;
        playerManager.PlayerIsSpawn += EnableUI;
        currentState = GameState.Playing;

    }
    void Start()
    {
        GenerateDungeon();
    }

    void Update()
    {
        
    }

    [ContextMenu("Generate Dungeon")]
    public void GenerateDungeon()
    {
        Instantiate(level, Vector3.zero, Quaternion.identity);
    }

    public void OnDungeonLoaded()
    {
        OnDungeonInstantiated?.Invoke();
    }

    private void EnableUI()
    {
        uIManager.DisplayUI();
    }

    public enum GameState
    {
        StandBy, Loading, Playing, Paused, LevelChange, Shopping
    }
}
