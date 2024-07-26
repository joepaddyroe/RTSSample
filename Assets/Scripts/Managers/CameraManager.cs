using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private Transform _cameraDolly;
    [SerializeField] private float _cameraHorizontalSpeed;
    [SerializeField] private float _cameraVerticalSpeed;
    [SerializeField] private float _cameraHorizontalBorder;
    [SerializeField] private float _cameraVerticalBorder;
    [SerializeField] private float _lerpCoef;
    [SerializeField] private float _cameraHorizontalMin;
    [SerializeField] private float _cameraHorizontalMax;
    [SerializeField] private float _cameraVerticalMin;
    [SerializeField] private float _cameraVerticalMax;
    
    private Vector3 _targetPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = _cameraDolly.position;
    }

    // Update is called once per frame
    void Update()
    {

        bool offScreen = (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width) ||
                         (Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height);
        
        if(offScreen)
            return;
        
        float horizontalBorderWidth = (Screen.width * _cameraHorizontalBorder);
        float verticalBorderWidth = (Screen.height * _cameraVerticalBorder);
        
        float horizontalMax = Screen.width - horizontalBorderWidth;
        float horizontalMin = horizontalBorderWidth;
        float verticalMax = Screen.height - verticalBorderWidth;
        float verticalMin = verticalBorderWidth;
        
        
        if (Input.mousePosition.x > horizontalMax && Input.mousePosition.x < Screen.width)
            _targetPosition.x += _cameraHorizontalSpeed * Time.deltaTime;
        if (Input.mousePosition.x  > 0 && Input.mousePosition.x < horizontalMin)
            _targetPosition.x -= _cameraHorizontalSpeed * Time.deltaTime;
        if (Input.mousePosition.y > verticalMax)
            _targetPosition.z += _cameraVerticalSpeed * Time.deltaTime;
        if (Input.mousePosition.y < verticalMin)
            _targetPosition.z -= _cameraVerticalSpeed * Time.deltaTime;

        _targetPosition.x = Mathf.Clamp(_targetPosition.x, _cameraHorizontalMin, _cameraHorizontalMax);
        _targetPosition.z = Mathf.Clamp(_targetPosition.z, _cameraVerticalMin, _cameraVerticalMax);
        
        float newX = Mathf.Lerp(_cameraDolly.position.x, _targetPosition.x, _lerpCoef);
        float newZ = Mathf.Lerp(_cameraDolly.position.z, _targetPosition.z, _lerpCoef);

        _cameraDolly.position = new Vector3(newX, _cameraDolly.position.y, newZ);
    }
}
