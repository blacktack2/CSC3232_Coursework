using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyStopped : BasicEnemyPassive
{
    public BasicEnemyStopped(BasicEnemyMachine stateMachine) : base("Stopped", stateMachine)
    {
    }
}
