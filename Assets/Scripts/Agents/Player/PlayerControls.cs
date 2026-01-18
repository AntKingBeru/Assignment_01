using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionReference harvestAction;
    
    private QuarryMinePoint _currentMinePoint;

    private void Start()
    {
        harvestAction.action.performed += OnHarvest;
    }

    private void OnTriggerEnter(Collider other)
    {
        var harvest = other.GetComponent<QuarryMinePoint>();
        if (harvest != null) _currentMinePoint = harvest;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<QuarryMinePoint>() == _currentMinePoint) _currentMinePoint = null;
    }

    private void OnHarvest(InputAction.CallbackContext context)
    {
        if (_currentMinePoint == null) return;

        if (_currentMinePoint.CanHarvest()) _currentMinePoint.Harvest();
    }
}