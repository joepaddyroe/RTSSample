using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootmanManager : UnitBase
{
    // state
    private StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;

    private Vector3 _currentDestination;
    public Vector3 CurrentDestination => _currentDestination;

    [SerializeField] private float _attackInterval;
    [SerializeField] private float _attackPoint;

    public float AttackInterval => _attackInterval;
    public float AttackPoint => _attackPoint;
    
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }
    
    public override void Init()
    {
        base.Init();
        
        _stateMachine = new StateMachine();
        _stateMachine.SetState(new FootmanIdleState(this));
        
        _uiSelectedUnit = _uiGame.AddUnitUI(_entitySelectionUI);
        _uiSelectedUnit.SetUnit(this);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Tick();
        UpdateScreenSpaceCoordinate(this);
    }

    public void GoToIdlState()
    {
        _stateMachine.SetState(new FootmanIdleState(this));
    }
    
    public override void GoToTravellingState(Vector3 targetDestination)
    {
        base.GoToTravellingState(targetDestination);
        _currentDestination = targetDestination;
        _stateMachine.SetState(new FootmanMovingToDestinationState(this));
    }
    
    
    // Selection Handling
    public override void Select()
    {
        base.Select();
    }

    public override void DeSelect()
    {
        base.DeSelect();
    }

    public override void MoveToLocation(Vector3 location)
    {
        GoToTravellingState(location);
    }
}
