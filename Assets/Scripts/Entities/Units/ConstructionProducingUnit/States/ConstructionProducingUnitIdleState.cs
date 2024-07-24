using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionProducingUnitIdleState : StateBase
{
    private ConstructionProducingUnit _peasantManager;
    
    public ConstructionProducingUnitIdleState(ConstructionProducingUnit peasantManager)
    {
        _peasantManager = peasantManager;
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void Enter()
    {
        base.Enter();
        _peasantManager.Agent.SetDestination(_peasantManager.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
