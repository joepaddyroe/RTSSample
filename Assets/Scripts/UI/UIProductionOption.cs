using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIProductionOption : MonoBehaviour
{
    [SerializeField] private TMP_Text _productionLabel;

    private ProductionPackage _productionPackage;
    private UIProductionPanel _uiProductionPanel;
    private ProductionType _productionType;

    public void Init(ProductionPackage productionPackage, UIProductionPanel uiProductionPanel)
    {
        _productionPackage = productionPackage;
        
        _productionType = _productionPackage.ProductionType;
        _productionLabel.text = _productionType.ToString();

        _uiProductionPanel = uiProductionPanel;
    }

    public void OnOptionClicked()
    {
        _uiProductionPanel.ProductionOptionClicked(_productionPackage);
    }
}
