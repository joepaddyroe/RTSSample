using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    
    [SerializeField] private GameManager _gameManager;
    
    [SerializeField] private Transform _unitsSelections;
    [SerializeField] private Transform _buildingsSelections;

    // parent canvas
    [SerializeField] private Canvas _parentCanvas;
    
    // drag selector panel
    [SerializeField] private RectTransform _dragSelectorPanel;
    
    // building construction panels
    [SerializeField] private GameObject _uiConstructionPanel;
    [SerializeField] private GameObject _uiProductionPanel;

    private Dictionary<ISelectableEntity, Vector2> _unitScreenSpaceCoordinates = new Dictionary<ISelectableEntity, Vector2>();
    private Camera _mainCamera;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        UIConstructionPanel.SetGameManager(_gameManager);
        UIProductionPanel.SetGameManager(_gameManager);
    }

    // buildings
    public UIConstructionPanel UIConstructionPanel => _uiConstructionPanel.GetComponent<UIConstructionPanel>();
    
    // units
    public UIProductionPanel UIProductionPanel => _uiProductionPanel.GetComponent<UIProductionPanel>();

    // enable/disable construction panels 
    public void SetUnitProductionBuildingSelected(bool visible, UnitProductionBuilding unitProductionBuilding = null, List<ProductionPackage> productionPackages = null)
    {
        _uiProductionPanel.SetActive(visible);
        if(visible)
            UIProductionPanel.Init(unitProductionBuilding, productionPackages);
    }
    
    public void SetWorkerUnitSelected(bool visible, WorkerUnit workerUnit = null, List<ConstructionPackage> constructionPackages = null)
    {
        _uiConstructionPanel.SetActive(visible);
        if(visible)
            UIConstructionPanel.Init(workerUnit, constructionPackages);
    }
    
    // Entity UI and Selection Objects
    public UISelectedUnit AddUnitUI(GameObject uiSelectedUnit)
    {
        GameObject uiUnitUI = GameObject.Instantiate(uiSelectedUnit, _unitsSelections, false);
        return uiUnitUI.GetComponent<UISelectedUnit>();
    }
    public void RemoveUnitUI(GameObject uiSelectedUnit)
    {
        
    }
    public UISelectedBuilding AddBuildingUI(GameObject uiSelectedBuilding)
    {
        GameObject uiBuildingUI = GameObject.Instantiate(uiSelectedBuilding, _buildingsSelections, false);
        return uiBuildingUI.GetComponent<UISelectedBuilding>();
    }
    public void RemoveBuildingUI(GameObject uiSelectedBuilding)
    {
        
    }

    public void UpdateUnitScreenSpaceCoordinate(ISelectableEntity unit, Vector3 position)
    {
        Vector2 coordinate = _mainCamera.WorldToScreenPoint(position);
        
        if (_unitScreenSpaceCoordinates.ContainsKey(unit))
            _unitScreenSpaceCoordinates[unit] = coordinate;
        else
            _unitScreenSpaceCoordinates.Add(unit, coordinate);
    }
    
    
    // set drag selection state
    public void SetDragSelectorVisible(bool visible)
    {
        _dragSelectorPanel.transform.gameObject.SetActive(visible);
    }
    public List<ISelectableEntity> SetDragSelectorSizeAndPosition(Vector2 startPoint, Vector2 endPoint)
    {

        Rect dragRect = new Rect();
        dragRect.xMin = endPoint.x < startPoint.x ? endPoint.x : startPoint.x;
        dragRect.xMax = endPoint.x >= startPoint.x ? endPoint.x : startPoint.x;
        
        dragRect.yMin = endPoint.y < startPoint.y ? endPoint.y : startPoint.y;
        dragRect.yMax = endPoint.y >= startPoint.y ? endPoint.y : startPoint.y;
        
        float width = Mathf.Abs(endPoint.x - startPoint.x) / _parentCanvas.scaleFactor;
        float height = Mathf.Abs(endPoint.y - startPoint.y) / _parentCanvas.scaleFactor;
        
        _dragSelectorPanel.sizeDelta = new Vector2(width, height);
        _dragSelectorPanel.anchoredPosition = (startPoint + new Vector2((endPoint.x - startPoint.x)/2, (endPoint.y - startPoint.y)/2)) - new Vector2((float)Screen.width/2, (float)Screen.height/2);
        _dragSelectorPanel.anchoredPosition /= _parentCanvas.scaleFactor;

        List<ISelectableEntity> dragSelected = new List<ISelectableEntity>();
        
        foreach (var unit in _unitScreenSpaceCoordinates)
        {
            if (dragRect.Contains(unit.Value))
            {
                dragSelected.Add(unit.Key);
            }
        }

        return dragSelected;
    }
    
}