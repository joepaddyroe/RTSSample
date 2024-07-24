using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionProducingUnitMovingToDestinationState : StateBase
{
    private ConstructionProducingUnit _constructionProducingUnit;
    
    public ConstructionProducingUnitMovingToDestinationState(ConstructionProducingUnit constructionProducingUnit)
    {
        _constructionProducingUnit = constructionProducingUnit;
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector3.Distance(_constructionProducingUnit.CurrentDestination, _constructionProducingUnit.transform.position) < 0.1f)
        {
            _constructionProducingUnit.StateMachine.SetState(new ConstructionProducingUnitIdleState(_constructionProducingUnit));
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        _constructionProducingUnit.Agent.SetDestination(_constructionProducingUnit.CurrentDestination);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
