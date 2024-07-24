using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIConstructionOption : MonoBehaviour
{
    [SerializeField] private TMP_Text _constructionLabel;

    private ConstructionPackage _constructionPackage;
    private UIConstructionPanel _uiConstructionPanel;
    private ConstructionType _constructionType;

    public void Init(ConstructionPackage productionPackage, UIConstructionPanel uiConstructionPanel)
    {
        _constructionPackage = productionPackage;
        
        _constructionType = _constructionPackage.ConstructionType;
        _constructionLabel.text = _constructionType.ToString();

        _uiConstructionPanel = uiConstructionPanel;
    }

    public void OnOptionClicked()
    {
        _uiConstructionPanel.ConstructionOptionClicked(_constructionPackage);
    }
}
