using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float speed = 2f, range = 4f;
    private Vector3 _start = Vector3.zero;
    
    private void Start()
    {
        _start = transform.position;
    }
    
    private void Update()
    {
        transform.position = _start + Vector3.right * (Mathf.Sin(Time.time * speed) * range);
    }
}