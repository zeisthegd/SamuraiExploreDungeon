using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject playingUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject staminaBar;
    [SerializeField] VoidEventChannelSO OnPlayerInstantiated;

    GameManager gameManager;

    void Start()
    {
        gameManager = GetComponentInParent<GameManager>();
    }
    void OnEnable()
    {
        OnPlayerInstantiated.OnEventRaised += ShowAppropriateMenu;
    }
    void OnDisable()
    {
        OnPlayerInstantiated.OnEventRaised -= ShowAppropriateMenu;
    }

    public void ShowAppropriateMenu()
    {
        switch (gameManager.CurrentState)
        {
            case GameState.Debugging:
                ShowMenu(debugMenu);
                ShowMenu(playingUI);
                break;
            case GameState.Paused:
                ShowMenu(pauseMenu);
                break;
            case GameState.Playing:
                ShowMenu(playingUI);
                break;
            case GameState.Shopping:
                ShowMenu(pauseMenu);
                break;
            case GameState.StandBy:
                ShowMenu(pauseMenu);
                break;
            default:
                Debug.Log("The game is not on any state. Make sure to have a game manager.");
                break;
        }
    }

    void ShowMenu(GameObject menu)
    {
        Instantiate(menu, Vector3.zero, Quaternion.identity);
    }
    void CreatePlayerStaminaBar()
    {
        Instantiate(staminaBar, Vector3.zero, Quaternion.identity);

    }
}
