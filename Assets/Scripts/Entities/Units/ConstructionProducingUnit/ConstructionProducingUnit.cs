using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConstructionProducingUnit : UnitBase, IWorkAssignableEntity
{
    // state
    private StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;

    private Vector3 _currentDestination;
    public Vector3 CurrentDestination => _currentDestination;

    // prefabs for construction
    [SerializeField] private ConstructionPrefabsSO _constructionPrefabsSO;
    public ConstructionPrefabsSO ConstructionPrefabsSO => _constructionPrefabsSO;
    
    [SerializeField] private float _constructionInterval;
    [SerializeField] private float _constructionPoint;
    
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }
    
    public override void Init()
    {
        base.Init();
        
        _stateMachine = new StateMachine();
        _stateMachine.SetState(new ConstructionProducingUnitIdleState(this));
        
        _uiSelectedUnit = _uiGame.AddUnitUI(_entitySelectionUI);
        _uiSelectedUnit.SetUnit(this);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Tick();
    }

    public void GoToIdlState()
    {
        _stateMachine.SetState(new ConstructionProducingUnitIdleState(this));
    }
    
    public override void GoToTravellingState(Vector3 targetDestination)
    {
        base.GoToTravellingState(targetDestination);
        _currentDestination = targetDestination;
        _stateMachine.SetState(new ConstructionProducingUnitMovingToDestinationState(this));
    }
    
    public void GoToTargetConstruction(BuildingBase targetConstruction, Vector3 targetBuildSitePosition)
    {
        _stateMachine.SetState(new ConstructionProducingUnitMovingToConstructionState(this, targetConstruction, targetBuildSitePosition));
    }
    
    public void GoToConstructionState(BuildingBase targetConstruction)
    {
        _stateMachine.SetState(new ConstructionProducingUnitConstructingState(this, targetConstruction, _constructionInterval, _constructionPoint));
    }
    
    
    // Selection Handling
    public override void Select()
    {
        base.Select();
        _uiGame.SetConstructionProducingUnitSelected(true, this, _constructionPrefabsSO.ConstructionPackages);
    }

    public override void DeSelect()
    {
        base.DeSelect();
        _uiGame.SetConstructionProducingUnitSelected(false);
    }

    public void SendToConstructBuilding(BuildingBase building)
    {
        GoToTargetConstruction(building, building.transform.position);
    }

    public override void MoveToLocation(Vector3 location)
    {
        GoToTravellingState(location);
    }
}
