using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    
    // mouse selection
    [SerializeField] private LayerMask _groundLayerMask;
    
    [SerializeField] private float  _mouseSelectionDraggingThreshold;

    [SerializeField] private UIGame _uiGame;

    private bool _skipInteractFrame;
    
    private PlayerInteractionStateMachine _playerInteractionStateMachine;

    public bool DebugDrawDragSelection;
    private Vector3 _debugDragSelectionStartPosition;
    private Vector3 _debugDragSelectionEndPosition;
    private Vector3 _debugDragSelectionPosition;
    private Vector3 _debugDragSelctionSize;
    
    public List<ISelectableEntity> CurrentlySelected = new List<ISelectableEntity>();
    
    public UIGame UIGame => _uiGame;
    public PlayerInteractionStateMachine PlayerInteractionStateMachine => _playerInteractionStateMachine;
    public LayerMask GroundLayerMask => _groundLayerMask;
    public float MouseSelectionDraggingThreshold => _mouseSelectionDraggingThreshold;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _playerInteractionStateMachine = new PlayerInteractionStateMachine();
        _playerInteractionStateMachine.SetState(new PlayerInteractionBasicSelectionState(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (_skipInteractFrame)
        {
            return;
        }
        _playerInteractionStateMachine.Tick();
    }

    public void ClearSkipInteract()
    {
        _skipInteractFrame = false;
    }
    
    public void SetBasicSelectionState(bool skipInteractFrame = false)
    {
        _skipInteractFrame = skipInteractFrame;
        if(_skipInteractFrame)
            Invoke("ClearSkipInteract", 0.2f);
        _playerInteractionStateMachine.SetState(new PlayerInteractionBasicSelectionState(this));
    }

    public void SetPlacingConstructionState(GameObject _constructionPrefab)
    {
        _playerInteractionStateMachine.SetState(new PlayerInteractionPlacingConstructionState(this, _constructionPrefab));
    }

    public void PlacedConstructionSite(BuildingBase targetConstruction, Vector3 targetBuildSite)
    {
        foreach (ISelectableEntity entity in CurrentlySelected)
        {
            ConstructionProducingUnit constructionProducingUnit = entity as ConstructionProducingUnit;
            if(constructionProducingUnit)
                constructionProducingUnit.GoToTargetConstruction(targetConstruction, targetBuildSite);
        }
    }
}
