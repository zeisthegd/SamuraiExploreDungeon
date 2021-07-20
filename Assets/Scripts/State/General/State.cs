using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State  
{
    void Update();
    void FixedUpdate();
    void StateChangeLogic();
    void Enter();
    void Exit();
}
