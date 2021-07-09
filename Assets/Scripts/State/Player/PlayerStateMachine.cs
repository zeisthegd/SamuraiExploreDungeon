using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    Player player;
    PlayerState idleState;
    PlayerState runState;
    PlayerState slashState;
    PlayerState chargeState;

    public PlayerStateMachine(Player player)
    {
        this.player = player;
        idleState = new IdleState(player, this);
        runState = new RunState(player, this);
        slashState = new SlashState(player, this);
        chargeState = new ChargeAndDashState(player, this);
    }

    public void ChangeStateToIdle()
    {
        ChangeState(idleState);
    }
    public void ChangeStateToRun()
    {
        ChangeState(runState);

    }
    public void ChangeStateToSlash()
    {
        ChangeState(slashState);

    }
    public void ChangeStateToCharge()
    {
        ChangeState(chargeState);

    }

    public PlayerState IdleState { get => idleState; set => idleState = value; }
    public PlayerState RunState { get => runState; set => runState = value; }
    public PlayerState SlashState { get => slashState; set => slashState = value; }
    public PlayerState ChargeState { get => chargeState; set => chargeState = value; }


}
