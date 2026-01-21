using UnityEngine;

public class MinimapIconScale : MonoBehaviour
{
    [SerializeField] private Transform minimapCamera;
    [SerializeField] private float baseScale = 2f;
    
    private Camera _camera;

    private void Start()
    {
        _camera = minimapCamera.GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        var zoom = _camera.orthographicSize;
        transform.localScale = Vector3.one * (baseScale * zoom * 0.075f);
    }
}