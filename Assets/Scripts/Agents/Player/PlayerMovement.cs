using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private InputActionReference targetInput;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;

        targetInput.action.performed += OnTarget;
    }

    private void OnTarget(InputAction.CallbackContext context)
    {
        if (BuildUIController.Instance != null && BuildUIController.Instance.IsPlacing) return;
        
        var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (!Physics.Raycast(ray, out var hit)) return;
        
        agent.SetDestination(hit.point);
    }
}