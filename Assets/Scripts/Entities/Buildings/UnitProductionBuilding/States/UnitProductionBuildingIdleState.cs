using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProductionBuildingIdleState : StateBase
{
    private UnitProductionBuilding _barracksManager;
    
    public UnitProductionBuildingIdleState(UnitProductionBuilding barracksManager)
    {
        _barracksManager = barracksManager;
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
