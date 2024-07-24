using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int _currentGold;
    [SerializeField] private int _currentLumber;
    

    [SerializeField] private PlayerInteractionManager _playerInteractionManager;
    [SerializeField] private ConstructionPrefabsSO _constructionPrefabs;
    

    public void StartConstruction(ConstructionType constructionType)
    {
        ConstructionPackage package = _constructionPrefabs.GetConstructionPackageByConstructionType(constructionType);

        bool enoughGold = _currentGold >= package.GoldCost;
        bool enoughLumber = _currentLumber >= package.LumberCost;
        
        if (enoughGold && enoughLumber)
        {
            _currentGold -= package.GoldCost;
            _currentLumber -= package.LumberCost;
            _playerInteractionManager.SetPlacingConstructionState(package.Prefab);
        }
        else
        {
            string failMessage = 
                "Sorry, not enough " 
                + (!enoughGold ? "Gold " : "") 
                + (!enoughGold && !enoughLumber ? " and " : "") 
                + (!enoughLumber ? "Lumber" : "");
            Debug.Log(failMessage);
        }
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