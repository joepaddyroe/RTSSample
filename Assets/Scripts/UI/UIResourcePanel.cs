using System;
using TMPro;
using UnityEngine;

public class UIResourcePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldValue;
    [SerializeField] private TMP_Text _lumberValue;

    private float _goldDeniedTimer;
    private float _lumberDeniedTimer;
    
    public void SetUIResources(int gold, int lumber)
    {
        _goldValue.text = gold.ToString();
        _lumberValue.text = lumber.ToString();
    }

    public void FlashResourceDenied(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Gold:
                _goldDeniedTimer = 2;
                _goldValue.color = Color.red;
                break;
            case ResourceType.Lumber:
                _lumberDeniedTimer = 2;
                _lumberValue.color = Color.red;
                break;
        }
    }

    private void Update()
    {
        if (_goldDeniedTimer > 0)
        {
            _goldDeniedTimer -= Time.deltaTime;
            if (_goldDeniedTimer <= 0)
            {
                _goldDeniedTimer = 0;
                _goldValue.color = Color.white;
            }
        }
        if (_lumberDeniedTimer > 0)
        {
            _lumberDeniedTimer -= Time.deltaTime;
            if (_lumberDeniedTimer <= 0)
            {
                _lumberDeniedTimer = 0;
                _lumberValue.color = Color.white;
            }
        }
    }
}