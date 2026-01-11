using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AgentMovement : MonoBehaviour
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
        var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (!Physics.Raycast(ray, out var hit)) return;

        var clickPoint = hit.point;
        
        agent.SetDestination(clickPoint);
    }
}