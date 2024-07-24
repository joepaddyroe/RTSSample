using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectedBuilding : MonoBehaviour
{

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _selectionImage;
    
    private BuildingBase _building;

    public void SetBuilding(BuildingBase building)
    {
        _building = building;
    }
    
    void Update()
    {
        _selectionImage.enabled = _building.IsSelected;
        if (_building.IsSelected)
        {
            transform.position =
                RectTransformUtility.WorldToScreenPoint(Camera.main, _building.transform.position);
        }
    }
}
