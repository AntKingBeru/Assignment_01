using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricCameraRotate : MonoBehaviour
{
    [Header("Camera Points")]
    [SerializeField] private Transform[] cameraPoints;
    
    [Header("Input Actions")]
    [SerializeField] private InputActionReference rotateLeftInput;
    [SerializeField] private InputActionReference rotateRightInput;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    
    private int _currentIndex = 0;
    private bool _isTransitioning = false;

    private void OnEnable()
    {
        rotateLeftInput.action.performed += RotateLeft;
        rotateRightInput.action.performed += RotateRight;
        
        rotateLeftInput.action.Enable();
        rotateRightInput.action.Enable();
    }

    private void OnDisable()
    {
        rotateLeftInput.action.performed -= RotateLeft;
        rotateRightInput.action.performed -= RotateRight;
    }

    private void LateUpdate()
    {
        if (!_isTransitioning) return;
        var target = cameraPoints[_currentIndex];
            
        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            Time.deltaTime * moveSpeed);
            
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            target.rotation,
            rotationSpeed * Time.deltaTime);

        if (!(Vector3.Distance(transform.position, target.position) < 0.01f) ||
            !(Quaternion.Angle(transform.rotation, target.rotation) < 0.5f)) return;
        transform.position = target.position;
        transform.rotation = target.rotation;
        _isTransitioning = false;
    }
    
    private void RotateLeft(InputAction.CallbackContext context)
    {
        if (_isTransitioning) return;
        
        _currentIndex--;
        if (_currentIndex < 0) _currentIndex = cameraPoints.Length - 1;
        
        _isTransitioning = true;
    }
    
    private void RotateRight(InputAction.CallbackContext context)
    {
        if (_isTransitioning) return;
        
        _currentIndex++;
        if (_currentIndex >= cameraPoints.Length) _currentIndex = 0;
        
        _isTransitioning = true;
    }
}