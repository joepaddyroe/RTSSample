using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProductionBuildingProductionState : StateBase
{
    private UnitProductionBuilding _unitProductionBuilding;
    private float _currentProductionTime = 0;
    private ProductionPackage _productionPackage;
    
    public UnitProductionBuildingProductionState(UnitProductionBuilding unitProductionBuilding, ProductionPackage productionPackage)
    {
        _unitProductionBuilding = unitProductionBuilding;
        _productionPackage = productionPackage;
    }

    public override void Tick()
    {
        base.Tick();

        if (_currentProductionTime < _productionPackage.ProductionTime)
        {
            _currentProductionTime += Time.deltaTime;
            return;
        }

        ProduceUnit();
        _unitProductionBuilding.StateMachine.SetState(new UnitProductionBuildingIdleState(_unitProductionBuilding));
    }

    private void ProduceUnit() //_unitProductionBuilding
    {
        GameObject unit = GameObject.Instantiate(_productionPackage.Prefab, _unitProductionBuilding.transform.position,
            Quaternion.identity);
        
        EntityManager.Instance.AddUnit(unit.GetComponent<UnitBase>());
        
        IMoveableEntity moveableEntity = unit.GetComponent<IMoveableEntity>();
        if(moveableEntity != null)
            moveableEntity.GoToTravellingState(_unitProductionBuilding.ConstructionLocation.position);
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entered Unit Production Building Production State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exited Unit Production Building Production State");
    }
}
