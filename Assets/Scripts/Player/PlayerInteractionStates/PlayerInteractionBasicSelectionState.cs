

using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionBasicSelectionState : PlayerInteractionStateBase
{
    private PlayerInteractionManager _playerInteractionManager;
    
    private List<ISelectableEntity> _dragSelected = new List<ISelectableEntity>();
    
    private bool _mouseSelectionDown;
    private bool _mouseSelectionDragging;
    private bool _mouseDraggingThresholdMet;
    
    private Vector3 _mouseSelectionDraggingStartPoint;
    private Vector2 _mousePositionStartPoint;
    private Vector3 _mouseSelectionDraggingCurrentPoint;
    private Vector2 _mousePositionCurrentPoint;
    private float  _mouseSelectionDraggingTimer;
    private Camera _mainCamera;
    
    public PlayerInteractionBasicSelectionState(PlayerInteractionManager playerInteractionManager)
    {
        _playerInteractionManager = playerInteractionManager;
    }

    public override void Tick()
    {
        base.Tick();
        
        // left click to select an entity
        // right click to de-select an entity
        // middle click to de-select an entity - for now

        if (Input.GetMouseButtonDown(0))
        {
            // set the drag start point here so its ready when th drag timer threshold is met
            // otherwise we get a start point that is offset by mouse drag time
            if (!_mouseSelectionDown)
            {
                _mouseSelectionDraggingStartPoint = GetMouseSelectionWorldPoint();
                _mouseSelectionDraggingCurrentPoint = _mouseSelectionDraggingStartPoint;
                _mousePositionStartPoint = Input.mousePosition;
            }
            _mouseSelectionDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_mouseSelectionDraggingTimer < _playerInteractionManager.MouseSelectionDraggingThreshold)
            {
                // check if current selected units should be assigned to work
                bool assignedToWorkSelection = DoSelectedInteraction();

                if (!assignedToWorkSelection)
                {
                    // do basic click selection
                    bool didSelection = DoSelect();

                    if (!didSelection)
                    {
                        // do unit movement command
                        DoUnitMovementCommand();
                    }
                }
            }
            _mouseSelectionDown = false;
            _mouseDraggingThresholdMet = false;
            DoMouseDraggingRelease();
        }
        
        if (Input.GetMouseButtonDown(1))
            DoDeselect();

        if (Input.GetMouseButtonDown(2))
            DoDeselect();

        if (_mouseSelectionDragging)
            DoMouseDraggingSelection();


        if (_mouseSelectionDown)
        {
            if (_mouseSelectionDraggingTimer >= _playerInteractionManager.MouseSelectionDraggingThreshold && !_mouseSelectionDragging)
            {
                StartMouseDraggingSelection();
            }
            _mouseSelectionDraggingTimer += Time.deltaTime;
        }
        
    }

    public override void Enter()
    {
        base.Enter();
        _mainCamera = Camera.main;
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    
    // selection functions
    
    // Mouse Selection functionality
    
    private bool DoSelect()
    {
        ISelectableEntity clicked = RayCheck();

        BuildingBase buildinConstructedCheck = clicked as BuildingBase;
        if (buildinConstructedCheck != null && !buildinConstructedCheck.Constructed)
            return false;
        
        if (clicked != null)
        {
            if (_playerInteractionManager.CurrentlySelected.Count > 0)
            {
                foreach (ISelectableEntity entity in _playerInteractionManager.CurrentlySelected)
                {
                    entity.DeSelect();
                }
            }

            _playerInteractionManager.CurrentlySelected = new List<ISelectableEntity>();
                
            _playerInteractionManager.CurrentlySelected.Add(clicked);
            clicked.Select();
            
            return true;
        }

        return false;
    }
    
    private bool DoSelectedInteraction()
    {
        ISelectableEntity clicked = RayCheck();

        if (clicked != null)
        {
            if (_playerInteractionManager.CurrentlySelected.Count > 0)
            {
                bool unitsInteracted = false;
                
                foreach (ISelectableEntity entity in _playerInteractionManager.CurrentlySelected)
                {
                    IWorkAssignableEntity worker = entity as IWorkAssignableEntity;
                    if (worker != null)
                    {
                        IWorkTargetEntity workTarget = clicked as IWorkTargetEntity;
                        if (workTarget != null)
                        {
                            BuildingBase building = workTarget as BuildingBase;
                            if (building != null)
                            {
                                if (!building.Constructed)
                                {
                                    worker.SendToConstructBuilding(building);
                                    unitsInteracted = true;
                                }
                            }
                        }
                    }
                }
                return unitsInteracted;
            }
            
            return false;
        }

        return false;
    }
    
    private void DoDeselect()
    {
        if (_playerInteractionManager.CurrentlySelected == null)
            return;
        
        foreach (ISelectableEntity entity in _playerInteractionManager.CurrentlySelected)
        {
            entity.DeSelect();
        }
        
        _playerInteractionManager.CurrentlySelected = new List<ISelectableEntity>();
    }

    private ISelectableEntity RayCheck()
    {
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            ISelectableEntity entity = hit.collider.gameObject.GetComponent<ISelectableEntity>();
            if(entity != null)
                return entity;
        }
    
        return null;
    }

    private void StartMouseDraggingSelection()
    {
        _mouseSelectionDragging = true;
    }
    
    private void DoMouseDraggingSelection()
    {
        _mouseSelectionDraggingCurrentPoint = GetMouseSelectionWorldPoint();

        // dont bother deselecting or processing drag select if its not beyond this length threshold
        // stops unwanted deselects on slow clickers ;)
        if (!_mouseDraggingThresholdMet && Vector3.Distance(_mouseSelectionDraggingStartPoint, _mouseSelectionDraggingCurrentPoint) > 0.5f)
        {
            _mouseDraggingThresholdMet = true;
        }

        if (!_mouseDraggingThresholdMet)
            return;
        
        // ui stuff
        _playerInteractionManager.UIGame.SetDragSelectorVisible(true);
        _mousePositionCurrentPoint = Input.mousePosition;
        
        _dragSelected = _playerInteractionManager.UIGame.SetDragSelectorSizeAndPosition(_mousePositionStartPoint, _mousePositionCurrentPoint);

        if (_dragSelected.Count > 0)
        {
            foreach (ISelectableEntity entity in _dragSelected)
            {
                entity.Select();
            }
            
            foreach (ISelectableEntity currentSelection in _playerInteractionManager.CurrentlySelected)
            {
                if (!_dragSelected.Contains(currentSelection) || _dragSelected.Count == 0)
                {
                    currentSelection.DeSelect();
                }
            }

            _playerInteractionManager.CurrentlySelected = _dragSelected;
        }
        else
        {
            foreach (ISelectableEntity currentSelection in _playerInteractionManager.CurrentlySelected)
            {
                if (_dragSelected.Count == 0)
                {
                    currentSelection.DeSelect();
                }
            }
            _playerInteractionManager.CurrentlySelected = _dragSelected;
        }
    }
    private void DoMouseDraggingRelease()
    {
        _mouseSelectionDragging = false;
        _mouseSelectionDraggingTimer = 0;
        _playerInteractionManager.UIGame.SetDragSelectorVisible(false);
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

    
    
    // unit commands
    private void DoUnitMovementCommand()
    {
        Vector3 location = GetMouseSelectionWorldPoint();

        if (location == Vector3.negativeInfinity)
            return;
        
        foreach (ISelectableEntity currentSelection in _playerInteractionManager.CurrentlySelected)
        {
            IMoveableEntity moveableUnit = currentSelection as IMoveableEntity;
            if(moveableUnit != null)
                moveableUnit.MoveToLocation(location);
        }
    }
    
}
