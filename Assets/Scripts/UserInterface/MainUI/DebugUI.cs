using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : UserInterface
{
    Text playerState;
    public override void Start()
    {
        base.Start();
        playerState = GetComponentInChildren<Text>();
    }

    public override void Update()
    {
        base.Update();
        playerState.text = player.StateMachine.CurrentState.ToString();
    }

}
