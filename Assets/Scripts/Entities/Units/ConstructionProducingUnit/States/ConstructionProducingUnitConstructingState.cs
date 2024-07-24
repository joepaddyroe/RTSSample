using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionProducingUnitConstructingState : StateBase
{
    private ConstructionProducingUnit _constructionProducingUnit;
    private BuildingBase _targetConstruction;
    private float _constructionInterval;
    private float _constructionTimer;
    private float _constructionPoint;
    
    public ConstructionProducingUnitConstructingState(ConstructionProducingUnit constructionProducingUnit, BuildingBase targetConstruction, float constructionInterval, float constructionPoint)
    {
        _constructionProducingUnit = constructionProducingUnit;
        _targetConstruction = targetConstruction;
        _constructionInterval = constructionInterval;
        _constructionPoint = constructionPoint;
    }

    public override void Tick()
    {
        base.Tick();

        if (_constructionTimer < _constructionInterval)
            _constructionTimer += Time.deltaTime;
        else
        {
            _constructionTimer = 0;
            if (_targetConstruction)
            {
                if (_targetConstruction.Construct(_constructionPoint))
                {
                    // do construction complete announcement/event stuff here
                    Debug.Log("The construction is complete!");
                    _constructionProducingUnit.GoToIdlState();
                }
                else
                {
                    Debug.Log("The construction is is still underway...");
                }
            }
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        _constructionProducingUnit.Agent.SetDestination(_constructionProducingUnit.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
