using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProductionPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _optionsPanel;
    [SerializeField] private GameObject _uiProductionOptionPrefab;
    [SerializeField] private GameManager _gameManager;
    
    private UnitProductionBuilding _unitProductionBuilding;
    private List<ProductionPackage> _productionPackages;
    
    public void Init(UnitProductionBuilding unitProductionBuilding, List<ProductionPackage> productionPackages)
    {
        _unitProductionBuilding = unitProductionBuilding;
        _productionPackages = productionPackages;

        PopulateUI();
    }
    
    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public void ProductionOptionClicked(ProductionPackage productionPackage)
    {
        if(productionPackage != null)
            GameManager.Instance.TryStartProductionProcess(_unitProductionBuilding, productionPackage);
        else
            Debug.Log("The production package was null");
    }

    private void PopulateUI()
    {
        foreach (Transform child in _optionsPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (ProductionPackage package in _productionPackages)
        {
            GameObject packageOption = GameObject.Instantiate(_uiProductionOptionPrefab, _optionsPanel);
            packageOption.GetComponent<UIProductionOption>().Init(package, this);
        }
    }
}
