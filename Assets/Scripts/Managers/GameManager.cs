using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] UIManager uIManager;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] GameState currentState;
    [SerializeField] GameObject level;

    public event System.Action OnDungeonInstantiated;

    GameObject playerPref;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        OnDungeonInstantiated += playerManager.LoadPlayer;
    }
    void Start()
    {
        CreateDungeon();
    }

    void Update()
    {

    }

    public void CreateDungeon()
    {
        Instantiate(level, Vector3.zero, Quaternion.identity);
    }

    public void OnDungeonLoaded()
    {
        OnDungeonInstantiated?.Invoke();
    }

    public enum GameState
    {
        StandBy, Loading, Playing, Paused, LevelChange, Shopping
    }
}
