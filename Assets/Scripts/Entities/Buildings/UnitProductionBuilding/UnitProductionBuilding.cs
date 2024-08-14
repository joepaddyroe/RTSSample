using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProductionBuilding : BuildingBase
{
    // construction helpers
    [SerializeField] private Transform _constructionLocation;
    public Transform ConstructionLocation => _constructionLocation;
    
    // prefabs for production
    [SerializeField] private ProductionPrefabsSO _productionPrefabsSO;
    public ProductionPrefabsSO ProductionPrefabsSO => _productionPrefabsSO;
    
    
    private StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;
    
    
    void Awake()
    {
        Init();
    }
    
    public override void Init()
    {
        base.Init();
        
        _stateMachine = new StateMachine();
        _stateMachine.SetState(new UnitProductionBuildingIdleState(this));
        
        _uiSelectedBuilding = _uiGame.AddBuildingUI(_entitySelectionUI);
        _uiSelectedBuilding.SetBuilding(this);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Tick();
    }
    
    // Selection Handling
    public override void Select()
    {
        base.Select();
        _uiGame.SetUnitProductionBuildingSelected(true, this, ProductionPrefabsSO.ProductionPackages);
    }

    public override void DeSelect()
    {
        base.DeSelect();
        _uiGame.SetUnitProductionBuildingSelected(false);
    }
    
    public override void DestroyOrDie()
    {
        base.DestroyOrDie();
        //ConstructionDestroyed();
        Destroy(gameObject, 2);
    }
}