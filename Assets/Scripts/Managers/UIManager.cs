using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject staminaBar;

    public void DisplayUI()
    {
        ShowMenu(debugMenu);
        CreatePlayerStaminaBar();
    }

    void ShowMenu(GameObject menu)
    {
        Instantiate(menu,Vector3.zero,Quaternion.identity);
    }
    void CreatePlayerStaminaBar()
    {
        Instantiate(staminaBar,Vector3.zero,Quaternion.identity);

    }
}
