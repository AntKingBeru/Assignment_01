using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
 {
     [SerializeField] private NavMeshAgent agent;
     [SerializeField] private InputActionReference targetInput;
     [SerializeField] private Animator animator;
 
     private Camera _mainCamera;
     private bool _moveRequested;
     private float _speed;
 
     private void Start()
     {
         _mainCamera = Camera.main;
 
         targetInput.action.performed += OnTarget;
     }
     
     private void OnDestroy()
     {
         targetInput.action.performed -= OnTarget;
     }
 
     private void OnTarget(InputAction.CallbackContext context)
     {
         _moveRequested = true;
     }

     private void Update()
     {
         _speed = agent.velocity.magnitude;
         _speed = Mathf.InverseLerp(_speed, 0f, 1f);
         
         animator.SetFloat("Speed", _speed);
         
         if (!_moveRequested) return;
         _moveRequested = false;
         
         if (BuildUIController.Instance && BuildUIController.Instance.IsPlacing) return;
         
         if (IsPointerOverUI()) return;
         
         var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
         
         if (!Physics.Raycast(ray, out var hit)) return;
         
         agent.SetDestination(hit.point);
     }
 
     private bool IsPointerOverUI()
     {
         return EventSystem.current && EventSystem.current.IsPointerOverGameObject();
     }
 }