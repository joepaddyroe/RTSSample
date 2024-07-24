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
    
    private void Awake()
    {
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
    
    public void SetConstructionProducingUnitSelected(bool visible, ConstructionProducingUnit constructionProducingUnit = null, List<ConstructionPackage> constructionPackages = null)
    {
        _uiConstructionPanel.SetActive(visible);
        if(visible)
            UIConstructionPanel.Init(constructionProducingUnit, constructionPackages);
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
    
    
    
    // set drag selection state
    public void SetDragSelectorVisible(bool visible)
    {
        _dragSelectorPanel.transform.gameObject.SetActive(visible);
    }
    public void SetDragSelectorSizeAndPosition(Vector2 startPoint, Vector2 endPoint)
    {
        float width = Mathf.Abs(endPoint.x - startPoint.x) / _parentCanvas.scaleFactor;
        float height = Mathf.Abs(endPoint.y - startPoint.y) / _parentCanvas.scaleFactor;
        _dragSelectorPanel.sizeDelta = new Vector2(width, height);

        //_dragSelectorPanel.anchoredPosition = (startPoint - new Vector2(Screen.width/2, Screen.height/2));
        _dragSelectorPanel.anchoredPosition = (startPoint + new Vector2((endPoint.x - startPoint.x)/2, (endPoint.y - startPoint.y)/2)) - new Vector2((float)Screen.width/2, (float)Screen.height/2);
        _dragSelectorPanel.anchoredPosition /= _parentCanvas.scaleFactor;
    }
    
    
}

public enum BuildingConstructionPanel
{
    TownHall,
    Barracks
}

public enum UnitConstructionPanel
{
    Peasant,
    Footman
}