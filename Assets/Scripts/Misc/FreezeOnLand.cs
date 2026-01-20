using UnityEngine;
using UnityEngine.Events;

public class FreezeOnLand : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    
    public UnityEvent onLanded;

    private Rigidbody _rb;
    private Collider _collider;
    private bool _landed = false;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_landed) return;

        if (((1 << other.gameObject.layer) & groundLayer) == 0) return;
        
        Land();
    }
    
    private void Land()
    {
        _landed = true;
        
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        
        _rb.isKinematic = true; 
        _rb.useGravity = false;
        
        _collider.isTrigger = true;
        
        onLanded.Invoke();
    }
}