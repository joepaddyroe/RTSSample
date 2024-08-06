using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int _currentGold;
    [SerializeField] private int _currentLumber;


    [SerializeField] private UIGame _uiGame;
    [SerializeField] private PlayerInteractionManager _playerInteractionManager;
    [SerializeField] private ConstructionPrefabsSO _constructionPrefabs;

    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    private void Awake()
    {
        _instance = this;
        _uiGame.UIResourcePanel.SetUIResources(_currentGold, _currentLumber);
    }

    public void AddGold(int amount)
    {
        _currentGold += amount;
        _uiGame.UIResourcePanel.SetUIResources(_currentGold, _currentLumber);
    }
    
    public void AddLumber(int amount)
    {
        _currentLumber += amount;
        _uiGame.UIResourcePanel.SetUIResources(_currentGold, _currentLumber);
    }
    

    public void TryStartConstructionProcess(ConstructionType constructionType)
    {
        ConstructionPackage package = _constructionPrefabs.GetConstructionPackageByConstructionType(constructionType);

        bool enoughGold = _currentGold >= package.GoldCost;
        bool enoughLumber = _currentLumber >= package.LumberCost;
        
        if (enoughGold && enoughLumber)
        {
            // _currentGold -= package.GoldCost;
            // _currentLumber -= package.LumberCost;
            _playerInteractionManager.SetPlacingConstructionState(package.Prefab, constructionType);
            //_uiGame.UIResourcePanel.SetUIResources(_currentGold, _currentLumber);
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
        _uiGame.UIResourcePanel.SetUIResources(_currentGold, _currentLumber);
    }

    public ConstructionPackage GetConstructionPackageByType(ConstructionType constructionType)
    {
        return _constructionPrefabs.GetConstructionPackageByConstructionType(constructionType);
    }
}

public enum ConstructionType
{
    TownHall,
    Farm,
    Barracks
}

public enum ProductionType
{
    Peasant,
    Footman
}