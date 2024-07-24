using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectedUnit : MonoBehaviour
{

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _selectionImage;
    
    private UnitBase _unit;

    public void SetUnit(UnitBase unit)
    {
        _unit = unit;
    }
    
    void Update()
    {
        _selectionImage.enabled = _unit.IsSelected;
        if (_unit.IsSelected)
        {
            transform.position =
                RectTransformUtility.WorldToScreenPoint(Camera.main, _unit.transform.position);
        }
    }
}
