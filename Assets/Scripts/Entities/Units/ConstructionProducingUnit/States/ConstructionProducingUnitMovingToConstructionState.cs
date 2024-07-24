using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionProducingUnitMovingToConstructionState : StateBase
{
    private ConstructionProducingUnit _constructionProducingUnit;
    private BuildingBase _targetConstruction;
    private Vector3 _targetBuildSitePosition;
    
    public ConstructionProducingUnitMovingToConstructionState(ConstructionProducingUnit constructionProducingUnit, BuildingBase targetConstruction, Vector3 targetBuildSitePosition)
    {
        _constructionProducingUnit = constructionProducingUnit;
        _targetConstruction = targetConstruction;
        _targetBuildSitePosition = targetBuildSitePosition;
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector3.Distance(_targetBuildSitePosition, _constructionProducingUnit.transform.position) < 2f)
        {
            _constructionProducingUnit.GoToConstructionState(_targetConstruction);
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        _constructionProducingUnit.Agent.SetDestination(_targetBuildSitePosition);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
