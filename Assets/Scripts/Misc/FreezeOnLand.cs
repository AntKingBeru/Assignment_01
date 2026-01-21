using UnityEngine;
using UnityEngine.Events;

public class FreezeOnLand : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider physicsCollider;

    private Rigidbody _rb;
    private bool _landed = false;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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

        if (physicsCollider != null)
        {
            physicsCollider.enabled = false;
        }
    }
}