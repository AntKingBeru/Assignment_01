using UnityEngine;
using UnityEngine.InputSystem;

public class MinimapCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float height = 75f;
    [SerializeField] private float followSmooth = 15f;
    
    [SerializeField] private InputActionReference zoomAction;
    [SerializeField] private float zoomSpeed = 5f, minZoom = 10f, maxZoom = 30f, zoomSmoothTime = 0.15f;
    [SerializeField] private float startZoom = 15f;
    
    private Camera _camera;
    private float _targetZoom, _zoomVelocity;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographicSize = startZoom;
        _targetZoom = _camera.orthographicSize;
    }

    private void OnEnable()
    {
        if (!zoomAction.action.enabled) zoomAction.action.Enable();
    }
    
    private void OnDisable()
    {
        zoomAction.action.Disable();
    }

    private void LateUpdate()
    {
        FollowTarget();
        HandleZoom();
    }
    
    private void FollowTarget()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (!target) return;
        }
        
        var desiredPosition = new Vector3(target.position.x, height, target.position.z);
        var time = Mathf.Clamp01(followSmooth * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, time);
    }
    
    private void HandleZoom()
    {
        var zoomInput = zoomAction.action.ReadValue<float>();
        
        if (!Mathf.Approximately(zoomInput, 0f))
        {
            _targetZoom = Mathf.Clamp(_targetZoom - zoomInput * zoomSpeed, minZoom, maxZoom);
        }

        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _targetZoom, ref _zoomVelocity, zoomSmoothTime);
    }
}