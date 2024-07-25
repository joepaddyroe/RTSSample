using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConstructionPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _optionsPanel;
    [SerializeField] private GameObject _uiConstructionOptionPrefab;
    [SerializeField] private GameManager _gameManager;
    
    private WorkerUnit _constructionProducingUnit;
    private List<ConstructionPackage> _constructionPackages;
    
    public void Init(WorkerUnit constructionProducingUnit, List<ConstructionPackage> constructionPackages)
    {
        _constructionProducingUnit = constructionProducingUnit;
        _constructionPackages = constructionPackages;

        PopulateUI();
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public void ConstructionOptionClicked(ConstructionPackage constructionPackage)
    {
        if(constructionPackage != null)
            //_constructionProducingUnit.StateMachine.SetState(new ConstructionProducingUnitConstructingState(_constructionProducingUnit, constructionPackage));
            _gameManager.StartConstruction(constructionPackage.ConstructionType);
        else
            Debug.Log("The construction package was null");
        
    }

    private void PopulateUI()
    {
        foreach (Transform child in _optionsPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (ConstructionPackage package in _constructionPackages)
        {
            GameObject packageOption = GameObject.Instantiate(_uiConstructionOptionPrefab, _optionsPanel);
            packageOption.GetComponent<UIConstructionOption>().Init(package, this);
        }
    }
}
