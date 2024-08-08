using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int _currentGold;
    [SerializeField] private int _currentLumber;
    [SerializeField] private int _currentFood = 3;
    [SerializeField] private int _currentFoodConsumed;
    
    [SerializeField] private UIGame _uiGame;
    [SerializeField] private PlayerInteractionManager _playerInteractionManager;
    
    [SerializeField] private ConstructionPrefabsSO _constructionPrefabs;
    
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    private void Awake()
    {
        _instance = this;
        _uiGame.UIResourcePanel.SetUIResources(_currentGold, _currentLumber);
        UpdateGoldAndLumberUI();
        UpdateFoodUI();
    }

    public void AddGold(int amount)
    {
        _currentGold += amount;
        UpdateGoldAndLumberUI();
    }
    
    public void AddLumber(int amount)
    {
        _currentLumber += amount;
        UpdateGoldAndLumberUI();
    }
    
    private void UpdateGoldAndLumberUI()
    {
        _uiGame.UIResourcePanel.SetUIResources(_currentGold, _currentLumber);
    }
    
    
    public void AddFood(int amount)
    {
        _currentFood += amount;
        UpdateFoodUI();
    }
    public void RemoveFood(int amount)
    {
        _currentFood -= amount;
        UpdateFoodUI();
    }
    
    public void AddFoodConsumer(int amount)
    {
        _currentFoodConsumed += amount;
        UpdateFoodUI();
    }
    public void RemoveFoodConsumer(int amount)
    {
        _currentFoodConsumed -= amount;
        UpdateFoodUI();
    }

    private void UpdateFoodUI()
    {
        _uiGame.UIResourcePanel.SetFoodValue(_currentFoodConsumed, _currentFood);
    }

    public void TryStartConstructionProcess(ConstructionType constructionType)
    {
        ConstructionPackage package = _constructionPrefabs.GetConstructionPackageByConstructionType(constructionType);

        bool enoughGold = _currentGold >= package.GoldCost;
        bool enoughLumber = _currentLumber >= package.LumberCost;
        
        if (enoughGold && enoughLumber)
        {
            _playerInteractionManager.SetPlacingConstructionState(package.Prefab, constructionType);
        }
        else
        {
            string failMessage = 
                "Sorry, not enough " 
                + (!enoughGold ? "Gold " : "") 
                + (!enoughGold && !enoughLumber ? " and " : "") 
                + (!enoughLumber ? "Lumber" : "");
            Debug.Log(failMessage);
            
            if(!enoughGold)
                _uiGame.UIResourcePanel.FlashResourceDenied(ResourceType.Gold);
            if(!enoughLumber)
                _uiGame.UIResourcePanel.FlashResourceDenied(ResourceType.Lumber);
        }
    }
    public void CompleteConstructionProcess(ConstructionType constructionType)
    {
        ConstructionPackage package = _constructionPrefabs.GetConstructionPackageByConstructionType(constructionType);
        _currentGold -= package.GoldCost;
        _currentLumber -= package.LumberCost;
        UpdateGoldAndLumberUI();
    }

    public ConstructionPackage GetConstructionPackageByType(ConstructionType constructionType)
    {
        return _constructionPrefabs.GetConstructionPackageByConstructionType(constructionType);
    }
    
    
    public void TryStartProductionProcess(UnitProductionBuilding unitProductionBuilding, ProductionPackage productionPackage)
    {
        bool enoughGold = _currentGold >= productionPackage.GoldCost;
        bool enoughLumber = _currentLumber >= productionPackage.LumberCost;
        bool enoughFood = (_currentFood-_currentFoodConsumed) >= productionPackage.FoodCost;
        
        if (enoughGold && enoughLumber && enoughFood)
        {
            unitProductionBuilding.StateMachine.SetState(new UnitProductionBuildingProductionState(unitProductionBuilding, productionPackage));
            _currentGold -= productionPackage.GoldCost;
            _currentLumber -= productionPackage.LumberCost;
            _currentFoodConsumed += productionPackage.FoodCost;
            UpdateGoldAndLumberUI();
            UpdateFoodUI();
        }
        else
        {
            string failMessage = 
                "Sorry, not enough " 
                + (!enoughGold ? "Gold " : "") 
                + (!enoughGold && !enoughLumber ? " and " : "") 
                + (!enoughLumber ? "Lumber" : "")
                + (!enoughFood ? "Food" : "");
            Debug.Log(failMessage);
            
            if(!enoughGold)
                _uiGame.UIResourcePanel.FlashResourceDenied(ResourceType.Gold);
            if(!enoughLumber)
                _uiGame.UIResourcePanel.FlashResourceDenied(ResourceType.Lumber);
            if(!enoughFood)
                _uiGame.UIResourcePanel.FlashResourceDenied(ResourceType.Food);
        }
    }
}