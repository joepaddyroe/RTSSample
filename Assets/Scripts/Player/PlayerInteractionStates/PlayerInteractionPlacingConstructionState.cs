

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInteractionPlacingConstructionState : PlayerInteractionStateBase
{
    private PlayerInteractionManager _playerInteractionManager;
    private GameObject _constructionPrefab;
    private GameObject _construction;
    private Camera _mainCamera;
    
    public PlayerInteractionPlacingConstructionState(PlayerInteractionManager playerInteractionManager, GameObject constructionPrefab)
    {
        _playerInteractionManager = playerInteractionManager;
        _constructionPrefab = constructionPrefab;
    }

    public override void Tick()
    {
        base.Tick();
        
        // left click to select an entity
        // right click to de-select an entity
        // middle click to de-select an entity - for now

        if (Input.GetMouseButtonDown(0))
        {
            PlaceConstructionAtPosition();
        }
        
        if (Input.GetMouseButtonDown(1))
            CancelConstruction();

        MoveConstructionToPosition();
    }

    public override void Enter()
    {
        base.Enter();
        _mainCamera = Camera.main;
        _construction = GameObject.Instantiate(_constructionPrefab);
        EntityManager.Instance.AddBuilding(_construction.GetComponent<BuildingBase>());
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    private Vector3 GetMouseSelectionWorldPoint()
    {
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, _playerInteractionManager.GroundLayerMask))
        {
            return hit.point;
        }
    
        return Vector3.negativeInfinity;
    }
    
    // construction functions

    private void CancelConstruction()
    {
        // destroy the prefab instance and switch state back to basic selection
        GameObject.Destroy(_construction);
        _playerInteractionManager.SetBasicSelectionState();
    }

    private void MoveConstructionToPosition()
    {
        if(_construction == null)
            return;
        _construction.transform.position = GetMouseSelectionWorldPoint();
    }
    
    private void PlaceConstructionAtPosition()
    {
        if(_construction == null)
            return;
        _construction.transform.position = GetMouseSelectionWorldPoint();
        _playerInteractionManager.PlacedConstructionSite(_construction.GetComponent<BuildingBase>(), _construction.transform.position);
        _playerInteractionManager.SetBasicSelectionState(true); // <- set to skip an interaction frame here for avoiding unwanted cross state clicks
    }
    
}
