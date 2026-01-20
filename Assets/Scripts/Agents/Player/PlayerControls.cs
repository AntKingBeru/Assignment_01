using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionReference harvestAction;
    
    private QuarryMinePoint _currentMinePoint;
    private TreeMinePoint _currentTreeMinePoint;
    private EggPickupPoint _currentEggPickupPoint;

    private void Start()
    {
        harvestAction.action.performed += OnHarvest;
    }

    private void OnTriggerEnter(Collider other)
    {
        var harvestStone = other.GetComponent<QuarryMinePoint>();
        if (harvestStone) _currentMinePoint = harvestStone;
        
        var harvestWood = other.GetComponent<TreeMinePoint>();
        if (harvestWood) _currentTreeMinePoint = harvestWood;
        
        var eggPickup = other.GetComponent<EggPickupPoint>();
        if (eggPickup) _currentEggPickupPoint = eggPickup;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<QuarryMinePoint>() == _currentMinePoint) _currentMinePoint = null;
        if (other.GetComponent<TreeMinePoint>() == _currentTreeMinePoint) _currentTreeMinePoint = null;
        if (other.GetComponent<EggPickupPoint>() == _currentEggPickupPoint) _currentEggPickupPoint = null;
    }

    private void OnHarvest(InputAction.CallbackContext context)
    {
        if (!ValidHarvestPoint()) return;

        if (_currentMinePoint.CanHarvest()) _currentMinePoint.Harvest();
        if (_currentTreeMinePoint.CanHarvest()) _currentTreeMinePoint.Harvest();
        if (_currentEggPickupPoint.CanHarvest()) _currentEggPickupPoint.Harvest();
    }

    private bool ValidHarvestPoint()
    {
        return _currentMinePoint != null || _currentTreeMinePoint != null || _currentEggPickupPoint != null;
    }
}