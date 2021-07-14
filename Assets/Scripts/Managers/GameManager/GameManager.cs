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

    [Header("Event Channels")]
    [SerializeField] VoidEventChannelSO OnDungeonGenerated;
    [SerializeField] VoidEventChannelSO OnPlayerInstantiated;
    static GameManager instance;

    GameObject playerPref;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;

        currentState = GameState.Debugging;
    }
    void Start()
    {
        CreateInstance();
        GameflowManaging();
    }

    void Update()
    {

    }

    void OnEnable()
    {

    }
    void OnDisable()
    {

    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(this.gameObject);
    }
    public void GenerateDungeon()
    {
        Instantiate(level, Vector3.zero, Quaternion.identity);
    }

    private void GameflowManaging()
    {
        switch (CurrentState)
        {
            case GameState.Debugging:
                GenerateDungeon();
                break;
            case GameState.Paused:
                break;
            case GameState.Playing:
                break;
            case GameState.Shopping:
                break;
            case GameState.StandBy:
                break;
            default:
                Debug.Log("The game is not on any state. Make sure to have a game manager.");
                break;
        }
    }

    public static GameManager Instance
    {
        get => instance;
    }
    public GameState CurrentState { get => currentState; set => currentState = value; }


}
public enum GameState
{
    StandBy, Loading, Playing, Paused, LevelChange, Shopping, Debugging
}
