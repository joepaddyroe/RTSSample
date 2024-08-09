using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIProductionOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _uiProductionPanel.ProductionOptionHovered(_productionPackage);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _uiProductionPanel.ProductionOptionExited();
    }
}
