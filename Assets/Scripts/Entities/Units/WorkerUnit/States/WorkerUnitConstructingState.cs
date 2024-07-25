using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitConstructingState : StateBase
{
    private WorkerUnit _workerUnit;
    private BuildingBase _targetConstruction;
    private float _constructionInterval;
    private float _constructionTimer;
    private float _constructionPoint;
    
    public WorkerUnitConstructingState(WorkerUnit workerUnit, BuildingBase targetConstruction, float constructionInterval, float constructionPoint)
    {
        _workerUnit = workerUnit;
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
                    _workerUnit.GoToIdlState();
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
        
        _workerUnit.Agent.SetDestination(_workerUnit.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
